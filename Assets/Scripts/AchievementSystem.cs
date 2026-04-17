using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private VoidEventChannel voidChannel;
    [SerializeField] private FloatEventChannel floatChannel;
    [SerializeField] private GameDataEventChannel gameDataChannel;
    [SerializeField] private int achievementJumps = 10;
    [SerializeField] private int achievementShots = 5;

    private int _currentJumps = 0;
    private int _currentShots = 0;

    private void OnEnable()
    {
        voidChannel.OnEventRaised += EventCalled;
        floatChannel.OnEventRaised += ShootEventCalled;
        gameDataChannel.OnEventRaised += GameDataEventCalled;
    }

    private void OnDisable()
    {
        voidChannel.OnEventRaised -= EventCalled;
        floatChannel.OnEventRaised -= ShootEventCalled;
        gameDataChannel.OnEventRaised -= GameDataEventCalled;
    }

    private void EventCalled()
    {
        _currentJumps++;
        if (_currentJumps == achievementJumps)
            Debug.Log("Achievement Unlocked: Player jumped " + _currentJumps + " times");
    }

    private void ShootEventCalled(float value)
    {
        _currentShots++;
        if (_currentShots == achievementShots)
            Debug.Log("Achievement Unlocked: Player made " + _currentShots + " shots.");
    }

    private void GameDataEventCalled(GameData data)
    {
        Debug.Log("Event with GameData data passed on for filename " + data.fileName);
    }
}