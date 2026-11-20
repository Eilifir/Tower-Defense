using TMPro;
using UnityEngine;

public class UpdateTextBuildButtons : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI archerText;
    [SerializeField] TextMeshProUGUI aoeText;
    [SerializeField] TextMeshProUGUI mageText;
    private void OnEnable()
    {
        archerText.text = TowerFlyWeightPointer.Get(TowerTypes.Archer, 0).cost.ToString() + " G";
        aoeText.text = TowerFlyWeightPointer.Get(TowerTypes.Aoe, 0).cost.ToString() + " G";
        mageText.text = TowerFlyWeightPointer.Get(TowerTypes.Mage, 0).cost.ToString() + " G";
    }
}
