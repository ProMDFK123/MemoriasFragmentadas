using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [Header("Componentes")]
    public GameSaver gameSaver;

    [Header("Escenas")]
    public string mainScene = "MainMenu";
    public string nextScene = "LevelSelector";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    void ExitGame()
    {
        if (gameSaver != null)
        {
            gameSaver.SaveGame();
        }

        string curScene = SceneManager.GetActiveScene().name;
        if (curScene == mainScene)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(mainScene);
        }
    }
}
