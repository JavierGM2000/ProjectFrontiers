using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I-I didn't mean to hurt anybody - Walker
// No one ever does, Walker. THREE. - Walker
public class WeaponBomb : Weapon
{
    [SerializeField]
    float explosionRad = 30f;
    [SerializeField]
    float damage = 100f;
    [SerializeField]
    float launchSpeed = 1;
    [SerializeField]
    GameObject testThing;
    bool bombDropped = false;


    Rigidbody myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetBombImpactPoint(0);
    }

    public override void Launch(Vector3 inSpeed)
    {
        bombDropped = false;
        transform.parent = null;
        myRigidBody.isKinematic = false;
        myRigidBody.velocity = transform.forward * (launchSpeed + inSpeed.magnitude);
    }

    public Vector3 GetBombImpactPoint(float currentSpeed)
    {
        float step = 0.5f; //In seconds
        Vector3 speed = transform.forward * (launchSpeed + currentSpeed);
        Vector3 position = transform.position;
        Vector3 impactPoint = new Vector3(999,999,999);
        RaycastHit hit;

        for (float i = 0; i < 30; i += step)
        {
            if(Physics.Raycast(position,speed.normalized,out hit, speed.magnitude * step, 1 << LayerMask.NameToLayer("Ground")))
            {
                impactPoint = hit.point;
                break;
            }
            else
            {
                position += step * speed;
                speed += Physics.gravity;
            }
        }
        testThing.transform.position = impactPoint;
        return impactPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ground" || other.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    public override LockType getLockType()
    {
        return LockType.GROUND_UNGUIDED;
    }
}
