using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyShow : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemySightPrefab;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class targetClass
{
    GameObject enemy;
    RadarInfo enRadInf;

    bool isSelected = false;
}
