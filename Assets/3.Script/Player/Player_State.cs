using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : Singleton<Player_State>
{

    //�÷��̾� �⺻ ��ġ
    public Vector3 playerPostion = new Vector3(-19.91f, 5.82f, 0f);

    public int MaxLife = 3;
    public int coin = 0;

    //�÷��̾ ������ ������ Ȯ��
    public bool FirstBoss = false;
    public bool SecoundBoss = false;
}
