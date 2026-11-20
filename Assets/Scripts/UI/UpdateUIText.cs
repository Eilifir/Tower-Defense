using TMPro;
using UnityEngine;

public class UpdateUIText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI livesText;
    private void OnEnable()
    {
        EventManager<GameEvents>.Subscribe<GoldChangedEvent>(GameEvents.goldChanged, ChangeGoldText);
        EventManager<GameEvents>.Subscribe<LivesChangedEvent>(GameEvents.livesChanged, ChangeLivesText);
    }
    private void OnDisable()
    {
        EventManager<GameEvents>.Unsubscribe<GoldChangedEvent>(GameEvents.goldChanged, ChangeGoldText);
        EventManager<GameEvents>.Unsubscribe<LivesChangedEvent>(GameEvents.livesChanged, ChangeLivesText);
    }

    void ChangeGoldText(GoldChangedEvent data)
    {
        goldText.text = data.gold.ToString();
    }
    void ChangeLivesText(LivesChangedEvent data)
    {
        livesText.text = data.currentLives.ToString();
    }
}
