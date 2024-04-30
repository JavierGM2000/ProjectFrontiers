using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MissileAgent : Agent
{
    MissileSettings m_missileSetting;
    Rigidbody missileRigidbody;
    Rigidbody enemyRigidBody;
    //bool thrustActive = false;

    float thrustTimer;
    float batteryTimer;

    float velocity;
    float acceleration;

    Vector3 enemyOgSpawn;

    public override void Initialize()
    {
        m_missileSetting = FindObjectOfType<MissileSettings>();
        missileRigidbody = GetComponent<Rigidbody>();
        missileRigidbody.mass = m_missileSetting.missileMass;
        enemyRigidBody = m_missileSetting.target.gameObject.GetComponent<Rigidbody>();
        enemyOgSpawn = m_missileSetting.target.position;
        acceleration = m_missileSetting.thurstForce / m_missileSetting.missileMass;

    }

    /**
     * VECTOR SENSOR
     * 0 - Enemy x distance
     * 1 - Enemy y distance
     * 2 - Enemy z distance
     * 3 - Angle to Missile
     * 4 - Missile speed
     * 5 - Missile x forward
     * 6 - Missile y forward
     * 7 - Missile z forward
     * 8 - Rotation x
     * 9 - rotation y
     * 10 - rotaion z
     */
    public override void CollectObservations(VectorSensor sensor)
    {
        //Vector3 distanceVector = (m_missileSetting.target.position - transform.position)/m_missileSetting.referenceRange;
        Plane xPlane = new Plane(transform.right, transform.position);
        Plane yPlane = new Plane(transform.up, transform.position);
        Plane zPlane = new Plane(transform.forward, transform.position);

        float xdistance = xPlane.GetDistanceToPoint(m_missileSetting.target.position);
        float ydistance = yPlane.GetDistanceToPoint(m_missileSetting.target.position);
        float zdistance = zPlane.GetDistanceToPoint(m_missileSetting.target.position);

        Vector3 distanceVector = new Vector3(xdistance, ydistance, zdistance) / m_missileSetting.referenceRange;

        //Debug.Log($"{xdistance}-{ydistance}-{zdistance}");
        sensor.AddObservation(distanceVector);
        /*sensor.AddObservation(xdistance);
        sensor.AddObservation(ydistance);
        sensor.AddObservation(zdistance);*/

        float angleToMissile = Vector3.Angle(transform.forward, distanceVector.normalized);
        //Debug.Log(distanceVector);
        sensor.AddObservation(angleToMissile / m_missileSetting.DegreeLock);

        sensor.AddObservation(velocity / m_missileSetting.maxSpeed);

        sensor.AddObservation(transform.forward);
        sensor.AddObservation(transform.eulerAngles / 360f);
        /*sensor.AddObservation(transform.eulerAngles.x);
        sensor.AddObservation(transform.eulerAngles.y);
        sensor.AddObservation(transform.eulerAngles.z);*/
    }

    public void MoveAgent(ActionSegment<float> act)
    {

        float maxRotation = m_missileSetting.DegreesPerFrame;
        //Debug.Log(act[0]);
        float pitch = maxRotation * Mathf.Clamp(act[0], -1, 1);
        float yaw = maxRotation * Mathf.Clamp(act[1], -1, 1);

        //missileRigidbody.MoveRotation(Quaternion.EulerAngles(pitch,yaw,0));
        //missileRigidbody.angularVelocity = new Vector3(pitch,yaw,0);
        transform.Rotate(pitch, yaw, 0, Space.Self);

        /*Vector3 velocity = missileRigidbody.velocity;
        velocity = Quaternion.Euler(pitch, yaw, 0) * velocity;
        missileRigidbody.velocity = velocity;*/

        Vector3 distanceVector = (m_missileSetting.target.position - transform.position);
        float angleToMissile = Vector3.Angle(transform.forward, distanceVector.normalized);
        if (angleToMissile > m_missileSetting.DegreeLock)
        {
            //Lost lock, negative reward and end episode
            AddReward(-5f);
            EndEpisode();
        }
        float angleFactor = angleToMissile / m_missileSetting.DegreeLock;
        float reward = AngleRewardCalculator(angleFactor);
        //Debug.Log(reward);
        AddReward(reward);



        //REWARD
        //Penalizar si no va recto?
        //penalizar si apunto de salir del rango o demasiado en el centro?
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        MoveAgent(actionBuffers.ContinuousActions);




        //float distanceFactor = distanceVector.magnitude/m_missileSetting.referenceRange;
        //AddReward(DistanceRewardCalculator(distanceFactor));


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continiousActionsOut = actionsOut.ContinuousActions;
        continiousActionsOut[1] = Input.GetAxis("Horizontal");
        continiousActionsOut[0] = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {

        if (thrustTimer > 0)
        {
            velocity += acceleration * Time.fixedDeltaTime;
            //missileRigidbody.AddForce(new Vector3(0,0, m_missileSetting.thurstForce));
            thrustTimer -= Time.fixedDeltaTime;
            if (thrustTimer <= 0)
            {
                m_missileSetting.engine.GetComponent<Renderer>().material = m_missileSetting.engineOff;
            }
        }
        missileRigidbody.velocity = transform.forward * velocity;
        if (batteryTimer > 0)
        {
            batteryTimer -= Time.fixedDeltaTime;
        }
        else
        {
            //Missile navigation died, negative reward and end episode
            //AddReward(-10f);
            EndEpisode();
        }
    }

    public override void OnEpisodeBegin()
    {
        // Reset missile pos
        transform.position = new Vector3(0, 0, 0);
        // Random missile roll
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        //transform.rotation = Quaternion.Euler(0, 0, 0);

        m_missileSetting.engine.GetComponent<Renderer>().material = m_missileSetting.engineOn;

        velocity = m_missileSetting.initSpeed;
        missileRigidbody.velocity = new Vector3(0, 0, m_missileSetting.initSpeed);
        missileRigidbody.angularVelocity = new Vector3(0, 0, 0);

        thrustTimer = m_missileSetting.thrustTime;
        batteryTimer = m_missileSetting.lifeTime;

        m_missileSetting.target.position = new Vector3(0, 0, 3000);
        float randomx = Random.Range(-m_missileSetting.spawnPosRand, m_missileSetting.spawnPosRand);
        float randomy = Random.Range(-m_missileSetting.spawnPosRand, m_missileSetting.spawnPosRand);
        float randomz = Random.Range(-m_missileSetting.spawnPosRand, m_missileSetting.spawnPosRand);

        Vector3 spawnPos = new Vector3(enemyOgSpawn.x + randomx, enemyOgSpawn.y + randomy, enemyOgSpawn.z + randomz);
        m_missileSetting.target.position = spawnPos;
        m_missileSetting.target.rotation = Random.rotation;
        missileRigidbody.angularVelocity = new Vector3(0, 0, 0);
        enemyRigidBody.velocity = m_missileSetting.target.forward * m_missileSetting.enemySpeed;


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Reward
            Debug.Log("Impact");
            AddReward(1000f);
            EndEpisode();
        }
    }

    private float AngleRewardCalculator(float x)
    {
        return -2 * x + 1;
    }
    /*private float AngleRewardCalculator(float x)
    {
        if(x < 0.5f)
        {
            return (0.5f * x) - 0.25f;
        }else if (x<0.95)
        {
            return (-1.111111111f * x) + 0.555555555f;
        }
        else
        {
            return (-6f * x) + 5.2f;
        }
    }*/

    private float DistanceRewardCalculator(float x)
    {
        if (x < 0.2)
        {
            return (-2.5f * x) + 0.6f;
        }
        else if (x < 0.5)
        {
            return (-x) + 0.3f;
        }
        else if (x < 0.8)
        {
            return (-1.333333333f * x) + 0.46666666666f;
        }
        else if (x < 1)
        {
            return -x + 0.2f;
        }
        else
        {
            return -0.8f;
        }
    }
}
