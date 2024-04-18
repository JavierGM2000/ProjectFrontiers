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

    public Transform explosionParticles;


    void Start()
    {
        /*if (!explosionParticles)
        {
            explosionParticles = this.transform.Find("WFX_Explosion Small");
            explosionParticles.gameObject.SetActive(false);
        }*/
        
    }

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
                explosionParticles.gameObject.SetActive(true);
                Destroy(gameObject,2);
            }
        }
        else
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                other.gameObject.GetComponent<Life>().dealDamage(damage);
                explosionParticles.gameObject.SetActive(true);
                Destroy(gameObject,2);
            }
        }
    }
}
