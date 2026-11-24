using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GS
{
    public class BuildSpotManager : MonoBehaviour
    {
        [SerializeField] private BuildSpotUI _uiBuildSpotPrefab;
        
        private List<BuildSpot> _buildSpots;

        private BuildSpot _highlightedBuildSpot;
        private BuildSpotUI _uiBuildSpotInstance;
        
        private void Start()
        {
            _buildSpots = GetComponentsInChildren<BuildSpot>().ToList();
            FindAnyObjectByType<TowerManager>()._towerBuilt += OnTowerBuilt;
        }

        private void OnDestroy()
        {
            FindAnyObjectByType<TowerManager>()._towerBuilt -= OnTowerBuilt;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
            bool hasClickMouse = InputSystem.actions.FindAction("Attack").WasPressedThisFrame();

            // If mouse on UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            BuildSpot hoveredSpot = null;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                hoveredSpot = hit.collider.GetComponentInParent<BuildSpot>();
            }

            // If there are no spot
            if (hoveredSpot != _highlightedBuildSpot)
            {
                _highlightedBuildSpot?.StopHighlight();
                _highlightedBuildSpot = hoveredSpot;
                _highlightedBuildSpot?.Highlight();
            }

            if (!hasClickMouse) return;

            // On click
            if (hoveredSpot != null && hoveredSpot.State == BuildSpotState.Free)
            {
                TryCloseUI();
                _uiBuildSpotInstance = Instantiate(_uiBuildSpotPrefab, hoveredSpot.transform);
                _uiBuildSpotInstance.Init(hoveredSpot);
            }
            else
            {
                TryCloseUI();
            }
        }

        private bool TryCloseUI()
        {
            if (_uiBuildSpotInstance == null) return false;
            
            _uiBuildSpotInstance.Close();
            return true;
        }

        private void OnTowerBuilt(BuildSpot buildSpot)
        {
            buildSpot.SetOccupied();
        }
    }
}
