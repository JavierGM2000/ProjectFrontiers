using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int gridSizeX = 10;
    [SerializeField]
    private int gridSizeY = 10;
    [SerializeField]
    private int gridSizeZ = 10;
    [SerializeField]
    public int cellSize = 1;
    [SerializeField]
    private GameObject gridMarkerObject;

    private GridMarkerBehaviour[,,] grid;

    void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        Vector3 markerPosition = new Vector3();
        grid = new GridMarkerBehaviour[gridSizeX, gridSizeY, gridSizeZ];

        for (int line = 0; line < gridSizeY; line++)
        {
            for (int column = 0; column < gridSizeX; column++)
            {
                for (int depth = 0; depth < gridSizeZ; depth++)
                {
                    markerPosition = new Vector3(column * cellSize, line * cellSize, depth * cellSize);
                    GameObject marker = Instantiate(gridMarkerObject, markerPosition, Quaternion.identity);
                    GridMarkerBehaviour markerBehaviour = marker.GetComponent<GridMarkerBehaviour>();
                    markerBehaviour.setGridPosition(column, line, depth);
                    grid[column, line, depth] = markerBehaviour;
                }
            }
        }
    }

    public GridMarkerBehaviour GetGridMarkerFromPosition(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / cellSize);
        int y = Mathf.RoundToInt(worldPosition.y / cellSize);
        int z = Mathf.RoundToInt(worldPosition.z / cellSize);

        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);
        z = Mathf.Clamp(z, 0, gridSizeZ - 1);

        return grid[x, y, z];
    }

    List<GridMarkerBehaviour> RetracePath(GridMarkerBehaviour startNode, GridMarkerBehaviour endNode)
    {
        List<GridMarkerBehaviour> path = new List<GridMarkerBehaviour>();
        GridMarkerBehaviour currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        path.Reverse();
        return path;
    }
    List<GridMarkerBehaviour> GetNeighbors(GridMarkerBehaviour currentNode)
    {
        List<GridMarkerBehaviour> neighbors = new List<GridMarkerBehaviour>();

        // Obtener las posiciones adyacentes en la cuadrícula
        int[] offsetX = { 1, -1, 0, 0, 0, 0 };
        int[] offsetY = { 0, 0, 1, -1, 0, 0 };
        int[] offsetZ = { 0, 0, 0, 0, 1, -1 };

        for (int i = 0; i < 6; i++)
        {
            int neighborX = currentNode.x + offsetX[i];
            int neighborY = currentNode.y + offsetY[i];
            int neighborZ = currentNode.z + offsetZ[i];

            // Comprobar si la posición vecina está dentro de los límites de la cuadrícula
            if (neighborX >= 0 && neighborX < gridSizeX &&
                neighborY >= 0 && neighborY < gridSizeY &&
                neighborZ >= 0 && neighborZ < gridSizeZ)
            {
                // Obtener el marcador de la cuadrícula en la posición vecina
                GridMarkerBehaviour neighbor = grid[neighborX, neighborY, neighborZ];

                // Agregar el vecino a la lista si es navegable
                if (neighbor.isNavigable)
                {
                    neighbors.Add(neighbor);
                }
            }
        }

        return neighbors;
    }




    public List<GridMarkerBehaviour> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        GridMarkerBehaviour startNode = GetGridMarkerFromPosition(startPos);
        GridMarkerBehaviour targetNode = GetGridMarkerFromPosition(targetPos);

        List<GridMarkerBehaviour> openSet = new List<GridMarkerBehaviour>();
        HashSet<GridMarkerBehaviour> closedSet = new HashSet<GridMarkerBehaviour>();


        startNode = startNode;
        targetNode = targetNode;
        openSet.Add(startNode);
        openSet = openSet;
        while (openSet.Count > 0)
        {
            GridMarkerBehaviour currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {

                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (GridMarkerBehaviour neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.isNavigable || closedSet.Contains(neighbor))
                {
                    continue;
                }

                float newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parentNode = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null; // No se encontró un camino válido
    }
    float GetDistance(GridMarkerBehaviour nodeA, GridMarkerBehaviour nodeB)
    {
        float distanceX = nodeA.x - nodeB.x;
        float distanceY = nodeA.y - nodeB.y;
        float distanceZ = nodeA.z - nodeB.z;

        return Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY + distanceZ * distanceZ);
    }

    public bool IsCellNavigable(int x, int y, int z)
    {
        if (x < 0 || x >= gridSizeX || y < 0 || y >= gridSizeY || z < 0 || z >= gridSizeZ)
        {
            return false; 
        }
        return grid[x, y, z];
    }
}

