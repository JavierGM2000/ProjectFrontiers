using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryLifeSystem : MonoBehaviour
{
    private Transform glowParticles;
    private Transform explosionParticles;

    public int life;

    // Start is called before the first frame update
    void Start()
    {
        life = 5;

        glowParticles = this.transform.Find("Glow");
        explosionParticles = this.transform.Find("Smoke");
    }

    // Update is called once per frame
    void Update()
    {
        if(life<=0)
        {
            glowParticles.gameObject.SetActive(false);
            explosionParticles.gameObject.SetActive(true);

            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other) //está de prueba, cambiar lo necesario
    {
        if(other.tag=="proyectil")
        {
            life--;
        }
    }
}
