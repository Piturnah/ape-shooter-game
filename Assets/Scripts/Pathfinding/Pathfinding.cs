using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour
{
    GridAStar grid;

    private void Awake() {
        grid = GetComponent<GridAStar>();
    }

    public void FindPath(PathRequest request, Action<PathResult> callback) {

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(request.pathStart);
        Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);

        if (startNode.walkable && targetNode.walkable) {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                Node currentNode = openSet.PopFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode) {
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbourNode in grid.GetNeighbouringNodes(currentNode)) {
                    if (!neighbourNode.walkable || closedSet.Contains(neighbourNode)) {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbourNode);
                    if (newMovementCostToNeighbour < neighbourNode.gCost || !openSet.Contains(neighbourNode)) {
                        neighbourNode.gCost = newMovementCostToNeighbour;
                        neighbourNode.hCost = GetDistance(neighbourNode, targetNode);
                        neighbourNode.parentNode = currentNode;

                        if (!openSet.Contains(neighbourNode)) {
                            openSet.Add(neighbourNode);
                        }
                        else {
                            openSet.UpdateItem(neighbourNode);
                        }
                    }
                }
            }
        }
        if (pathSuccess) {
            waypoints = RetracePath(startNode, targetNode);
        }
        callback(new PathResult(waypoints, pathSuccess, request.callback));
    }

    Vector3[] RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path) {
        List<Vector3> waypoints = new List<Vector3>();
        Vector3 directionOld = Vector3.zero;

        for (int i = 0; i < path.Count - 1; i++) {
            Vector3 directionNew = (path[i + 1].worldPosition - path[i].worldPosition).normalized;
            if (directionNew != directionOld) {
                waypoints.Add(path[i].worldPosition);
                directionOld = directionNew;
            }
        }
        if (path.Count > 0) {
            waypoints.Add(path[path.Count - 1].worldPosition);
        }
        
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB) {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY) {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
