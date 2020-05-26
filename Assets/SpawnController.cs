using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    GridAStar grid;
    public GameObject crate;

    public GameObject[] weaponsPrefabs;
    public Transform weaponSlot;

    int weaponIndex;

    private void Start() {
        grid = FindObjectOfType<GridAStar>();
        
        Crate.cratePickedUp += SpawnCrate;

        weaponIndex = Random.Range(0, weaponsPrefabs.Length);

        SpawnCrate();
    }

    void SpawnCrate() {
        Node spawnNode = grid.nodeMap[Random.Range(0, grid.gridSize), Random.Range(0, grid.gridSize)];
        while (!spawnNode.walkable) {
            spawnNode = grid.nodeMap[Random.Range(0, grid.gridSize), Random.Range(0, grid.gridSize)];
        }
        Instantiate(crate, spawnNode.worldPosition + Vector3.up * 2, Quaternion.identity);

        SwapPlayerWeapon();
    }

    void SwapPlayerWeapon() {
        int oldIndex = weaponIndex;
        while (weaponIndex == oldIndex) {
            weaponIndex = Random.Range(0, weaponsPrefabs.Length);
        }

        UseWeapon(weaponIndex);
    }

    void UseWeapon(int index) {
        Destroy(weaponSlot.transform.GetChild(0).gameObject);
        Instantiate(weaponsPrefabs[index], weaponSlot);
    }
}
