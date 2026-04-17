using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : PersistentSingleton<Menu>
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;

    [SerializeField] private string PlayScene = "SampleScene";

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
           SceneManager.LoadScene(PlayScene); 
        });

        saveButton.onClick.AddListener(() =>
        {
            SaveLoadSystem.instance.gameData.fileName = "Menu"; 
            SaveLoadSystem.instance.gameData.sceneName = "SampleScene";
            SaveLoadSystem.instance.SaveGame();
        });

        loadButton.onClick.AddListener(() =>
        {
            SaveLoadSystem.instance.LoadGame("Menu");
        });
    }
}
