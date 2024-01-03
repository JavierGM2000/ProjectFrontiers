using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMarkerBehaviour : MonoBehaviour
{
    public Material navigableMaterial;
    public Material notNavigableMaterial;

    public bool isNavigable;
    public bool isTemporal;
    public float counter;
    public int x;
    public int y;
    public int z;

    //A* VARIABLES
    public float gCost;
    public float hCost;
    public float fCost { get { return gCost + hCost; } }

    public GridMarkerBehaviour parentNode;



    private Renderer renderer;
    void Start()
    {
        isTemporal = false;
        gCost = 0;
        hCost = 0;
        isNavigable = true;
        renderer = GetComponent<Renderer>();
        if (renderer != null) { 
            renderer.material = navigableMaterial;
        }
        
    }

    public void setGridPosition(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void CalculateHCost(Vector3 targetPosition)
    {
        hCost = Vector3.Distance(transform.position, targetPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            return;
        if (isNavigable)
        {
            isNavigable = false;
            if (renderer != null)
            {
                renderer.material = notNavigableMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isNavigable)
        {
            isNavigable = true;
            if (renderer != null)
            {
                renderer.material = navigableMaterial;
            }
        }
    }

    public Vector3 GetGridPosition()
    {
        return new Vector3(x, y, z);
    }
}
