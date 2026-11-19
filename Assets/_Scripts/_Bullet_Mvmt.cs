using UnityEngine;

public class _Bullet_Mvmt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void MoveToTarget(Transform targetedTower, float BulletSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetedTower.position, BulletSpeed);
    }
}   
