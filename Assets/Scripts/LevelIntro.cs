using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelIntro : MonoBehaviour
{
    [Header("Componentes")]
    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI levelIntroText;
    public CanvasGroup levelGroup;
    public CanvasGroup introGroup;
    public CanvasGroup bgGroup;

    [Header("Información del nivel")]
    public string levelName;
    [TextArea(3, 6)] public string levelIntro;

    [Header("Configuraciones")]
    public float fadeDuration = 1f;
    public float displayDuration = 2f;
    public float introTime = 5f;

    void Start()
    {
        Time.timeScale = 0f; // Pausar el juego al inicio

        levelNameText.text = levelName;
        levelIntroText.text = levelIntro;

        levelGroup.alpha = 0f;
        introGroup.alpha = 0f;
        bgGroup.alpha = 1f;

        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        //Mostrar el nombre del nivel
        yield return StartCoroutine(FadeIn(levelGroup));
        Debug.Log("Nombre del nivel mostrado: " + levelName);
        yield return new WaitForSecondsRealtime(displayDuration);
        yield return StartCoroutine(FadeOut(levelGroup));

        //Mostrar la introducción del nivel
        yield return StartCoroutine(FadeIn(introGroup));
        Debug.Log("Introducción del nivel mostrada: " + levelIntro);
        yield return new WaitForSecondsRealtime(introTime);
        yield return StartCoroutine(FadeOut(introGroup));

        //Ocultar el fondo
        yield return StartCoroutine(FadeOut(bgGroup));

        Debug.Log("Fin intro, empieza el juego");
        //Reanudar el juego
        Time.timeScale = 1f;
    }

    IEnumerator FadeIn(CanvasGroup group)
    {
        float elapsedTime = 0f;
        group.alpha = 0f;
        group.gameObject.SetActive(true);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            group.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }

        group.alpha = 1f;
    }

    IEnumerator FadeOut(CanvasGroup group)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            group.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            yield return null;
        }

        group.alpha = 0f;
        group.gameObject.SetActive(false);
    }
}
