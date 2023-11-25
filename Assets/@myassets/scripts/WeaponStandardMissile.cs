using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// My name is Jeremy Donaldson. If you can... somehow.. and I really envy you if you can... have a peaceful night.
// Jeremy
public class WeaponStandardMissile : Weapon
{

    bool isLaunched = false;
    bool isActive = false;
    bool lostLock = false;

    [SerializeField]
    float activationTime = 0.5f;
    float activationTimer;
    [SerializeField]
    float aliveTime = 10f;
    [SerializeField]
    float lockAngle = 60f;
    [SerializeField]
    float maxTurnRate=5.0f; //Degrees per second
    [SerializeField]
    float distanceFuze = 3.0f;
    [SerializeField]
    float targetSpeed = 400f;
    [SerializeField]
    float acceleration = 200f;
    [SerializeField]
    LockType missileType = LockType.ALL;
    [SerializeField]
    float groundTurnDistance = 10;

    float activationY;
    bool groundAttack = false;

    [SerializeField]
    Transform target;
    Rigidbody targetRigidbody;

    Rigidbody myRigidBody;

    Vector3 previousPos;

    // Start is called before the first frame update
    void Start()
    {
        aliveTime += activationTime;
        myRigidBody = GetComponent<Rigidbody>();
        targetRigidbody = target.GetComponent<Rigidbody>(); // DEBUG ONLY REMOVE LATER
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaunched)
        {
            activationTimer += Time.deltaTime;
            if (activationTime <= activationTimer && !isActive)
            {
                missileActivate();
            }
            if (isActive)
            {
                if (!lostLock)
                {
                    if (missileType == LockType.GROUND)
                        ActiveGroundMissileUpdate();
                    else
                        ActiveMissileUpdate();
                }
            }
            if (activationTimer > aliveTime)
            {
                Destroy(this.gameObject);
            }
            
        } 
    }
   

    private void missileActivate()
    {
        myRigidBody.useGravity = false;
        if (missileType == LockType.GROUND)
        {
            activationY = transform.position.y;
            previousPos = new Vector3(target.position.x,activationY,target.position.z);
        }  
        else
            previousPos = target.position;
        isActive = true;
        
    }
    private void ActiveGroundMissileUpdate()
    {
        Vector3 targetAbove = new Vector3(target.position.x,activationY,target.position.z);
        Debug.Log(Vector3.Distance(transform.position, targetAbove));
        Vector3 targetPos;
        if((Vector3.Distance(transform.position, targetAbove) < groundTurnDistance))
        {
            groundAttack = true;
        }
        if(groundAttack)
        {
            targetPos = target.position;
        }
        else
        {
            targetPos = targetAbove;
        }
        float distaceToPrevious = (previousPos - targetPos).magnitude;
        float secondsAway = distaceToPrevious / myRigidBody.velocity.magnitude;
        Vector3 trackedDirection = targetRigidbody.velocity.normalized; // Where enemy is going
        float trackedSpeed = targetRigidbody.velocity.magnitude; // How fast the enemy is going
        Vector3 newObjectivePost = targetPos + trackedDirection * (trackedSpeed * secondsAway);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, newObjectivePost - transform.position, maxTurnRate * Mathf.Deg2Rad * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (myRigidBody.velocity.magnitude < targetSpeed)
        {
            myRigidBody.velocity = transform.forward * (myRigidBody.velocity.magnitude + (acceleration * Time.deltaTime));
        }
        else
        {
            myRigidBody.velocity = transform.forward * myRigidBody.velocity.magnitude;
        }
        previousPos = targetPos;
    }

    private void ActiveMissileUpdate()
    {
        float distanceToTarget = (target.position - transform.position).magnitude;
        //Debug.Log(distanceToTarget);
        if (distanceToTarget <= distanceFuze)
        {
            Debug.Log("Distance Fuze Activated");
            Destroy(gameObject);
        }
        float currentMaxTurn = (myRigidBody.velocity.magnitude / targetSpeed) * maxTurnRate;

        float distaceToPrevious = (previousPos - transform.position).magnitude;
        float secondsAway = distaceToPrevious / myRigidBody.velocity.magnitude;

        Vector3 trackedDirection = targetRigidbody.velocity.normalized; // Where enemy is going
        float trackedSpeed = targetRigidbody.velocity.magnitude; // How fast the enemy is going
        Vector3 newObjectivePost = target.position + trackedDirection * (trackedSpeed * secondsAway); // where the enemy will be when we arrive
        //Debug.Log(Vector3.Angle(transform.forward, newObjectivePost - transform.position));
        if(Vector3.Angle(transform.forward, (target.position - transform.position).normalized) >= lockAngle)
        {
            Debug.Log("Lost lock");
            lostLock = true;
            return;
        }
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, newObjectivePost - transform.position, currentMaxTurn * Mathf.Deg2Rad * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        if (myRigidBody.velocity.magnitude < targetSpeed)
        {
            myRigidBody.velocity = transform.forward * (myRigidBody.velocity.magnitude + (acceleration * Time.deltaTime));
        }
        else
        {
            myRigidBody.velocity = transform.forward * myRigidBody.velocity.magnitude;
        }
        previousPos = target.position;
    }

    public void LaunchTest()
    {
        Launch(new Vector3(0, 0, 0));
    }

    public override void Launch(Vector3 inSpeed)
    {
        transform.parent = null;
        myRigidBody.isKinematic = false;
        myRigidBody.velocity = inSpeed;
        isLaunched = true;
    }

    public override LockType ReturnLockType()
    {
        return LockType.ALL;
    }

    public override void SetTarget(Transform targetTrans)
    {
        target = targetTrans;
        targetRigidbody = target.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
