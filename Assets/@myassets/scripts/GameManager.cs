using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeLimit;
    public TextMeshProUGUI timeText;

    private bool isPlaying;

    public AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        aSource.enabled = false;
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

        if (timeLimit < 9)
        {
            aSource.enabled = true;
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
