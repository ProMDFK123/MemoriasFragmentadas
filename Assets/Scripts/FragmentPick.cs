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
    [TextArea] public List<string> textLines = new List<string>();

    [Header("UI")]
    public GameObject panel;
    public TextMeshProUGUI dialogueTxt;
    public float duration = 2f;

    private bool collected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collected || !other.CompareTag("Player")) return;
        collected = true;

        // Reproducir sonido al recoger
        AudioSource audioSource = FindObjectOfType<AudioSource>();
        if (pickupSound != null && audioSource != null)
            audioSource.PlayOneShot(pickupSound);

        // Ocultar visualmente el fragmento
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Mostrar diálogo y luego actualizar progreso
        StartCoroutine(HandleFragmentPickup());
    }

    IEnumerator HandleFragmentPickup()
    {
        yield return StartCoroutine(ShowDialogue());

        FragmentManager manager = FindObjectOfType<FragmentManager>();
        if (manager != null)
        {
            manager.CollectFragment();
        }
    }

    IEnumerator ShowDialogue()
    {
        panel.SetActive(true);

        foreach (string line in textLines)
        {
            dialogueTxt.text = line;
            yield return new WaitForSeconds(duration);
        }

        panel.SetActive(false);
    }
}
