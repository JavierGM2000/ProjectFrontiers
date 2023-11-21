using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// My name is Jeremy Donaldson. If you can... somehow.. and I really envy you if you can... have a peaceful night.
// Jeremy
public class WeaponStandardMissile : Weapon
{

    bool isLaunched;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Launch(Vector3 inSpeed)
    {
        throw new System.NotImplementedException();
    }

    public override LockType ReturnLockType()
    {
        return LockType.ALL;
    }
}
