using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSpawnControll : MonoBehaviour
{
    public GameObject goal;
    public Transform[] spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAtRandom();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void SpawnAtRandom()
    {
        GameObject a = Instantiate(goal) as GameObject;
        int randomSpawnSpot = Random.Range(0, spawnLocations.Length);
        
        a.transform.position = new Vector3(spawnLocations[randomSpawnSpot].transform.position.x,
                                           spawnLocations[randomSpawnSpot].transform.position.y,
                                           spawnLocations[randomSpawnSpot].transform.position.z);

        a.transform.rotation = spawnLocations[randomSpawnSpot].transform.rotation;
    }
}
