using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneUI : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip Intro;
    public AudioClip GameOver;
    public AudioClip Outro;

    private GameState curState = GameState.TRANSITION_IN;

    public GameState CurState
    {
        get { return curState; }
        set
        {
            curState = value;

            switch (curState)
            {
                case GameState.INTRO:
                    UI[(int)GameState.INTRO].SetActive(true);
                    break;
                case GameState.GAMEOVER:
                    UI[(int)GameState.GAMEOVER].SetActive(true);
                    break;
                case GameState.OUTRO:
                    UI[(int)GameState.OUTRO].SetActive(true);
                    break;
                case GameState.TRANSITION_OUT:
                    Loading.nextSceneName = "Map";
                    UI[(int)GameState.TRANSITION_OUT].GetComponent<Iris_In_Scene>().nextSceneName = "Loading";
                    UI[(int)GameState.TRANSITION_OUT].SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
    public PlayerMovement player;
    public GameObject[] UI = new GameObject[5];

    private void Awake()
    {
        GameManager.Instance.UI = this;
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
        TryGetComponent(out audioSource);
    }


    void Update()
    {
        Debug.Log(GameManager.Instance.CurState + "<< 지금 상태 ");
        if (player.currenthp <= 0)
        {
            GameManager.Instance.CurState = GameState.GAMEOVER;
        }
    }

    public void OnTransitionEnd()
    {
        GameManager.Instance.CurState = GameState.INTRO;
        UI[0].SetActive(false);
    }

    public void OnOutroEnd()
    {
        GameManager.Instance.CurState = GameState.TRANSITION_OUT;
    }
}
