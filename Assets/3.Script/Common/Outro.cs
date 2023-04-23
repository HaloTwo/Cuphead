using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outro : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip knock_out;

    private void Awake()
    {
        TryGetComponent(out audioSource);
    }

    public void OnAnimationEnd()
    {
        GameManager.Instance.CurState = GameState.TRANSITION_OUT;
        gameObject.SetActive(false);
    }

    public void Knock_Out_Voice()
    {
        audioSource.clip = knock_out;
        audioSource.Play();
    }
}
