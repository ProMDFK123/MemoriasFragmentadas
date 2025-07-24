using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject bg;
    public TextMeshProUGUI outroTxt;
    [TextArea] public string finalText = "nivel completado";
    public float display = 3f;
    public string nextScene = "LevelSelector";
    public GameSaver saver;

    public void CompleteLevel()
    {
        StartCoroutine(ShowOutro());
    }

    IEnumerator ShowOutro()
    {
        bg.SetActive(true);
        outroTxt.text = finalText;

        if (saver != null) saver.SaveGame();

        yield return new WaitForSeconds(display);

        SceneManager.LoadScene(nextScene);
    }
}
