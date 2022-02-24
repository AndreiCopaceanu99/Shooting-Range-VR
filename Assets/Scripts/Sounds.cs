using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.volume = Random.Range(0.7f, 0.9f);
            //audioSource.pitch = Random.Range(0.3f, 0.7f);
            audioSource.Play();
        }
    }
}
