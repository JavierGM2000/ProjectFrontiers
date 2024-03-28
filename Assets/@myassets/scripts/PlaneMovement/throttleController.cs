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
    public Transform startReference, endReference;
    public Transform handTransform;
    public Vector3 closestPointToHand;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (!isSelected)
            return;
        Vector3 puntoMasCercano = CalcularPuntoMasCercano(startReference.position, endReference.position, handTransform.position);
        float porcentaje = (Vector3.Distance(startReference.position, puntoMasCercano) / Vector3.Distance(startReference.position, endReference.position)) * 100f;
        float targetAngle = (porcentaje - maxAngle);
        Debug.DrawLine(handTransform.position, puntoMasCercano, Color.red);
        Debug.Log(leverObject.forward.y);
        Debug.Log("Angle: " + targetAngle);


        Quaternion newRotation = Quaternion.Euler(targetAngle, 0f, 0f);
        if (leverObject.localRotation.x < 0)
        {
            new Quaternion(-newRotation.x, newRotation.y, newRotation.z, newRotation.w);
        }
        leverObject.localRotation = newRotation;
        //limitAngle(targetAngle);
        angle = leverObject.localRotation.eulerAngles.x;
    }

    public void toggleSelected(bool _selected)
    {
        isSelected = _selected;
    }

    public float getThrottle()
    {
        
        return angle;
        
    }

    public void limitAngle(float targetAngle) {

        if (leverObject.forward.y < -0.7f)
        {
            targetAngle = maxAngle;
        }
        else if (leverObject.forward.y > 0.7f)
        {
            targetAngle = minAngle;
        }

        

        Quaternion newRotation = Quaternion.Euler(targetAngle, 0f, 0f);
        if (leverObject.localRotation.x < 0)
        {
            new Quaternion(-newRotation.x, newRotation.y, newRotation.z, newRotation.w);
        }
        leverObject.localRotation = newRotation;

    }
    Vector3 CalcularPuntoMasCercano(Vector3 lineaPunto1, Vector3 lineaPunto2, Vector3 objetoPos)
    {
        // Vector que representa la dirección de la línea
        Vector3 direccionLinea = (lineaPunto2 - lineaPunto1).normalized;

        // Vector que va desde el punto inicial de la línea hasta la posición del objeto
        Vector3 desdePunto1 = objetoPos - lineaPunto1;

        // Proyección de 'desdePunto1' en la dirección de la línea
        float distancia = Vector3.Dot(desdePunto1, direccionLinea);

        // Calcula el punto más cercano en la línea
        Vector3 puntoMasCercano = lineaPunto1 + direccionLinea * distancia;

        // Si el punto más cercano está más allá de los extremos de la línea, ajusta la distancia
        if (distancia < 0)
        {
            puntoMasCercano = lineaPunto1;
        }
        else if (distancia > Vector3.Distance(lineaPunto1, lineaPunto2))
        {
            puntoMasCercano = lineaPunto2;
        }
        closestPointToHand = puntoMasCercano;
        return puntoMasCercano;
    }

}
