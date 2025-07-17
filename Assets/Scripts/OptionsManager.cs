using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public TMP_Dropdown FPSDropdown;

    private void Start()
    {
        //Cargar configuraciones guardadas
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        int savedFPSIndex = PlayerPrefs.GetInt("FPSIndex", 1);

        volumeSlider.value = savedVolume;
        FPSDropdown.value = savedFPSIndex;

        ApplyVolume(savedVolume);
        ApplyFPS(savedFPSIndex);

        //Asignar eventos a los controles
        volumeSlider.onValueChanged.AddListener(ApplyVolume);
        FPSDropdown.onValueChanged.AddListener(ApplyFPS);
    }

    public void ApplyVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
        Debug.Log("Volumen aplicado: " + volume);
    }

    public void ApplyFPS(int index)
    {
        int fps = 60;

        switch (index)
        {
            case 0: fps = 30; break;
            case 1: fps = 60; break;
            case 2: fps = 120; break;
            case 3: fps = -1; break;
        }

        Application.targetFrameRate = fps;
        PlayerPrefs.SetInt("FPSIndex", index);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
