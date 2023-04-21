using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject Boss1;
    [SerializeField] private GameObject Boss2;
    [SerializeField] private GoopyBoss Boss1_Script;

    bool Boss_go = false;


    // Update is called once per frame
    void Update()
    {
        //3페 보스 출격
        if (Boss1_Script.Currenthp <= 0 && !Boss_go)
        {
            Invoke("Boss_Go", 4.5f);
            Boss_go = true;
        }
    }

    //3페 보스 출격!
    void Boss_Go()
    {
        moveTarget();
        Boss2.SetActive(true);
    }

    //보스 추격하기
    public void moveTarget()
    {
        Vector2 currentBoss = Boss1.transform.position;

        Boss2.transform.position = currentBoss + new Vector2(0, 10f);
        //float dir = Boss1.transform.position.x - Boss2.transform.position.x;
        //dir = (dir < 0) ? -1 : 1;
    }
}
