using UnityEngine;

public class _Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject Target;
    [SerializeField] private _ENemy_Data _EnmData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed);
        
    }
    public void OnDeath()
    {
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("M_ENM_Target"))
        {
            OnDeath();
        }
    }

}