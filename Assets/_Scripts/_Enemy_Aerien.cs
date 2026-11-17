using UnityEngine;

public class _Enemy_Aerien : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float FlyDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _enemy.transform.position = new Vector3(transform.position.x, FlyDistance, transform.position.z);
    }
}
