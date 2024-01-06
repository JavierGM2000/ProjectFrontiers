using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIGHT FOR THE FRONT AND FREEDOM! MOVE OUT!
// - Strife
public class Hardpoint : MonoBehaviour
{
    public float reloadTime;
    public int ammo;
    private bool isLoading = false;
    [SerializeField]
    public GameObject weaponBase;
    [SerializeField]
    private GameObject spawnedWeapon;

    Weapon.LockType hardpointWeapon;
    float lockMaxAngle;

    // Start is called before the first frame update
    void Start()
    {
       Weapon wap =  weaponBase.GetComponent<Weapon>();
        hardpointWeapon = wap.getLockType();
        lockMaxAngle = wap.getLockAngle();
    }
    public void setTarget(Transform transform)
    {
        spawnedWeapon.GetComponent<Weapon>().SetTarget(transform);
    }

    public bool launchWeapons(Vector3 speed)
    {
        if (ammo == 0)
            return false;
        else if (isLoading == true)
            return false;

        spawnedWeapon.GetComponent<Weapon>().Launch(speed);
        spawnedWeapon = null;
        ammo--;
        isLoading = true;
        if (ammo > 0)
        {
            StartCoroutine(reload());
        }
        return true;
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(reloadTime);
        spawnedWeapon = Instantiate(weaponBase, transform);
        isLoading = false;
    }
}
