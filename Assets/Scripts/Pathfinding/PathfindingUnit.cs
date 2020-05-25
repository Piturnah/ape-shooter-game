using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingUnit : MonoBehaviour
{
    public Transform target;
    float speed = 10;
    Vector3[] path;
    int targetIndex;

    float pathUpdateMoveThreshold;
    float minPathUpdateTime;

    GridAStar grid;

    private void Start() {
        target = FindObjectOfType<PlayerController>().transform;
        grid = FindObjectOfType<GridAStar>();
        StartCoroutine(UpdatePath());
    }

    void FindNewPath() {
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            StopCoroutine("FollowPath");
            targetIndex = 0;
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath() {
        if (Time.timeSinceLevelLoad < .3f) {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        grid.MakeUnwalkable(transform.position);

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true) {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];

        while (true) {
            if (transform.position.x == currentWaypoint.x && transform.position.y == currentWaypoint.y) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentWaypoint.x, transform.position.y, currentWaypoint.z), speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos() {
        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(path[i], FindObjectOfType<GridAStar>().nodeRadius);

                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
