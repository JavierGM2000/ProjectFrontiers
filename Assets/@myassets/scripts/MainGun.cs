using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// What do you mean overcharg-
// Black Mesa Security
public class MainGun : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private Transform targetTransform;
    private Rigidbody targetRigidBody;
    [SerializeField]
    private Rigidbody planeRigidbody;
    
    [SerializeField]
    private Transform planeGunPodTrans;

    // Start is called before the first frame update
    void Start()
    {
        planeGunPodTrans = transform;
    }


    public float getBulletImpactPoint(out Vector3 lagvector, float planeSpeed)
    {
        Vector3 fromTargetToPlane = targetTransform.position - planeGunPodTrans.transform.position;
        Vector3 targetDirection = targetRigidBody.velocity.normalized;
        float angle = Vector3.Angle(fromTargetToPlane, targetDirection);
        angle = angle * Mathf.Deg2Rad;
        float targetVelocity = targetRigidBody.velocity.magnitude;
        float bulletVelocity = planeRigidbody.velocity.magnitude + planeSpeed;
        float distance = fromTargetToPlane.magnitude;
        float squareRoot = Mathf.Sqrt((distance * distance) * (((targetVelocity * targetVelocity) * Mathf.Pow(Mathf.Cos(angle), 2)) - (targetVelocity * targetVelocity) + (bulletVelocity * bulletVelocity)));
        float bulletTime = (squareRoot + distance * targetVelocity * Mathf.Cos(angle)) / ((bulletVelocity * bulletVelocity) - (targetVelocity * targetVelocity));
        //Debug.Log(angle);
        lagvector = targetDirection * bulletTime * targetVelocity;
        Vector3 indicator = targetTransform.position + lagvector;
        Debug.DrawLine(targetTransform.position, indicator, Color.red);
        return distance;
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target = newTarget;
        targetTransform = newTarget.GetComponent<Transform>();
        targetRigidBody = newTarget.GetComponent<Rigidbody>();
    }
}
