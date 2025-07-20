using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    [TextArea] public string fragmentText;

    private FragmentManager fm;

    void Start()
    {
        fm = FindObjectOfType<FragmentManager>();
        if (fm == null)
        {
            Debug.LogError("FragmentManager not found in the scene.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fm != null)
            {
                fm.GetFragment(fragmentText);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("FragmentManager is not assigned.");
            }
        }
    }
}
