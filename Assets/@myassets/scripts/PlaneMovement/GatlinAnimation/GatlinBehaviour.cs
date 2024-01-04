using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlinBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject rotatingPlatform;
    [SerializeField]
    private List<Animator> barrelAnimations;
    
    [SerializeField]
    private float shootingCooldown = 0.6f;
    private float counter;
    private int currentBarrel;

    [SerializeField]
    private float rotationSpeed = -50f;



    private void Start()
    {
        currentBarrel = 0;
        counter = 1;
    }
    public  void rotatePlatForm() {
            
            rotatingPlatform.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        counter += Time.deltaTime;
        if (counter >= shootingCooldown) {
            barrelAnimations[currentBarrel].SetTrigger("Recoil");
            if (currentBarrel >= barrelAnimations.Count - 1) {
                currentBarrel = 0;
            }
            else
            {
                currentBarrel += 1;
            }
            counter = 0;
            
        }
    }

}
