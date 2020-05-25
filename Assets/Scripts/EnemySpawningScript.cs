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

    public float SpawnRate = 1f;
    public float period = 0.1f;
    public float timeInterval = 10f;
    public float Decreaseovertime = 1f;

    void Update()
    {
        if (period > timeInterval)
        {
            if (numberofchimps < SpawnLimit)
            {

                SpawnEnemy();
                numberofchimps += 1;
                if (timeInterval > 2)
                {
                    timeInterval -= Decreaseovertime;
                }

            }
            period = 0;
        }
        period += UnityEngine.Time.deltaTime * SpawnRate;
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
