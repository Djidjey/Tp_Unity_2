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
    private CoinCounter coinCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Target = GameObject.FindGameObjectWithTag("M_ENM_Target");

        Wave_Manager = FindAnyObjectByType<Wave_Manager>();
        _HealthBar = GetComponentInChildren<Slider>();
        coinCounter = FindAnyObjectByType<CoinCounter>();
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
        coinCounter.IncreaseCoins(_EnmData.MoneyDrop);
        Destroy(gameObject);

    }
    
    public void Enm_Move()
    {
        Vector3 TargetPos = _Target.transform.position;
        TargetPos.y = transform.position.y;
        
        Quaternion TargetRot = _Target.transform.rotation;
        TargetRot.x = 0;

        transform.position = Vector3.MoveTowards(transform.position, TargetPos, _EnmData.Speed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(_Target.transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);

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

    public void TakeDamage(int  damage)
    {

        Health -= damage;

        Debug.Log("Damage received");
        Debug.Log("Amount of damage : " + damage);
        Debug.Log("Health remaininf : " + Health);

        if (Health <= 0)
        {
            Destroy(gameObject);
        }

        UpdateEnmHealthBar();
    }

    public void UpdateEnmHealthBar()
    {
        _HealthBar.value = Health;
    }    

}