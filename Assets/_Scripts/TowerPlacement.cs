using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private LayerMask PlacementCheckMask;
    [SerializeField] private LayerMask placementCollideMask;
    [SerializeField] private Camera PlayerCamera;
    private GameObject CurrentPlacingTower;

    void Start()
    {
        
    }
     
    // Update is called once per frame
    void Update()
    {
        if(CurrentPlacingTower != null)
        {
            Ray camray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;

            if(Physics.Raycast(camray, out HitInfo, 100f, placementCollideMask))
            {
                CurrentPlacingTower.transform.position = new Vector3(HitInfo.point.x, 0, HitInfo.point.z);
            }

            if(Input.GetMouseButtonDown(0) && HitInfo.collider.gameObject != null)
            {
                // If the object we are hitting does not have the can't place tag
                if(!HitInfo.collider.CompareTag("CantPlace"))
                {

                    // take the reference of the collider
                    BoxCollider TowerCollider = CurrentPlacingTower.gameObject.GetComponent<BoxCollider>();

                    // Provide the checkbow to check our own collider
                    TowerCollider.isTrigger = true; 

                    // Add the offset and divide it by two
                    Vector3 BoxCenter = CurrentPlacingTower.gameObject.transform.position + TowerCollider.center;
                    Vector3 HalfExtents = TowerCollider.size / 2;
                   
                    // Check if we are not intersecting with another collider
                    if(!Physics.CheckBox(BoxCenter, HalfExtents, Quaternion.identity, PlacementCheckMask, QueryTriggerInteraction.Ignore))
                    {
                        TowerCollider.isTrigger = false;
                        CurrentPlacingTower = null;
                        Debug.Log("Something happened...");
                    }
                    
                }
            }
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        CurrentPlacingTower = Instantiate(tower, transform.position, Quaternion.identity);
    }
}
