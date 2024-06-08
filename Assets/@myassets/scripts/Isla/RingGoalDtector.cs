using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGoalDtector : MonoBehaviour
{
    public GoalSpawnControll spawner;
    public RaceGoalScore scoring;

    public AudioSource SoundSource;
    public AudioClip GoalReachSource;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<GoalSpawnControll>();
        scoring = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RaceGoalScore>();
        SoundSource = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Host") //crear tags para host y client
        {
            scoring.hostScore++; 
            SoundSource.PlayOneShot(GoalReachSource);
            spawner.SpawnAtRandom();
            Destroy(gameObject);
        }
        
        else if(other.tag == "Client")
        {
            scoring.clientScore++;
            SoundSource.PlayOneShot(GoalReachSource);
            spawner.SpawnAtRandom();
            Destroy(gameObject);
        }

    }
}
