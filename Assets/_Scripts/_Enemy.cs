using UnityEngine;

public class _Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _Target;
    [SerializeField] private _ENemy_Data _EnmData;
    public float _Bulletspeed;
    public float _FireRate;
    public GameObject _BulletPrefab;
    [HideInInspector] public bool _canMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

}