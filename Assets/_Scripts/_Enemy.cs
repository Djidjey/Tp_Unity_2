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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            {
                Health -= 5;
                UpdateEnmHealthBar();
                print(Health);
            }
        }
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
        transform.rotation = _Target.transform.rotation;

        if (transform.position.z == TargetPos.z && transform.position.x == TargetPos.x)
        {
            print("dead");
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