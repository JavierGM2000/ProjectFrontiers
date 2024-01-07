using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLifeSystem : MonoBehaviour
{
    private float maxLife;
    public float currentLife;
    public GameObject lifeLight;

    public TextMeshProUGUI lifeText;

    private bool hasPlayed;
    public AudioSource SoundSource;
    public AudioClip critDamageSource;

    LevelManager levelMag;

    // Start is called before the first frame update
    void Start()
    {
        levelMag = GameObject.FindObjectOfType<LevelManager>();
        maxLife = 100;
        currentLife = maxLife;
        SoundSource = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        lightEmision();
        lifeText.text = currentLife.ToString()+"%";
    }

    private void lightEmision()
    {
        if(currentLife==maxLife)
        {
            lifeLight.GetComponent<Light>().color = new Color(0f,1f,0f,1f);
            lifeText.GetComponent<TextMeshProUGUI>().color = new Color(0f, 1f, 0f, 1f);
            hasPlayed = false;
        }

        else if (currentLife <= (maxLife/2) && currentLife > (maxLife / 4))
        {
            lifeLight.GetComponent<Light>().color = new Color(1f, 1f, 0f, 1f);
            lifeText.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 0f, 1f);
            hasPlayed = false;
        }

        else if (currentLife <= (maxLife/4))
        {
            lifeLight.GetComponent<Light>().color = new Color(1f, 0f, 0f, 1f);
            lifeText.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0f, 0f, 1f);

            if (!hasPlayed)
            {
                SoundSource.PlayOneShot(critDamageSource);
                hasPlayed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy"||other.tag=="Misil")
        {
            currentLife--;
        }

        if ((other.tag == "Player" || other.tag == "Misil") && currentLife <= 0) //tag de prueba, serú} el del proyectil
        {
            levelMag.changeLevel("GameOver");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) //tag de prueba, serú} el del proyectil
        {
            levelMag.changeLevel("GameOver");
        }

        if (currentLife <= 0)
        {
            levelMag.changeLevel("GameOver");
        }
    }
}
