using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private bool hurtsPlayer = false;
    [SerializeField]
    private float lifetime = 5f;
    [SerializeField]
    private int damage = 2;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hurtsPlayer)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<Life>().dealDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                other.gameObject.GetComponent<Life>().dealDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
