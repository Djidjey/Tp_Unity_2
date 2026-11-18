using UnityEngine;

public class _Enemy_Aerien : MonoBehaviour
{
    [SerializeField] private float FlyDistance;

    private _Enemy _Enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Enemy = GetComponent<_Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, FlyDistance, transform.position.z);    
    }
}
