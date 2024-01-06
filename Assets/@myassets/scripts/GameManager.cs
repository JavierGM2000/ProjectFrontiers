using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeLimit;
    public TextMeshProUGUI timeText;

    private bool isPlaying;

    private bool hasPlayed;
    public AudioSource SoundSource;
    public AudioClip timeLeftSource;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        SoundSource = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
            contadorMientrasJuegas();
    }

    private void contadorMientrasJuegas()
    {
        timeLimit -= Time.deltaTime;
        updateTimer(timeLimit);

        if (timeLimit < 60 && !hasPlayed)
        {
            SoundSource.PlayOneShot(timeLeftSource);
            timeText.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0f, 0f, 1f);
            hasPlayed = true;
        }

        if (timeLimit < 0)
        {
            print("WRYYYYYYYYYYYYYYYYYYYy");
            isPlaying = false;
        }
    }

    private void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minuto = Mathf.FloorToInt(currentTime / 60);
        float segundo = Mathf.FloorToInt(currentTime % 60);

        timeText.text = string.Format("{0:00} : {1:00}", minuto, segundo);
    }
}
