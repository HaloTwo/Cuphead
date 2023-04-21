using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outro : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        GameManager.Instance.CurState = GameState.TRANSITION_OUT;
        gameObject.SetActive(false);
    }
}
