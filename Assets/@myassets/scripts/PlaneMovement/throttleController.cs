using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throttleController : MonoBehaviour
{
    public bool isSelected;
    public float maxAngle, minAngle;
    public float angle;
    public Transform leverObject;
    public Vector3 newTransform;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = true;
        
    }

    

    // Update is called once per frame
    void Update()
    {
        Debug.Log(leverObject.forward.y);
        Debug.Log("Angle: " + getThrottle());
        limitAngle();
    }

    public void toggleSelected()
    {
        isSelected = !isSelected;
    }

    public float getThrottle()
    {
        angle = leverObject.localRotation.eulerAngles.x;
        return angle;
        
    }

    public void limitAngle() {

        if (leverObject.forward.y < -0.7f)
        {
            angle = maxAngle;
        }
        else if (leverObject.forward.y > 0.7f)
        {
            angle = minAngle;
        }



        //if (angle > maxAngle && angle < 350) {

        //    angle = maxAngle;

        //} else if (angle < minAngle) {
        //    angle = minAngle;
        //}
        

        Quaternion newRotation = Quaternion.Euler(angle, 0f, 0f);
        if (leverObject.localRotation.x < 0)
        {
            new Quaternion(-newRotation.x, newRotation.y, newRotation.z, newRotation.w);
        }

        leverObject.localRotation = newRotation;

    }


}
