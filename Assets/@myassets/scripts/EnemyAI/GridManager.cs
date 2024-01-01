using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSizeX = 10;
    [SerializeField] private int gridSizeY = 10;
    [SerializeField] private int gridSizeZ = 10;
    [SerializeField] public int cellSize = 1;
    [SerializeField] private GameObject gridMarkerObject;

    private GridMarkerBehaviour[,,] grid;

    void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new GridMarkerBehaviour[gridSizeX, gridSizeY, gridSizeZ];

        for (int line = 0; line < gridSizeY; line++)
        {
            for (int column = 0; column < gridSizeX; column++)
            {
                for (int depth = 0; depth < gridSizeZ; depth++)
                {
                    Vector3 markerPosition = new Vector3(column * cellSize, line * cellSize, depth * cellSize);
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
        int x = Mathf.Clamp(Mathf.RoundToInt(worldPosition.x / cellSize), 0, gridSizeX - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt(worldPosition.y / cellSize), 0, gridSizeY - 1);
        int z = Mathf.Clamp(Mathf.RoundToInt(worldPosition.z / cellSize), 0, gridSizeZ - 1);

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
        var offsets = new[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0), new Vector3(0, 0, 1), new Vector3(0, 0, -1) };
        List<GridMarkerBehaviour> neighbors = new List<GridMarkerBehaviour>();

        foreach (var offset in offsets)
        {
            int neighborX = currentNode.x + Mathf.RoundToInt(offset.x);
            int neighborY = currentNode.y + Mathf.RoundToInt(offset.y);
            int neighborZ = currentNode.z + Mathf.RoundToInt(offset.z);

            if (IsCellNavigable(neighborX, neighborY, neighborZ))
            {
                neighbors.Add(grid[neighborX, neighborY, neighborZ]);
            }
        }

        return neighbors;
    }

    public List<GridMarkerBehaviour> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        GridMarkerBehaviour startNode = GetGridMarkerFromPosition(startPos);
        GridMarkerBehaviour targetNode = GetGridMarkerFromPosition(targetPos);

        List<GridMarkerBehaviour> openSet = new List<GridMarkerBehaviour> { startNode };
        HashSet<GridMarkerBehaviour> closedSet = new HashSet<GridMarkerBehaviour>();

        while (openSet.Count > 0)
        {
            GridMarkerBehaviour currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
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
        return x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY && z >= 0 && z < gridSizeZ && grid[x, y, z].isNavigable;
    }
}
