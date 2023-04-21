using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Intro : MonoBehaviour
{
        private void Awake()
    {
        GameManager.Instance.playerIntro = gameObject;
    }

    public void OnIntroEnd()
    {
        GameManager.Instance.CurState = GameState.INGAME;
    }
}
