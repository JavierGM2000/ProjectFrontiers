using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatPlaneSystem : MonoBehaviour
{
    private float maxLife;

    public AudioSource SoundSource;
    public AudioClip damageSource;
    public AudioClip explosionSource;

    public GameObject[] weakPoints;
    public Transform glowParticles;
    public int weakPointCounter;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = 10;
        SoundSource = this.GetComponent<AudioSource>();
        this.GetComponent<BoxCollider>().enabled = false;
        glowParticles = this.transform.Find("Glow");
        weakPointCounter = weakPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        weakPointsCounter();
    }

    private void weakPointsCounter()
    {
        if(weakPointCounter<=0)
        {
            this.GetComponent<BoxCollider>().enabled = true;
            glowParticles.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Misil")
        {
            maxLife--;
            SoundSource.PlayOneShot(damageSource);
        }

        if ((other.tag == "Player" || other.tag == "Misil") && maxLife <= 0)
        {
            SoundSource.PlayOneShot(explosionSource);
            //destruir el avión o pasar a la siguiente escena
        }
    }
}
