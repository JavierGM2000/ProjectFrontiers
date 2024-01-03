using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GridManager grid;
    public Vector3 currentGridPosition;
    
    private List<GridMarkerBehaviour> path = new List<GridMarkerBehaviour>();
    private int nextPoint = 0;
    float counter;
    [SerializeField]
    float pathRefreshCooldown = 10f;
    public Vector3 targetGridPosition = new Vector3(9, 7, 4);
    public GameObject target;

    public Rigidbody rigidbody;
    public float turnForce = 5;
    public float responsiveness = 10f;

    public float maxRollForce = 1f;
    public float maxPitchForce = 1f;
    public float movementForce = 4f;

    private float responseModifier
    {
        get
        {
            return (rigidbody.mass / 10f) * responsiveness;
        }

    }
    void Start()
    {
        targetGridPosition = target.transform.position;
        rigidbody = GetComponent<Rigidbody>();   
        counter = 0;
        currentGridPosition = transform.position;
        CalculatePath(targetGridPosition);
    }

    void Update()
    {
        
        currentGridPosition = transform.position;
        Debug.LogError("counter = "+ counter);
        counter += Time.deltaTime;
        if (counter >= pathRefreshCooldown) { 
            targetGridPosition = target.transform.position;
            CalculatePath(targetGridPosition);
            counter = 0;
        }
        
        MoveEnemy();
    }

    public void CalculatePath(Vector3 finalGridPosition)
    {
        //ClearPath();
        path = grid.FindPath(currentGridPosition, finalGridPosition);
    }

    public void MoveEnemy()
    {
        if (path == null || path.Count == 0 || nextPoint >= path.Count)
            return;


        Vector3 targetPosition = path[nextPoint].transform.position;


        adjustRoll(targetPosition);
        adjustPitch(targetPosition);
        moveForward();
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            nextPoint++;

        }

        Debug.DrawLine(transform.position, path[0].transform.position, Color.blue);
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i].transform.position, path[i + 1].transform.position, Color.blue);
        }
    }

    public void SetStartGridPosition(Vector3 position)
    {
        currentGridPosition = position;
    }

    public void adjustPitch(Vector3 targetPosition) {
        Vector3 movementVector = targetPosition - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, movementVector, transform.right);
        
        Debug.Log("angle = " + angle);
        float appliedTurnForce =  maxPitchForce* 100 * angle / 180;
        rigidbody.AddTorque(transform.right * appliedTurnForce * responseModifier * Time.deltaTime/300 );
       
    }

    public void adjustRoll(Vector3 targetPosition) {
       
        Vector3 targetDirection = targetPosition - transform.position;

        
        Vector3 horizontalDirection = Vector3.ProjectOnPlane(targetDirection, transform.up).normalized;

      
        float rollAngle = Vector3.SignedAngle(transform.forward, horizontalDirection, transform.up);

       
        float rollForce = maxRollForce * rollAngle / 180f; 
        rigidbody.AddTorque(transform.forward * -rollForce * responseModifier * Time.deltaTime);

    }
    public void moveForward()
    {
        rigidbody.AddForce(transform.forward * movementForce * Time.deltaTime, ForceMode.Acceleration);
    }


    private void ClearPath()
    {
        // Filtra y destruye solo los GridMarkers temporales
        List<GridMarkerBehaviour> tempMarkers = path.FindAll(marker => marker.isTemporal);

        foreach (GridMarkerBehaviour marker in tempMarkers)
        {
            Destroy(marker.gameObject);
        }

        // Elimina los GridMarkers temporales de la lista path
        path.RemoveAll(marker => marker.isTemporal);

        // Reinicia el índice nextPoint
        nextPoint = 0;
    }
}
