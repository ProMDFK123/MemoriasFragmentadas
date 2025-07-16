using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    public FPSLimiter instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetFPS(int targetFPS)
    {
        Application.targetFrameRate = targetFPS;
    }

    private void Start()
    {
        int savedFPS = PlayerPrefs.GetInt("TargetFPS", 60);
        SetFPS(savedFPS);
    }
}
