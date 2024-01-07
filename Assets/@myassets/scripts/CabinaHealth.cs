using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinaHealth : Life
{
    public float maxLife;
    private float currentLife;

    public bool canTakeDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
    }



    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Misil")
        {
            currentLife--;
            SoundSource.PlayOneShot(damageSource);
        }

        if ((other.tag == "Player" || other.tag == "Misil") && currentLife <= 0) //tag de prueba, serú} el del proyectil
        {
            SoundSource.PlayOneShot(explosionSource);
            explosionParticles.gameObject.SetActive(true);
            enemyModel.gameObject.SetActive(false);
            Destroy(gameObject, explosionSource.length);
        }
    }*/

    public override void dealDamage(int damage)
    {
        if (!canTakeDamage)
        {
            return;
        }
        currentLife -= damage;
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
