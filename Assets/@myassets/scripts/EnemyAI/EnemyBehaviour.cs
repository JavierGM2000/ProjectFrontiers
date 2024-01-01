using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GridManager grid;
    public Vector3 currentGridPosition;
    public float movementSpeed = 5f;
    private List<GridMarkerBehaviour> path = new List<GridMarkerBehaviour>();
    private int nextPoint = 0;
    float counter;
    [SerializeField]
    float pathRefreshCooldown = 10f;
    public Vector3 targetGridPosition = new Vector3(9, 7, 4);

    void Start()
    {
        counter = 0;
        currentGridPosition = transform.position;
        CalculatePath(targetGridPosition);
    }

    void Update()
    {
        Debug.LogError("counter = "+ counter);
        counter += Time.deltaTime;
        if (counter >= pathRefreshCooldown) { 
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
        if (path == null || path.Count == 0)
            return;

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
}
