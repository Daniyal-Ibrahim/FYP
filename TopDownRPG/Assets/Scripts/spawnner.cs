using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnner : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject enemyPrefab;
    public int enemyCount;
    bool spawn = true;
    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            StartCoroutine(Delay());
            spawn = false;
        }
    }

    IEnumerator Delay()
    {
        Spawn();
        enemyCount++;
        yield return new WaitForSecondsRealtime(5f);
        spawn = true;
    }

    void Spawn()
    {
        Instantiate(enemyPrefab, spawnPoint.transform);

    }
}
