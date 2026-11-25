using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildButtonsUI : MonoBehaviour
{
    [SerializeField] GameObject towerIcons;
    [SerializeField] GameObject upgradeIcons;
    [SerializeField] GameObject upgradeIcon;
    [SerializeField] TowerPlacer towerPlacer;
    [SerializeField] LayerMask towerLayer;
    BuildSite caller;
    TowerTargetingSystem tower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        EventManager<GameEvents>.Subscribe<BuildSpotClickedEvent>(GameEvents.buildSpotClicked, BuildSpotClicked);
        EventManager<GameEvents>.Subscribe<TowerClickedEvent>(GameEvents.towerClicked, TowerClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!Physics2D.Raycast(worldPoint, Vector2.zero, towerLayer) &&!EventSystem.current.IsPointerOverGameObject())
            {
                towerIcons.SetActive(false);
                //upgradeIcons.SetActive(false);
            }
        }
    }

    public void BuildSpotClicked(BuildSpotClickedEvent data)
    {
        caller = data.site;
        towerIcons.transform.position = Camera.main.WorldToScreenPoint(data.position);
        towerIcons.SetActive(true);
    }
    public void TowerClicked(TowerClickedEvent data)
    {
        tower = data.tower;
        if(tower.level >= tower.maxLevel) upgradeIcon.SetActive(false);
        else 
        { 
            upgradeIcon.SetActive(true); 
            TowerFlyWeight towerData = TowerFlyWeightPointer.Get(tower.TowerType, tower.level);
            upgradeIcon.GetComponentInChildren<TextMeshProUGUI>().text = towerData.cost.ToString() + " G";
        }
        upgradeIcons.transform.position = Camera.main.WorldToScreenPoint(data.position);
        upgradeIcons.SetActive(true);
    }

    //Criminal but: Archer = 0, Mage = 1, AoE = 2
    public void BuildTower(int type)
    {
        towerPlacer.TryBuild(caller, (TowerTypes)type);
        towerIcons.SetActive(false);
    }
    public void UpgradeTower()
    {
        towerPlacer.TryUpgrade(tower);
        upgradeIcons.SetActive(false);
    }
    public void Sell()
    {
        towerPlacer.TrySell(tower, tower.TowerType);
        upgradeIcons.SetActive(false);
    }
}
