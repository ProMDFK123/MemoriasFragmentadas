using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MirrorInteraction : MonoBehaviour
{
    [Header("Helper")]
    public GameObject enterIcon;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string dialogue;

    [Header("Crack")]
    public GameObject crack;
    public AudioSource audioSource;
    public AudioClip crackSound;
    public float crackDelay = 3f;

    private bool playerInRange = false;
    private bool interacted = false;

    void Update()
    {
        if (playerInRange && !interacted && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Interaction());
        }
    }

    IEnumerator Interaction()
    {
        interacted = true;
        enterIcon.SetActive(false);
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogue;

        yield return new WaitForSeconds(crackDelay);

        dialoguePanel.SetActive(false);
        crack.SetActive(true);
        if (audioSource && crackSound)
        {
            audioSource.PlayOneShot(crackSound);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !interacted)
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
