using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MirrorInteraction : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject grieta;
    public AudioClip crackSound;

    private bool playerInRange = false;
    private bool dialogueActive = false;
    private AudioSource audioSource;

    void Start()
    {
        dialoguePanel.SetActive(false);
        grieta.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Return) && !dialogueActive)
        {
            StartCoroutine(StartDialogue());
        }
    }

    IEnumerator StartDialogue()
    {
        dialogueActive = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = "Para recordar, primero debes perderte.";

        yield return new WaitForSeconds(3f);

        dialoguePanel.SetActive(false);
        grieta.SetActive(true);
        audioSource.PlayOneShot(crackSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
