using UnityEngine;

[CreateAssetMenu(fileName = "_ENemy_Data", menuName = "Scriptable Objects/_ENemy_Data")]
public class _ENemy_Data : ScriptableObject
{
    [SerializeField] private string _Enemy_Name;

    [SerializeField] private float _speed;

    [SerializeField] private int _damage;

    [SerializeField] private int _Initialheath;

    [SerializeField] private float _fireRate;

    public float Speed => _speed;
    public int Damage => _damage;
    public int InitialHealth => _Initialheath;
    public float FireRate => _fireRate;
    
}
