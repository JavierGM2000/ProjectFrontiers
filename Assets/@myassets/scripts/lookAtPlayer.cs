using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement();
    }


    private void enemyMovement()
    {
        Quaternion playerRotation = Quaternion.LookRotation(playerPos.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, 2 * Time.deltaTime);
    }

}
