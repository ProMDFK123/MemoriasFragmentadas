using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public string menuScene = "MainMenu";
    public string lvlsScene = "LevelSelector";
    public GameSaver saver;

    void Start()
    {
        if (saver == null) saver = FindObjectOfType<GameSaver>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (saver != null) saver.SaveGame();

            string curScene = SceneManager.GetActiveScene().name;
            
            if (curScene == lvlsScene) SceneManager.LoadScene(menuScene);
            else SceneManager.LoadScene(lvlsScene);
        }
    }
}
