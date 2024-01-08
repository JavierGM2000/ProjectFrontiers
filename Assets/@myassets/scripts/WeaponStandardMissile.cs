using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// My name is Jeremy Donaldson. If you can... somehow.. and I really envy you if you can... have a peaceful night.
// Jeremy
public class WeaponStandardMissile : Weapon
{
    [SerializeField]
    private bool hurtsPlayer = false;
    [SerializeField]
    private int damage = 10;
    bool isLaunched = false;
    bool isActive = false;
    bool lostLock = false;
    private MissileWarning targetWarning;

    [SerializeField]
    private float lockDistance = 2800;
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

    private AudioSource launchSound;

    private GameObject smoke;

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
        launchSound = GetComponent<AudioSource>();
        smoke = transform.Find("smoke").gameObject;
        aliveTime += activationTime;
        myRigidBody = GetComponent<Rigidbody>();
        if(target)
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
        smoke.SetActive(true);
        smoke.GetComponent<ParticleSystem>().Play();
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
            target.GetComponent<Life>().dealDamage(damage);
            Destroy(gameObject);
        }
        //float currentMaxTurn = (myRigidBody.velocity.magnitude / targetSpeed) * maxTurnRate;
        float currentMaxTurn =  maxTurnRate;

        float distaceToPrevious = (previousPos - transform.position).magnitude;
        float secondsAway = distaceToPrevious / targetSpeed;

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
        if (!launchSound)
        {
            launchSound = GetComponent<AudioSource>();
        }
        if (!myRigidBody)
        {
            myRigidBody = GetComponent<Rigidbody>();
        }
        launchSound.Play();
        transform.parent = null;
        myRigidBody.isKinematic = false;
        myRigidBody.velocity = inSpeed;
        isLaunched = true;
        if (target.TryGetComponent<MissileWarning>(out MissileWarning misWarn))
        {
            targetWarning = misWarn;
            misWarn.addMissile(this.gameObject);
        }
    }

    public override LockType getLockType()
    {
        return LockType.ALL;
    }

    public override void SetTarget(Transform targetTrans)
    {
        target = targetTrans;
        targetRigidbody = target.GetComponent<Rigidbody>();
    }

    public override float getLockAngle()
    {
        return lockAngle;
    }

    public override float getLockDistance()
    {
        return lockDistance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (hurtsPlayer)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<Life>().dealDamage(damage);
                smoke.transform.parent = null;
                Destroy(smoke, 5f);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                other.gameObject.GetComponent<Life>().dealDamage(damage);
                smoke.transform.parent = null;
                Destroy(smoke, 5f);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (isLaunched  && targetWarning)
        {
            targetWarning.removeMissiles(this.gameObject.GetInstanceID());

        }
    }
}
