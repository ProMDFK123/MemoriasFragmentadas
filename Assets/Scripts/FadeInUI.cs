using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInUI : MonoBehaviour
{
    public Image background;
    public TextMeshProUGUI text;
    public TextMeshProUGUI continueText;
    public float fadeDuration = 2f;
    public bool readyToContinue = false;
    public string nextSceneName;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        Color panelColor = background.color;
        Color textColor = text.color;

        panelColor.a = 0f;
        textColor.a = 0f;

        background.color = panelColor;
        text.color = textColor;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);

            panelColor.a = alpha;
            textColor.a = alpha;

            background.color = panelColor;
            text.color = textColor;

            yield return null;
        }

        StartCoroutine(ShowContinueText());
    }

    IEnumerator ShowContinueText()
    {
        continueText.gameObject.SetActive(true);
        Color color = continueText.color;
        color.a = 0f;
        continueText.color = color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);

            color.a = alpha;
            continueText.color = color;

            yield return null;
        }

        readyToContinue = true;
    }

    void Update()
    {
        if (readyToContinue && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}