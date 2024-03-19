using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] public Transform leverTransform;
    [SerializeField] public float maxAngle;
    [SerializeField] public float minAngle;
    public float currentAngle;
    public Vector3 rotationObjective;

    // Start is called before the first frame update
    void Start()
    {
        rotationObjective = new Vector3(leverTransform.localRotation.eulerAngles.x, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = leverTransform.localRotation.eulerAngles.x;
        if (currentAngle > maxAngle)
        {
            currentAngle = maxAngle;
        }
        else if(currentAngle < minAngle)
        {
            currentAngle = minAngle;
        }
        rotationObjective.x = currentAngle;
        leverTransform.localRotation = Quaternion.Euler(rotationObjective);
    }
}
