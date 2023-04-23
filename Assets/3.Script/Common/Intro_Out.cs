using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Out : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] Intro;
    public AudioClip[] Intro_Start;

    private void Awake()
    {
        TryGetComponent(out audioSource);
    }

    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }

    public void OnIntro_Voice()
    {
        int index = Random.Range(0, Intro.Length);
        audioSource.clip = Intro[index];
        audioSource.Play();
    }

    public void OnIntro_Start_Voice()
    {
        int index = Random.Range(0, Intro_Start.Length);
        audioSource.clip = Intro_Start[index];
        audioSource.Play();
    }
}
