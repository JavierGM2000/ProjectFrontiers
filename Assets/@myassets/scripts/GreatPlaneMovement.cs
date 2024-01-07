using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatPlaneMovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveForwards();
    }

    private void moveForwards()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
