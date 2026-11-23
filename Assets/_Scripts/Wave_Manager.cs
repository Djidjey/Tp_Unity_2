using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
    public class EnemySpawn
    {
         public GameObject prefab;
         public int spawnCount = 0;
    }
[System.Serializable]
public class PathWave
{
    public Transform SpawnPoints;
    public List<EnemySpawn> enemies;
}


[System.Serializable]
    public class Wave
    {
         public string name;

        public List<PathWave> PathWaves; 
    }
    public class Wave_Manager : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves;
        [SerializeField] private float timeBetweenWave;
        [SerializeField] private float Time_Betweens_enemies;
        private const int PathCount = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
        {
            StartCoroutine(RunWaves());
        
        }
    private void OnValidate()
    {
        /*if (waves == null)return;

        foreach (Wave wave in waves)
        {
            if (wave.PathWaves == null)
                wave.PathWaves = new List<PathWave>();
            while(wave.PathWaves.Count < PathCount)
            {
                wave.PathWaves.Add(new PathWave { enemies = new List<EnemySpawn>() });
            }
            while (wave.PathWaves.Count > PathCount)
                wave.PathWaves.RemoveAt(wave.PathWaves.Count - 1);
        }*/

    }

    // Update is called once per frame
    void Update()
        {

        }
        IEnumerator RunWaves()
        {   
            
            foreach (Wave wave in waves)
            {
                int PathRunning = 0;

                foreach (PathWave path in wave.PathWaves)
                    {
                        PathRunning++; 
                        StartCoroutine(RunPath(path, () => PathRunning --));
                    }
            
            
                yield return new WaitUntil(() => PathRunning == 0);
                
                yield return new WaitForSeconds(timeBetweenWave);
        }       
    }
    IEnumerator RunPath(PathWave path, Action OnFinished)
    {
        int total = 0;
        foreach (EnemySpawn EnemyList in path.enemies) total += EnemyList.spawnCount;

        int spawned = 0;
        foreach (EnemySpawn spawn in path.enemies)
        {
            for (int i = 0; i < spawn.spawnCount; i++)
            {
                Instantiate(spawn.prefab, path.SpawnPoints.position, Quaternion.identity);
                spawned++;

                if (spawned < total)
                    yield return new WaitForSeconds(Time_Betweens_enemies);
            }
        }
        OnFinished?.Invoke();
    }
}    