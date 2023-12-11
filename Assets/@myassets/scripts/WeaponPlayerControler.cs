using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You f*cked up my face
// - Mitchell "Hunt Down the Freeman Freeman" Shephard
public class WeaponPlayerControler : MonoBehaviour
{
    [SerializeField]
    private MainGun maGun;
    [SerializeField]
    private float gunAmmo;

    private GameObject target;

    private bool standardSelected = true;

    [SerializeField]
    private float gunPredictDistance = 1500;

    [SerializeField]
    private float lockTime;
    [SerializeField]
    private float lockdistance;
    private bool locked = false;


    [SerializeField]
    private int standardAmmo;
    [SerializeField]
    private float reloadTimeStandard;
    private int standardCounter = 0;
    [SerializeField]
    private Hardpoint[] standardHardpoints;


    [SerializeField]
    private int specialAmmo;
    [SerializeField]
    private float reloadTimeSpecial;
    private int specialCounter = 0;
    [SerializeField]
    private Hardpoint[] specialHardpoints;

    private Weapon.LockType selectedLockType;
    private float selectedlockDistance;
    private float selectedLockAngle;

    // Start is called before the first frame update
    void Start()
    {
        int ammoPerHardpoint = standardAmmo / standardHardpoints.Length;
        foreach(Hardpoint hardpoint in standardHardpoints)
        {
            hardpoint.ammo = ammoPerHardpoint;
            hardpoint.reloadTime = reloadTimeStandard;
        }

        int ammoSpecPerHardpoint = specialAmmo / specialHardpoints.Length;
        foreach (Hardpoint hardpoint in specialHardpoints)
        {
            hardpoint.ammo = ammoPerHardpoint;
            hardpoint.reloadTime = reloadTimeStandard;
        }
        upldateSelected();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if(selectedLockType != Weapon.LockType.GROUND_UNGUIDED)
            {
                if (distance <= selectedlockDistance)
                {
                    if (Vector3.Angle(transform.forward, (target.transform.position - transform.position).normalized) >= selectedLockAngle)
                    {
                        locked = true;
                    } else
                    {
                        locked = false;
                    }
                }
                else
                {
                    locked = false;
                }
            }
            if (distance < gunPredictDistance)
            {
                float speed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                Vector3 leadPos;
                maGun.getBulletImpactPoint(out leadPos, speed);
            }
        }
    }

    public void launchWeapon()
    {
        if(selectedLockType!=Weapon.LockType.GROUND_UNGUIDED && !locked)
        {
            return;// If the weapon needs to be locked but it is not we won't let it fire
        }
        Vector3 speed = gameObject.GetComponent<Rigidbody>().velocity;
        if (standardSelected)
        {
            if(standardAmmo>0 && standardHardpoints[standardCounter].launchWeapons(speed))
            {
                standardAmmo--;
                if (++standardCounter >= standardHardpoints.Length)
                    standardCounter = 0;
            }
        }
        else
        {
            if (specialAmmo > 0 && specialHardpoints[specialCounter].launchWeapons(speed))
            {
                specialAmmo--;
                if (++specialCounter >= specialHardpoints.Length)
                    specialCounter = 0;
            }
        }
    }

    private void upldateSelected()
    {
        Weapon selectedWeapon;
        if (standardSelected)
        {
            selectedWeapon = standardHardpoints[0].weaponBase.GetComponent<Weapon>();
        }
        else
        {
            selectedWeapon = specialHardpoints[0].weaponBase.GetComponent<Weapon>();
        }
        selectedLockType = selectedWeapon.getLockType();
        selectedlockDistance = selectedWeapon.getLockDistance();
        selectedLockAngle = selectedWeapon.getLockAngle();
    }

    public void findNewTarget()
    {
        locked = false;
        Vector3 scanOrigin;
        if (standardSelected)
        {
            scanOrigin = standardHardpoints[standardCounter].transform.position;
        }
        else
        {
            scanOrigin = specialHardpoints[specialCounter].transform.position;
        }


        int i = 1;
        RaycastHit closestEnemy;
        bool hasFound;
        do
        {
            Debug.Log(LayerMask.GetMask("Enemy"));
            //hasFound = Physics.CapsuleCast(gunpod.transform.position, gunpod.transform.position + transform.forward * 200, 10, transform.forward, out closestEnemy,Mathf.Infinity);
            hasFound = Physics.SphereCast(scanOrigin, i * 10, transform.forward, out closestEnemy, 1000, LayerMask.GetMask("Enemy"));
            //hasFound = Physics.Raycast(gunpod.transform.position, gunpod.transform.position + transform.forward * 200, out closestEnemy, Mathf.Infinity);
            //Debug.DrawLine(gunpod.transform.position, gunpod.transform.position + transform.forward * 200, Color.blue, 2f);
            i++;
        } while (hasFound == false && i < 5);
        if (hasFound == true)
        {
            target = closestEnemy.transform.gameObject;
        }

    }

    public bool switchWeapon()
    {
        if(standardSelected)
        {
            if (specialAmmo <= 0)
                return false;

            standardSelected = false;
        }else
        {
            standardSelected = true;
        }
        upldateSelected();
        return true;
    }


}
