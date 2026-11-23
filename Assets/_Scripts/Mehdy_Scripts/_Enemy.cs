using GS;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class _Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _Target;
    [SerializeField] private _ENemy_Data _EnmData;
    public float _Bulletspeed;
    public float _FireRate;
    public GameObject _BulletPrefab;
    [HideInInspector] public bool _canMove = true;

    private Slider _HealthBar;

    [SerializeField] private int Health;

    [SerializeField] private int UpHealth ;
    private Wave_Manager Wave_Manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wave_Manager = FindAnyObjectByType<Wave_Manager>();
        _HealthBar = GetComponentInChildren<Slider>();
        Health = _EnmData.InitialHealth;
        _HealthBar.maxValue = _EnmData.InitialHealth;
        _HealthBar.value = Health;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (_canMove)
            Enm_Move();

    }
    public void OnDeath()
    {
        
        Destroy(gameObject);

    }
    
    public void Enm_Move()
    {
        Vector3 TargetPos = _Target.transform.position;
        TargetPos.y = transform.position.y;
        
        

        transform.position = Vector3.MoveTowards(transform.position, TargetPos, _EnmData.Speed * Time.deltaTime);
        transform.LookAt(_Target.transform);
        if (transform.position.z == TargetPos.z && transform.position.x == TargetPos.x)
        {
            OnDeath();
        }
       
    }

    public void Stop_Enm_Move()
    {
        _canMove = false;
    }

    public void UpEnemyHealth()
    {
         Health += UpHealth;
    }

    public void UpdateEnmHealthBar()
    {
        _HealthBar.value = Health;
    }    

}