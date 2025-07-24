using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FragmentPick : MonoBehaviour
{
    [Header("SFX")]
    public AudioClip pickupSound;

    [Header("Dialogo")]
    [TextArea] public string text;
    public float duration = 2.5f;

    [Header("UI")]
    public GameObject panel;
    public TextMeshProUGUI dialogueTxt;

    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected || !other.CompareTag("Player")) return;
        collected = true;

        // Reproducir sonido
        AudioSource audioSource = FindObjectOfType<AudioSource>();
        if (pickupSound && audioSource)
            audioSource.PlayOneShot(pickupSound);

        // Actualizar progreso
        FragmentManager manager = FindObjectOfType<FragmentManager>();
        if (manager != null)
            manager.CollectFragment();

        // Mostrar diálogo
        if (panel != null && dialogueTxt != null)
            StartCoroutine(ShowDialogue());

        // Ocultar fragmento
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator ShowDialogue()
    {
        panel.SetActive(true);
        dialogueTxt.text = text;
        yield return new WaitForSeconds(duration);
        panel.SetActive(false);
    }
}
