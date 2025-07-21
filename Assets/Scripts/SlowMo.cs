using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    [Range(0f, 1f)]
    public float slowFactor = 0.5f; // 0.5 = mitad de la velocidad normal

    void Start()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // ajusta física
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
