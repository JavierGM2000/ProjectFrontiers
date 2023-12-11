using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I know the truth is hard to hear, Walker, but it's time. You're all that's left, and we can't live this lie forever. I'm going to count to five, then I'm pulling the trigger. - Walker
// You're not real, this is all in my head... - Walker
// Are you sure? Maybe it's in mine... ONE. - Walker

public abstract class Weapon : MonoBehaviour
{
    public enum LockType { ALL, AIR, GROUND, GROUND_UNGUIDED };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Launch(Vector3 inSpeed);
    public abstract LockType getLockType();

    public virtual void SetTarget(Transform targetTrans)
    {

    }

    public virtual float getLockAngle()
    {
        return -1;
    }

    public virtual float getLockDistance()
    {
        return -1;
    }
}
