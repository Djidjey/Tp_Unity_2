using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public float delayAfter = -1f;
}

[System.Serializable]
public class Wave
{
    [SerializeField] public string name;
    [SerializeField] public List<EnemySpawn> enemies;
    [HideInInspector] public float spawnDelay = 1f;
}
public class Wave_Manager : MonoBehaviour
{
    [SerializeField] public List<Wave> waves;
    [SerializeField] public float timeBetweenWave;
    [SerializeField] public Transform EnemySpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RunWaves());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator RunWaves()
    {
        foreach (Wave wave in waves)
        {
            foreach (EnemySpawn spawn in wave.enemies)
            {
                GameObject enemy = Instantiate(spawn.prefab, EnemySpawn.position, Quaternion.identity);

                float delay = spawn.delayAfter >= 0f ? spawn.delayAfter : wave.spawnDelay;
            

                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(timeBetweenWave);

        }

    }
}
    
