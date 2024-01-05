using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisilSystem : MonoBehaviour
{
    private float timer;
    private float counter = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= timer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Destroy(gameObject);
    }
}
