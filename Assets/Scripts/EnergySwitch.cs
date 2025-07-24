using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySwitch : MonoBehaviour
{
    public GameObject[] lights;
    public GameObject[] fragments;
    public AudioSource switchSound;
    public GameObject interactIcon;

    private bool activated = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) interactIcon?.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) interactIcon?.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Return))
        {
            activated = true;

            foreach (GameObject light in lights) light.SetActive(true);

            foreach (GameObject fragment in fragments) fragment.SetActive(true);

            switchSound?.Play();

            interactIcon?.SetActive(false);
        }
    }
}
