using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAStar : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public Collider terrainMeshCollider;

    Node[,] nodeMap;

    float nodeDiameter;
    int gridSize;

    private void Start() {
        nodeDiameter = nodeRadius * 2;
        gridSize = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);

        CreateGrid();
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
                if (Physics.Raycast(ray, out hit, 40)) {
                    worldPoint = hit.point;
                } else {
                    Debug.LogError("NO TERRAIN FOUND IN RAYCAST AT WORLD POS: " + rayCastWorldPoint);
                }

                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);
                nodeMap[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 20, gridWorldSize.y));

        if (nodeMap != null) {
            foreach (Node node in nodeMap) {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                Gizmos.DrawSphere(node.worldPosition, nodeRadius);
            }
        }
    }
}
