using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GS
{
    public enum TowerState
    {
        Idle,
        Detecting,
        Targeting, 
        Attacking
    }
    
    public class Tower : MonoBehaviour
    {
        [SerializeField] private TowerData _towerData;
        [SerializeField] private LayerMask _layerMask;
        
        private TowerState _currentState = TowerState.Idle;
        private float _timerAttack;

        private List<_Enemy> _allEnemies;
        private List<_Enemy> _detectedEnemies;
        private List<_Enemy> _targetedEnemies;

        private EnemyManager _enemyManager;
        private GameObject actualTargetedEnnemy;

        private void Start()
        {
            _detectedEnemies = new List<_Enemy>();
            _enemyManager = FindAnyObjectByType<EnemyManager>();

            // ---------------------------------------- //
                
        }

        public TowerData GetTowerData() { return _towerData; } // Fonction 
        public TowerData TowerData => _towerData; // Property



        // ====================================================== MAIN FUNCTIONS ======================================================



        private void Update()
        {
            _timerAttack += Time.deltaTime;
            
            // Define tower current state 

            switch (_currentState)
            {
                case TowerState.Idle:
                    if (_timerAttack > _towerData.RatePerSeconds)
                    {
                        _timerAttack = 0;
                        _currentState = TowerState.Detecting;
                    }
                    break;

                case TowerState.Detecting:
                    _detectedEnemies = DetectEnemies();

                    if (_detectedEnemies.Count > 0)
                    {

                        _currentState = TowerState.Targeting;

                        // Rotate toward the ennemy
                        //Quaternion targetRotation = Quaternion.LookRotation(actualTargetedEnnemy.transform.position - transform.position, Vector3.up);
                        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.5f);
                    }

                    else _currentState = TowerState.Idle;

                    break;

                case TowerState.Targeting:
                    _targetedEnemies = new List<_Enemy>();
                    _targetedEnemies = TargetEnemies(_detectedEnemies);

                    if (_targetedEnemies.Count > 0) _currentState = TowerState.Attacking;

                    else _currentState = TowerState.Idle;


                    break;

                case TowerState.Attacking:
                    Attack(_targetedEnemies);
                    _currentState = TowerState.Idle;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }



        // ============================================================================================================================


        // ====================================================== CUSTOM FUNCTIONS ====================================================



        private void Attack(List<_Enemy> targetedEnemies)
        {
            foreach (_Enemy targetedEnemy in targetedEnemies)
            {
                // if (targetedEnemy = null)
                actualTargetedEnnemy = targetedEnemy.gameObject;


                // Attack the ennemy
                targetedEnemy.TakeDamage(_towerData.Damage);
            }
        }



        private List<_Enemy> TargetEnemies(List<_Enemy> detectedEnemies)
        {
            if (detectedEnemies.Count == 0) return new List<_Enemy>();

            List<_Enemy> targetedEnemies = new List<_Enemy>();
            
            switch (_towerData.TowerTargetingMode)
            {
                case TowerTargetingMode.FirstOnPath:
                    break;
                case TowerTargetingMode.ClosestOnPath:
                    _Enemy closestEnemy = detectedEnemies[0];
                    float shortestDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);

                    for (int i = 1; i < detectedEnemies.Count; i++)
                    {
                        float distance = Vector3.Distance(transform.position, detectedEnemies[i].transform.position);
                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                            closestEnemy = detectedEnemies[i];
                        }
                    }
                    
                    targetedEnemies.Add(closestEnemy);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return targetedEnemies;
        }

        private List<_Enemy> DetectEnemies()

        // Find and target the enemies

        {
            List<_Enemy> enemies = new List<_Enemy>();

            foreach (Collider col in Physics.OverlapSphere(transform.position, _towerData.Range, _layerMask))
            {
                if (col.TryGetComponent(out _Enemy enemy))
                {
                    Debug.Log("detected inside the loop");
                    enemies.Add(enemy);
                }
            }

            return enemies;
        }



        // ============================================================================================================================



        // ===================================================== ON EVENT FUNCTIONS ===================================================



        private void OnDrawGizmosSelected()
        {
            switch (_currentState)
            {
                case TowerState.Idle:
                    Gizmos.color = Color.grey;
                    break;

                case TowerState.Detecting:
                    Debug.Log("Has detected something...");
                    Gizmos.color = Color.yellow;
                    break;

                case TowerState.Targeting:
                    Debug.Log("Has targeted something...");
                    Gizmos.color = Color.orange;
                    break;

                case TowerState.Attacking:
                    Debug.Log("Has attacked something...");
                    Gizmos.color = Color.red;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            Gizmos.DrawWireSphere(transform.position, _towerData.Range);

            if (_detectedEnemies != null && _currentState == TowerState.Detecting)
            {
                foreach (_Enemy detectedEnemy in _detectedEnemies)
                {
                    Gizmos.DrawLine(transform.position, detectedEnemy.transform.position);
                }
            }

            if (_targetedEnemies != null && _currentState == TowerState.Targeting)
            {
                foreach (_Enemy detectedEnemy in _targetedEnemies)
                {
                    Gizmos.DrawLine(transform.position, detectedEnemy.transform.position);
                }
            }
        }

        // ============================================================================================================================

    }
}


