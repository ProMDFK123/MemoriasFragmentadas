using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Escenas")]
    public string optionsSceneName = "Options";
    public string startGameSceneName = "Intro1";

    [Header("Paneles")]
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;

    public void Start()
    {
        ShowMainMenu();
    }

    public void NewGame()
    {
        //Borrar datos del juego anterior
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");

        //Cargar la escena de inicio del juego
        SceneManager.LoadScene(startGameSceneName);
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            //Cargar la escena guardada
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
        }
        else
        {
            Debug.LogWarning("No hay partida guardada para continuar.");
        }
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene(optionsSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
