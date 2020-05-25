using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    GridAStar grid;
    public GameObject crate;

    private void Start() {
        grid = FindObjectOfType<GridAStar>();
        StartCoroutine(SpawnCubes());
    }

    private void Update() {
        
    }

    IEnumerator SpawnCubes() {
        while (true) {
            SpawnCrate();
            yield return new WaitForSeconds(1);
        }
    }

    void SpawnCrate() {
        Node spawnNode = grid.nodeMap[Random.Range(0, grid.gridSize), Random.Range(0, grid.gridSize)];
        while (!spawnNode.walkable) {
            spawnNode = grid.nodeMap[Random.Range(0, grid.gridSize), Random.Range(0, grid.gridSize)];
        }
        Instantiate(crate, spawnNode.worldPosition + Vector3.up * 2, Quaternion.identity);
        Debug.Log("spawned crate");
    }
}
