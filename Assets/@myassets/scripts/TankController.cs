using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public GameObject torreta;
    
    private GameObject player;
    private bool isInRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange==true)
            lookAtPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            isInRange = true;
            print("there");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
            print("gone");
        }
    }

    private void lookAtPlayer()
    {
        //torreta.transform.LookAt(player.transform.position); //en todos los sentidos
        //en uno específico
        /*Vector3 difference = player.transform.position - torreta.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        torreta.transform.rotation = Quaternion.Euler(0.0f, rotationZ, 0.0f);*/
        Vector3 targetPostition = new Vector3(player.transform.position.x,
                                       torreta.transform.position.y,
                                       player.transform.position.z);
        torreta.transform.LookAt(targetPostition);
    }
}
