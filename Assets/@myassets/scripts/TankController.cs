using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public GameObject torreta;
    
    private GameObject player;
    private bool isInRange;

    private float counter;
    public float coolDown = 0.0f;

    public GameObject spawnPoint;
    public GameObject misil;
    public float launchSpeed = 750f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isInRange = false;
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange == true)
        {
            lookAtPlayer();
            counter += Time.deltaTime;
            shootAtPlayer();
        }
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
            counter = 0.0f;
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

    private void shootAtPlayer()
    {
        if (counter >= coolDown)
        {
            GameObject launchedBullet = Instantiate(misil, spawnPoint.transform.position, spawnPoint.transform.rotation);
            launchedBullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchSpeed));
            counter = 0.0f;
        }
    }
}
