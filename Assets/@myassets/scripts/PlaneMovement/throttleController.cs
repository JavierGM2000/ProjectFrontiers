using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throttleController : MonoBehaviour
{
    public bool isSelected;
    public GameObject targetHand;
    public GameObject handle;
    public float angleThro;
    public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = true;
    }

    float NormalizeAngle(float angle)
    {
        angle = angle % 360; // Asegura que el ángulo esté en el rango -360 a 360

        if (angle < 0)
        {
            angle += 360; // Convierte los ángulos negativos a su equivalente positivo
        }

        return angle;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            Vector3 directionToTarget = targetHand.transform.position - handle.transform.position;
            directionToTarget.y = 0; // Proyecta en el plano XZ

            // Calcula el ángulo entre la dirección y el eje Z positivo
            float angle = Vector3.SignedAngle(Vector3.forward, directionToTarget, Vector3.up);

            // Asegúrate de que el ángulo esté en el rango [0, 360)
            angle = NormalizeAngle(angle);

            // Calcula la rotación relativa entre el avión y la palanca
            Quaternion relativeRotation = Quaternion.Inverse(plane.transform.rotation) * handle.transform.rotation;

            // Calcula el ángulo directamente desde la rotación en el eje X local
            float xRotation = handle.transform.localRotation.eulerAngles.x;

            // Aplica la rotación al objeto en el eje local X teniendo en cuenta la rotación del avión
            transform.localRotation = Quaternion.Euler(angle, 0, 0) * Quaternion.Euler(xRotation, 0, 0);

            Debug.Log(getThrottle());
        }
    }

    public void toggleSelected()
    {
        isSelected = !isSelected;
    }

    public float getThrottle()
    {
        float normalizedAngle = NormalizeAngle(handle.transform.localRotation.eulerAngles.x);
        return normalizedAngle;
    }

    public void setTarget(GameObject target)
    {
        targetHand = target;
    }
}
