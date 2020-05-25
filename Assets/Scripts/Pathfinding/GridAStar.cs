using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAStar : MonoBehaviour
{
    public bool displayGridGizmos;
    public LayerMask unwalkableMask;
    public LayerMask walkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public Collider terrainMeshCollider;

    Node[,] nodeMap;

    float nodeDiameter;
    int gridSize;

    List<Node> toRemakeWalkable = new List<Node>();

    private void Awake() {
        nodeDiameter = nodeRadius * 2;
        gridSize = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);

        CreateGrid();
    }

    public void MakeUnwalkable(Vector3 worldPos) {
        Node node = NodeFromWorldPoint(worldPos);
        if (node.walkable) {
            NodeFromWorldPoint(worldPos).walkable = false;
            toRemakeWalkable.Add(node);
        }
    }

    private void LateUpdate() {
        foreach (Node n in toRemakeWalkable) {
            n.walkable = true;
        }
    }

    void CreateGrid() {
        nodeMap = new Node[gridSize, gridSize];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2f - Vector3.forward * gridWorldSize.y / 2f;

        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {

                // Determine height of terrain mesh at pos
                RaycastHit hit;
                Vector3 worldPoint = Vector3.zero;
                Vector3 rayCastWorldPoint = new Vector3(worldBottomLeft.x + (x * nodeDiameter + nodeRadius), 20, worldBottomLeft.z + (y * nodeDiameter + nodeRadius));

                Ray ray = new Ray(rayCastWorldPoint, Vector3.down);
                if (Physics.Raycast(ray, out hit, 40, unwalkableMask + walkableMask)) {
                    worldPoint = hit.point;
                } else {
                    Debug.LogError("NO TERRAIN FOUND IN RAYCAST AT WORLD POS: " + rayCastWorldPoint);
                }

                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);
                nodeMap[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public int MaxSize {
        get {
            return gridSize * gridSize;
        }
    }

    public List<Node> GetNeighbouringNodes(Node node) {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSize && checkY >= 0 && checkY < gridSize) {
                    neighbours.Add(nodeMap[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition) {
        float percentX = Mathf.Clamp01((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float percentY = Mathf.Clamp01((worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

        int x = Mathf.RoundToInt((gridSize - 1) * percentX);
        int y = Mathf.RoundToInt((gridSize - 1) * percentY);

        return nodeMap[x, y];
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 20, gridWorldSize.y));

        if (nodeMap != null && displayGridGizmos) {
            foreach (Node node in nodeMap) {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                Gizmos.DrawSphere(node.worldPosition, nodeRadius);
            }
        }
    }
}
