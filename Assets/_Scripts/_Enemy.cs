using UnityEngine;

public class _Enemy : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public GameObject Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed);
    }
}
