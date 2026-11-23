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

        private CoinCounter _coinCounter;
        public Action<BuildSpot> _towerBuilt;

        public void BuildTower(BuildSpot buildSpot, TowerType type)
        {
            if (buildSpot.State == BuildSpotState.Occupied) return;

            GameObject prefab = GetPrefab(type);
            if (prefab == null) return;
            if (!prefab.TryGetComponent(out Tower tower)) return;
            _coinCounter = FindAnyObjectByType<CoinCounter>();

            // ----------------------------------------- // 


            if (tower.TowerData.Cost <= _coinCounter.currentCoins)
            {
                _coinCounter.IncreaseCoins(tower.TowerData.Cost * -1);
                Debug.Log("salut toi");

                Instantiate(prefab, buildSpot.transform.position, Quaternion.identity);

                _towerBuilt?.Invoke(buildSpot);
            }
            else
            {
                Debug.Log("La tour n'a pas pu être crée : Fonds suffisant");
                Debug.Log("Vos fonds : " + _coinCounter.currentCoins);
                Debug.Log("Prix de la tour : " + tower.TowerData.Cost);
                Debug.Log("Fonction : " + tower.TowerData.Cost + " <= " + _coinCounter.currentCoins);
            }

            // ----------------------------------------- // 

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