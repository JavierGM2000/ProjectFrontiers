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

    // Start is called before the first frame update
    void Start()
    {
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
        if(other.tag=="Enemy")
        {
            currentLife--;
        }
    }
}
