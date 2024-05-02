using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// My name is Jeremy Donaldson. If you can... somehow.. and I really envy you if you can... have a peaceful night.
// Jeremy
public class WeaponStandardMissileAI : Weapon
{
    [SerializeField]
    MissileSettings missileSettingAI;
    [SerializeField]
    MissileAgent missileAgent;
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
    float distanceFuze = 5.0f;

    private Transform target;

    private AudioSource launchSound;

    private GameObject smoke;

    Rigidbody myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        launchSound = GetComponent<AudioSource>();
        smoke = transform.Find("smoke").gameObject;
        aliveTime += activationTime;
        myRigidBody = GetComponent<Rigidbody>();
        /*if(target)
            targetRigidbody = target.GetComponent<Rigidbody>();*/ // DEBUG ONLY REMOVE LATER
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
        isActive = true;
        missileAgent.enabled = true;
    }
    
    private void ActiveMissileUpdate()
    {
        float distanceToTarget = (target.position - transform.position).magnitude;
        if (distanceToTarget <= distanceFuze)
        {
            Debug.Log("Distance Fuze Activated");
            target.GetComponent<Life>().dealDamage(damage);
            Destroy(gameObject);
        }
        if (Vector3.Angle(transform.forward, (target.position - transform.position).normalized) >= lockAngle)
        {
            Debug.Log("Lost lock");
            lostLock = true;
            missileSettingAI.enabled = false;
            return;
        }
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
    }

    public override LockType getLockType()
    {
        return LockType.ALL;
    }

    public override void SetTarget(Transform targetTrans)
    {
        target = targetTrans;
        missileSettingAI.target = targetTrans;
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
        if (isLaunched  && targetWarning && !lostLock)
        {
            targetWarning.removeMissiles(this.gameObject.GetInstanceID());

        }
    }
}
