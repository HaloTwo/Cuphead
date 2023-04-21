using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    TRANSITION_IN,    //들어가는 화면
    INTRO,            //인트로
    INGAME,           //배틀
    GAMEOVER,         //패배 시
    OUTRO,            //승리 시
    TRANSITION_OUT,   //나가는 화면
}


public class GameManager : Singleton<GameManager>
{
    public Vector3 playerPosition;
    public GameObject player;
    public GameObject playerIntro;
    public BossSceneUI UI;
    public int curPhase;

    //맵에서 첫번째 보스 깻는지
    public bool FirstBoss = false;

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
                    UI.CurState = GameState.INTRO;
                    playerIntro.GetComponent<Animator>().SetTrigger("Intro");
                    break;

                case GameState.INGAME:
                    playerIntro.SetActive(false);
                    player.SetActive(true);
                    UI.CurState = GameState.INGAME;
                    break;

                case GameState.GAMEOVER:
                    UI.CurState = GameState.GAMEOVER;
                    break;
                case GameState.OUTRO:
                    UI.CurState = GameState.OUTRO;
                    break;
                case GameState.TRANSITION_OUT:
                    UI.CurState = GameState.TRANSITION_OUT;
                    break;

                default:
                    break;
            }
        }
    }
}
