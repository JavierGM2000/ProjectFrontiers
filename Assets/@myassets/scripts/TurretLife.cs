using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLife : Life
{
    public float maxLife;
    private float currentLife;

    [SerializeField]
    private Transform smokeParticles;
    [SerializeField]
    private Transform explosionParticles;

    public AudioSource SoundSource;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        SoundSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void checkSmoke()
    {
        if (currentLife <= (maxLife / 2) && currentLife >= 0)
        {
            smokeParticles.gameObject.SetActive(true);
        }
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
        currentLife -= damage;
        checkSmoke();
        if (currentLife <= 0)
        {
            Destroy(gameObject,0.5f);
            explosionParticles.gameObject.SetActive(true);
            
        }
    }
}
