using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGoalDtector : MonoBehaviour
{
    public GoalSpawnControll spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<GoalSpawnControll>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        spawner.SpawnAtRandom();
        Destroy(gameObject);
    }
}
