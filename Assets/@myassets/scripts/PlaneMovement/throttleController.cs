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
        angle = angle % 360; // Asegura que el �ngulo est� en el rango -360 a 360

        if (angle < 0)
        {
            angle += 360; // Convierte los �ngulos negativos a su equivalente positivo
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

            // Calcula el �ngulo entre la direcci�n y el eje Z positivo
            float angle = Vector3.SignedAngle(Vector3.forward, directionToTarget, Vector3.up);

            // Aseg�rate de que el �ngulo est� en el rango [0, 360)
            angle = NormalizeAngle(angle);

            // Calcula la rotaci�n relativa entre el avi�n y la palanca
            Quaternion relativeRotation = Quaternion.Inverse(plane.transform.rotation) * handle.transform.rotation;

            // Calcula el �ngulo directamente desde la rotaci�n en el eje X local
            float xRotation = handle.transform.localRotation.eulerAngles.x;

            // Aplica la rotaci�n al objeto en el eje local X teniendo en cuenta la rotaci�n del avi�n
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
