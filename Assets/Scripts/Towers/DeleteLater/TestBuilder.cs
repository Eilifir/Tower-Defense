using UnityEngine;
using UnityEngine.InputSystem;

public class TestBuilder : MonoBehaviour
{
    [SerializeField] LayerMask towerLayers;
    [SerializeField] TowerTypes type;

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero,towerLayers);
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.gameObject.TryGetComponent<BuildSite>(out BuildSite spot))
                {
                    var data = new BuildSpotClickedEvent(hit.transform.position, spot);
                    EventManager<GameEvents>.Publish<BuildSpotClickedEvent>(GameEvents.buildSpotClicked, data);
                }
                else if(hit.collider.gameObject.TryGetComponent<TowerTargetingSystem>(out  TowerTargetingSystem targetingSystem))
                {
                    var data = new TowerClickedEvent(hit.transform.position, targetingSystem);
                    EventManager<GameEvents>.Publish<TowerClickedEvent>(GameEvents.towerClicked, data);
                }
            }
        }
    }
}
