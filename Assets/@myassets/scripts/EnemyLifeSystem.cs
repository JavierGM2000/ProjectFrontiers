using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeSystem : Life
{
    private float maxLife;
    public float currentLife;

    private Transform smokeParticles;
    private Transform explosionParticles;
    private Transform enemyModel;

    public AudioSource SoundSource;
    public AudioClip damageSource;
    public AudioClip explosionSource;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = 100;
        currentLife = maxLife;
        SoundSource = this.GetComponent<AudioSource>();
        smokeParticles = this.transform.Find("Smoke");
        explosionParticles = this.transform.Find("Explosion");
        enemyModel = this.transform.Find("naveEnemigo");
    }

    // Update is called once per frame
    private void checkSmoke()
    {
        if (currentLife <= (maxLife / 2) && currentLife>=0)
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
        currentLife-= damage;
        SoundSource.PlayOneShot(damageSource);
        checkSmoke();
        if (currentLife <= 0)
        {
            SoundSource.PlayOneShot(explosionSource);
            explosionParticles.gameObject.SetActive(true);
            enemyModel.gameObject.SetActive(false);
            Destroy(gameObject, explosionSource.length);
        }
    }
}
