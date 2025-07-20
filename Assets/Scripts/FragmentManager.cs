using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FragmentManager : MonoBehaviour
{
    [Header("Fragmentos")]
    public int totalFragments;
    private int collectedFragments = 0;

    [Header("UI")]
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public CanvasGroup outroCG;
    public TextMeshProUGUI outroText;
    public float dialogueDuration = 2.5f;
    public float outroDuration = 5f;
    public float fadeDuration = 1f;

    [Header("Escena siguiente")]
    public string nextSceneName = "LevelSelector";

    [Header("GameSaver")]
    public GameSaver gameSaver;

    void Start()
    {
        dialoguePanel.SetActive(false);

        if (outroCG != null)
        {
            outroCG.alpha = 0f;
            outroCG.gameObject.SetActive(false);
        }
    }

    public void GetFragment(string msg)
    {
        collectedFragments++;
        StartCoroutine(ShowFragmentDialogue(msg));

        if (collectedFragments >= totalFragments)
        {
            StartCoroutine(ShowOutro());
        }
    }

    private IEnumerator ShowFragmentDialogue(string msg)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = msg;

        yield return new WaitForSeconds(dialogueDuration);

        dialoguePanel.SetActive(false);
    }

    private IEnumerator ShowOutro()
    {
        yield return new WaitForSeconds(1f);

        if (outroCG != null)
        {
            outroCG.gameObject.SetActive(true);

            //Fade-in
            float t = 0f;
            while (t < fadeDuration)
            {
                outroCG.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
                t += Time.deltaTime;
                yield return null;
            }

            outroCG.alpha = 1f;
            yield return new WaitForSeconds(outroDuration);

            //Fade-out
            t = 0f;
            while (t < fadeDuration)
            {
                outroCG.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
                t += Time.deltaTime;
                yield return null;
            }

            outroCG.alpha = 0f;
        }

        if (gameSaver != null)
        {
            gameSaver.SaveGame();
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneName);
    }
}
