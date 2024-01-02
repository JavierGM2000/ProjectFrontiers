using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GridManager grid;
    public Vector3 currentGridPosition;
    public float movementSpeed = 1f;
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
        path = grid.FindPath(currentGridPosition, finalGridPosition);
    }

    public void MoveEnemy()
    {
        if (path == null || path.Count == 0 || nextPoint >= path.Count)
            return;


        Vector3 targetPosition = path[nextPoint].transform.position;
        adjustPitch(targetPosition);
        // Mueve al enemigo hacia el siguiente punto del camino
       // Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
       // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.5f * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        // Verifica si el enemigo ha llegado al siguiente punto del camino
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Avanza al siguiente punto del camino
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
        float appliedTurnForce = 100 * angle / 180;
        rigidbody.AddTorque(transform.right * appliedTurnForce * responseModifier * Time.deltaTime/300 );
        rigidbody.AddForce(transform.forward * 5f , ForceMode.Acceleration);
    }

    public void adjustRoll(Vector3 targetPosition) { 
    
    
    }

}
