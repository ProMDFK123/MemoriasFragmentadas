using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    [Header("Estado de la puerta")]
    public bool isOpen = false;
    public Transform teleportTarget;
    public Transform cameraTargetPosition; // nueva posición para la cámara

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string lockedMessage = "La puerta está cerrada.";
    public float messageDuration = 2f;

    [Header("Sonidos")]
    public AudioClip doorOpenSound;
    public AudioClip doorLockedSound;
    private AudioSource audioSource;

    private bool playerInRange = false;
    private GameObject player;
    private bool showingMessage = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            if (isOpen)
            {
                PlaySound(doorOpenSound);
                TeleportPlayerAndCamera();
            }
            else if (!showingMessage)
            {
                PlaySound(doorLockedSound);
                StartCoroutine(ShowLockedMessage());
            }
        }
    }

    private void TeleportPlayerAndCamera()
    {
        if (player != null && teleportTarget != null)
        {
            player.transform.position = teleportTarget.position;

            // mover cámara si hay destino asignado
            if (cameraTargetPosition != null)
            {
                Camera.main.transform.position = new Vector3(
                    cameraTargetPosition.position.x,
                    cameraTargetPosition.position.y,
                    Camera.main.transform.position.z // mantener z
                );
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    private System.Collections.IEnumerator ShowLockedMessage()
    {
        showingMessage = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = lockedMessage;
        yield return new WaitForSecondsRealtime(messageDuration);
        dialoguePanel.SetActive(false);
        showingMessage = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }
}
