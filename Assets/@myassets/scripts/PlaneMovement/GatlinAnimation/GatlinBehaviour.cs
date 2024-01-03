using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlinBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject rotatingPlatform;


    

    [SerializeField]
    private float rotationSpeed = 50f;

    public  void rotatePlatForm() {

            rotatingPlatform.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
    }

}
