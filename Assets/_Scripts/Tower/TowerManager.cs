using System;
using UnityEngine;

namespace GS
{
    public enum TowerType
    {
        Canon,
        Condensateur,
        Mortier
    }

    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _towerPrefab;   
        [SerializeField] private GameObject _towerPrefabB;  
        [SerializeField] private GameObject _towerPrefabC;  

        public Action<BuildSpot> _towerBuilt;

        public void BuildTower(BuildSpot buildSpot, TowerType type)
        {
            if (buildSpot.State == BuildSpotState.Occupied) return;

            GameObject prefab = GetPrefab(type);
            if (prefab == null) return;

            Instantiate(prefab, buildSpot.transform.position, Quaternion.identity);

            _towerBuilt?.Invoke(buildSpot);
        }

        private GameObject GetPrefab(TowerType type)
        {
            switch (type)
            {
                case TowerType.Canon: return _towerPrefab;
                case TowerType.Condensateur: return _towerPrefabB;
                case TowerType.Mortier: return _towerPrefabC;
                default: return null;
            }
        }
    }
}