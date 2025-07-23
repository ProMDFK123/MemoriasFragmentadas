using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrackInteraction : MonoBehaviour
{
    public GameObject enterIcon;
    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("LevelSelector");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enterIcon.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enterIcon.SetActive(false);
            playerInRange = false;
        }
    }
}
