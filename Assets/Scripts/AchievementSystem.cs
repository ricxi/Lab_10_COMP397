using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private VoidEventChannel voidChannel;
    [SerializeField] private GameDataEventChannel gameDataChannel;
    [SerializeField] private int achievementJumps = 10;

    private int _currentJumps = 0;

    private void OnEnable()
    {
        voidChannel.OnEventRaised += EventCalled;
        gameDataChannel.OnEventRaised += GameDataEventCalled;
    }

    private void OnDisable()
    {
        voidChannel.OnEventRaised -= EventCalled;
        gameDataChannel.OnEventRaised -= GameDataEventCalled;
    }

    private void EventCalled()
    {
        _currentJumps++;
        if (_currentJumps == achievementJumps)
            Debug.Log("Achievement Unlocked. Player jumped " + achievementJumps + " times");
    }

    private void GameDataEventCalled(GameData data)
    {
        Debug.Log("Event with GameData data passed on for filename " + data.fileName);
    }
}