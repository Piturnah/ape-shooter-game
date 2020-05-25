using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningScript : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public GameObject Spawnto;
    public int numberofchimps = 0;
    public int SpawnLimit = 100;

    //This is so it doesn't need to use two different objects to spawn on both sides
    public Vector3 center;
    public Vector3 xsize;
    public Vector3 zsize;
    public Vector3 distance = new Vector3(-185,0,0);

    public float initialTimeBtwChimps = 5;
    public float finalTimeBtwChimps = 0.2f;
    public float timeUntilMaxChimpRate = 500;

    float previousChimpSpawnTime;

    void Update()
    {
        if (previousChimpSpawnTime + Mathf.Lerp(initialTimeBtwChimps, finalTimeBtwChimps, Time.time/timeUntilMaxChimpRate) < Time.time) {
            SpawnEnemy();
            previousChimpSpawnTime = Time.time;
            numberofchimps++;
            Debug.Log(Mathf.Lerp(initialTimeBtwChimps, finalTimeBtwChimps, Time.time / timeUntilMaxChimpRate));
        }
    }

    public void SpawnEnemy()
    {
        Vector3 pos1 = transform.localPosition + center + new Vector3(Random.Range(-xsize.x / 2, xsize.x / 2), 10, Random.Range(-zsize.z / 2, zsize.z / 2));
        Vector3 pos2 = transform.localPosition + center + new Vector3((Random.Range(-xsize.x / 2, xsize.x / 2) + distance.x), 10, Random.Range(-zsize.z / 2, zsize.z / 2));

        EnemyPrefab = Instantiate(EnemyPrefab, pos1, Quaternion.identity);
        EnemyPrefab.transform.SetParent(Spawnto.transform);

        EnemyPrefab = Instantiate(EnemyPrefab, pos2, Quaternion.identity);
        EnemyPrefab.transform.SetParent(Spawnto.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.localPosition + center, new Vector3(xsize.x, 1, zsize.z));
        Gizmos.DrawWireCube(transform.localPosition + center + distance, new Vector3(xsize.x, 1, zsize.z));
    }

}
