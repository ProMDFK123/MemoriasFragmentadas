using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockedDoor : MonoBehaviour
{
    [Header("Configuración")]
    public string requiredKeyID;
    public GameObject doorToOpen;
    public AudioSource audio;

    [Header("UI")]
    public GameObject dialogueBox;    
    public TextMeshProUGUI dialogueText;          
    public float dialogueDuration = 2f;
    public GameObject interactIcon;

    private bool playerInRange;
    private PlayerInventory inventory;

    void Start()
    {
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            if (inventory != null && inventory.HasItem(requiredKeyID))
            {
                OpenDoor();
            }
            else
            {
                ShowLockedMessage();
            }
        }
    }

    private void OpenDoor()
    {
        if (doorToOpen != null)
        {
            if (audio != null) audio.Play();
            doorToOpen.SetActive(false);
            interactIcon.SetActive(false);
        }

        Debug.Log("Puerta abierta.");
    }

    private void ShowLockedMessage()
    {
        StopAllCoroutines();
        StartCoroutine(ShowDialogue("Está cerrada... Parece que necesitas una llave."));
    }

    IEnumerator ShowDialogue(string message)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = message;
        yield return new WaitForSeconds(dialogueDuration);
        dialogueBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactIcon.SetActive(true);
            inventory = other.GetComponent<PlayerInventory>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactIcon.SetActive(false);
            inventory = null;
        }
    }
}
