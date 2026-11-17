    using JetBrains.Annotations;
    using System;
    using System.Collections;
    using System.Collections.Generic;
using System.IO;
using UnityEngine;

    [System.Serializable]
    public class EnemySpawn
    {
         public GameObject prefab;
         public int spawnCount = 0;
    }

    
    
    [System.Serializable]
    public class Wave
    {
         public string name;
        public Transform SpawnPoints;

        public List<EnemySpawn> enemies; 
    }
    public class Wave_Manager : MonoBehaviour
    {
        [SerializeField] private List<Wave> waves;
        [SerializeField] private float timeBetweenWave;
        [SerializeField] public float Time_Betweens_enemies;


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
                    for (int i = 0; i < spawn.spawnCount; i ++) 
                    {
                        GameObject enemy = Instantiate(spawn.prefab, wave.SpawnPoints.position, Quaternion.identity);
                        yield return new WaitForSeconds(Time_Betweens_enemies);
                    }
                }
                yield return new WaitForSeconds(timeBetweenWave);

            }

        }
    }
    
