using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    TRANSITION_IN,    //���� ȭ��
    INTRO,            //��Ʈ��
    INGAME,           //��Ʋ
    GAMEOVER,         //�й� ��
    OUTRO,            //�¸� ��
    TRANSITION_OUT,   //������ ȭ��
}


public class GameManager : Singleton<GameManager>
{
    public Vector3 playerPosition;
    public GameObject player;
    public GameObject playerIntro;
    public BossSceneUI UI;
    public int curPhase;

    //�ʿ��� ù��° ���� ������
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
