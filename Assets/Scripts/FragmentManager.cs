using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentManager : MonoBehaviour
{
    public int total = 2;
    private int collected = 0;

    public LevelManager completed;
    public AudioSource completedSound;

    public void CollectFragment()
    {
        collected++;

        if (collected >= total)
        {
            if (completedSound != null) completedSound.Play();
            if (completed != null) completed.CompleteLevel();
        }
    }
}
