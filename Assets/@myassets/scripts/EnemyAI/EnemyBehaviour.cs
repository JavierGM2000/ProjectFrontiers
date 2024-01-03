using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //BEHAVIOUR  VARIABLES//
    public int agresivityLevel; //  0 -> Not tracking player / 5 -> directly attacking player  (More agressive -> more stamina lose)
    public float maxStamina = 100f;
    public float currentStamina; // Determines turn and speeding capability of enemy
    public float staminaLoseCoeficient;

    public float maxStress = 100f;
    public float currentStress;
    public float stressGainCoeficient;

    public float approachDistance = 500;
    public float trackDistance = 100;
    public float directAttackDistance = 25;













    public GridManager grid;
    public Vector3 currentGridPosition;
    
    private List<GridMarkerBehaviour> path = new List<GridMarkerBehaviour>();
    private int nextPoint = 0;
    float counter;
    [SerializeField]
    float pathRefreshCooldown = 10f;
    public Vector3 targetGridPosition = new Vector3(9, 7, 4);
    public GameObject target;
    private Rigidbody targetRigidbody;


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
        targetRigidbody = target.GetComponent<Rigidbody>();
        rigidbody = GetComponent<Rigidbody>();   
        counter = 0;
        currentGridPosition = transform.position;
        agresivityLevel = 3;

    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetGridPosition) > 100)
        {
            agresivityLevel = 2;

        }
        else if (Vector3.Distance(transform.position, targetGridPosition) < 100 && Vector3.Distance(transform.position, targetGridPosition) > 30)
        {
            agresivityLevel = 3;

        }
        else {
            agresivityLevel = 4;
        }
        currentGridPosition = transform.position;
        //Debug.LogError("counter = "+ counter);
        counter += Time.deltaTime;
        if (counter > pathRefreshCooldown + 2)
            counter = pathRefreshCooldown;

        playBehaviour();

        
    }

    public void CalculatePath(Vector3 finalGridPosition)
    {
        
        path = grid.FindPath(currentGridPosition, finalGridPosition);
    }

    public void MoveEnemyPath()
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


    public void BehaviourPatrolling() { 
    
    
    }
    public void BehaviourApproachPlayer()
    {   
       
        targetGridPosition = target.transform.position;
        Debug.DrawLine(transform.position, targetGridPosition, Color.yellow);
        adjustRoll(targetGridPosition);
        adjustPitch(targetGridPosition);
        moveForward();
        

    }
    public void BehaviourTrackPlayer()
    {
        if (counter >= pathRefreshCooldown)
        {
            targetGridPosition = target.transform.position;
            CalculatePath(targetGridPosition);
            counter = 0;
        }

        MoveEnemyPath();

    }
    public void BehaviourDirectAttackPlayer()
    {
        targetGridPosition = target.transform.position + (targetRigidbody.velocity);
        Debug.DrawLine(transform.position, targetGridPosition, Color.red);
        adjustRoll(targetGridPosition);
        adjustPitch(targetGridPosition);
        moveForward();

    }
    public void BehaviourRunAway()
    {


    }




    public void playBehaviour() {

        switch (agresivityLevel)
        {
            case 1:
                BehaviourPatrolling();
                break;
            case 2:
                BehaviourApproachPlayer();
                break;
            case 3:
                BehaviourTrackPlayer();
                break;
            case 4:
                BehaviourDirectAttackPlayer();
                break;
            case 5:
                BehaviourRunAway();
                break;
            default:
                BehaviourPatrolling();////
                break;
        }

    }

    public void switchBehaviour() {

        if (currentStress <= 0 ) {
            currentStress = 0;
            if(agresivityLevel != 5)
            agresivityLevel = 5;
            return;
        }
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance <= approachDistance && distance > trackDistance)
        {
            if (agresivityLevel != 2)
                agresivityLevel = 2;
            return;
        }
        else if (distance <= trackDistance && distance > directAttackDistance)
        {
            if (agresivityLevel != 3)
                agresivityLevel = 3;
            return;
        }
        else if (distance <= directAttackDistance)
        {
            if (agresivityLevel != 4)
                agresivityLevel = 4;
            return;
        }
        else {
            if (agresivityLevel != 1)
                agresivityLevel = 1;
            return;


        }

    }


}
