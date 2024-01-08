using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAfterTime : MonoBehaviour
{
    public float waitTime;
    LevelManager level;

    private void Start()
    {
        level = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            level.changeLevel("EscenaFinal");
        }
    }
}
