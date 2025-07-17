using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultConfiguration : MonoBehaviour
{
    void Awake()
    {
        float vol = PlayerPrefs.GetFloat("Volume", 1f);
        int fpsIndex = PlayerPrefs.GetInt("FPSIndex", 1);

        AudioListener.volume = vol;

        int fps = 60;
        switch (fpsIndex)
        {
            case 0: fps = 30; break;
            case 1: fps = 60; break;
            case 2: fps = 120; break;
            case 3: fps = -1; break;
        }

        Application.targetFrameRate = fps;
    }
}
