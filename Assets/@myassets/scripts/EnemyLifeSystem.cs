using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeSystem : MonoBehaviour
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
    void Update()
    {
        if (currentLife <= (maxLife / 3) && currentLife>=0)
        {
            smokeParticles.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Player")
        {
            currentLife--;
        }*/

        if (other.tag == "Player") //tag de prueba, sería el del proyectil
        {
            currentLife--;
            SoundSource.PlayOneShot(damageSource);
        }

        if (other.tag == "Player" && currentLife <= 0) //tag de prueba, sería el del proyectil
        {
            SoundSource.PlayOneShot(explosionSource);
            explosionParticles.gameObject.SetActive(true);
            enemyModel.gameObject.SetActive(false);

            Destroy(gameObject, explosionSource.length);
        }
    }
}
