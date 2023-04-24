using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : Singleton<Player_State>
{

    //플레이어 기본 위치
    public Vector3 playerPostion = new Vector3(-19.91f, 5.82f, 0f);

    public int MaxLife = 3;
    public int coin = 0;

    //플레이어가 보스를 깼는지 확인
    public bool FirstBoss = false;
    public bool SecoundBoss = false;
}
