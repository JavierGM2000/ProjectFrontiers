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
    float accelerationTime = 1.0f;
    float necessaryforce;

    Transform target;
    Rigidbody targetRigidbody;

    Rigidbody myRigidBody;

    

    // Start is called before the first frame update
    void Start()
    {
        aliveTime += activationTime;
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaunched)
        {
            activationTimer += Time.deltaTime;
            if (activationTime <= activationTimer)
            {
                isActive = true;
            }
            if (isActive)
            {
                if (!lostLock)
                {
                    ActiveMissileUpdate();
                }
            }
            if (activationTimer > aliveTime)
            {
                //KILL MISSILE
            }
            
        } 
    }

    private void ActiveMissileUpdate()
    {
        float distanceToTarget = (target.position - transform.position).magnitude;
        Debug.Log(distanceToTarget);
        if (distanceToTarget <= distanceFuze)
        {
            Debug.Log("Distance Fuze Activated");
            Destroy(gameObject);
        }
    }

    public override void Launch(Vector3 inSpeed)
    {
        transform.parent = null;
        myRigidBody.isKinematic = false;
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
}
