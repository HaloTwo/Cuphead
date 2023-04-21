using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Transition_In : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        GameManager.Instance.CurState = GameState.INTRO;
        gameObject.SetActive(false);
    }
}
