using UnityEngine;
using UnityEngine.UI;

namespace GS
{
    public class BuildSpotUI : MonoBehaviour
    {
        [SerializeField] private Button _buttonTower1;
        [SerializeField] private Button _buttonTower2;
        [SerializeField] private Button _buttonTower3;

        private BuildSpot _buildSpot;

        private void Awake()
        {
            _buttonTower1.onClick.AddListener(() => OnButtonClicked(TowerType.Canon));
            _buttonTower2.onClick.AddListener(() => OnButtonClicked(TowerType.Condensateur));
            _buttonTower3.onClick.AddListener(() => OnButtonClicked(TowerType.Mortier));
        }

        public void Init(BuildSpot buildSpot)
        {
            _buildSpot = buildSpot;
        }

        private void OnButtonClicked(TowerType type)
        {
            TowerManager towerManager = FindAnyObjectByType<TowerManager>();
            towerManager.BuildTower(_buildSpot, type);
            Close();
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}