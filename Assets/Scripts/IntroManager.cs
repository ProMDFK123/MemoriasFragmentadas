using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [Header("Canvas")]
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI introTxt;
    public Image blackBG;

    [Header("Tiempos")]
    public float fadeTime = 1f;
    public float nameTime = 2f;
    public float introTime = 5f;

    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        //Estado inicial
        SetAlpha(nameTxt, 0);
        SetAlpha(introTxt, 0);
        SetAlpha(blackBG, 1);

        //Fade titulo
        yield return FadeIn(nameTxt);
        yield return new WaitForSeconds(nameTime);
        yield return FadeOutTxt(nameTxt);

        //Fade intro
        yield return FadeIn(introTxt);
        yield return new WaitForSeconds(introTime);
        yield return FadeOutTxt(introTxt);

        //Fade-out fondo
        yield return FadeOutImg(blackBG);
    }

    void SetAlpha(Graphic graphic, float alpha)
    {
        var color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }

    IEnumerator FadeIn(TextMeshProUGUI text)
    {
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            SetAlpha(text, Mathf.Lerp(0, 1, t / fadeTime));
            yield return null;
        }
        SetAlpha(text, 1);
    }

    IEnumerator FadeOutTxt(TextMeshProUGUI text)
    {
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            SetAlpha(text, Mathf.Lerp(1, 0, t / fadeTime));
            yield return null;
        }
        SetAlpha(text, 0);
    }

    IEnumerator FadeOutImg(Image img)
    {
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            SetAlpha(img, Mathf.Lerp(1, 0, t / fadeTime));
            yield return null;
        }
        SetAlpha(img, 0);
    }
}
