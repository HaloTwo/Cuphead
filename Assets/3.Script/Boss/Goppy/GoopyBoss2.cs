using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GoopyBoss2 : Boss
{
    [SerializeField] private GoopyBoss Boss;
    [SerializeField] private float bossMoveSpeed = 10f;
    Vector2 Dir = Vector2.left;

    [SerializeField] private int attack_count = 0;
    [SerializeField] private int Randomcount = 3;

    public AudioClip DeathCilp;
    public AudioClip DropCilp;
    public AudioClip Move1;
    public AudioClip Move2;
    public AudioClip Splat;
    public AudioClip SplatStart;

    private bool OnDie = false;
    private bool Right_Move = true;
    private bool Left_Move = true;
    //생성되면 움직이기 실행
    private void OnEnable()
    {
        audioSource.clip = DropCilp;
        audioSource.Play();
        Invoke("Boss2_Move_Start", 2.5f);
    }

    void Animation_Attack_Start()
    {
        audioSource.clip = SplatStart;
        audioSource.Play();
    }
    void Animation_Attack()
    {
        audioSource.clip = Splat;
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        //체력이 0이하일때
        if (Currenthp <= 0 && !OnDie)
        {
            OnDie = true;
            animator.SetBool("Die", true);
            audioSource.clip = DeathCilp;
            audioSource.Play();
            Dir = Vector2.zero;
            GameManager.Instance.FirstBoss = true;
            GameManager.Instance.CurState = GameState.OUTRO;
        }
        //왼쪽으로 움직일때
        else if (animator.GetBool("Move_Left"))
        {
            transform.Translate(Dir * bossMoveSpeed * Time.deltaTime);

            if (Left_Move)
            {
                audioSource.clip = Move1;
                audioSource.Play();
                Left_Move = false;
            }
        }
        //오른쪽으로 움직일때
        else if (animator.GetBool("Move_Right"))
        {
            transform.Translate(-Dir * bossMoveSpeed * Time.deltaTime);

            if (Right_Move)
            {
                audioSource.clip = Move2;
                audioSource.Play();
                Right_Move = false;
            }
        }

        //벽에 3번 움직이고 난 다음
        if (attack_count >= Randomcount)
        {
            //플레이어와 겹쳐졌을때
            if (target.transform.position.x - transform.position.x >= -2f && target.transform.position.x - transform.position.x <= 2f)
            {
                //공격 실행
                StartCoroutine(Attack_Pattren());
                StopCoroutine(Attack_Pattren());
            }
        }
    }

    //보스 움직임 시작 메서드
    private void Boss2_Move_Start()
    {
        animator.SetBool("Touch", false);
        animator.SetBool("Move_Left", true);
    }

    //벽 충돌시 반대편 이동
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Randomcount = Random.Range(2, 5);
        if (collision.CompareTag("Wall") && animator.GetBool("Move_Left") == true)
        {
            animator.SetBool("Move_Left", false);
            animator.SetBool("Move_Right", true);
            Left_Move = true;
            attack_count++;
        }
        else if (collision.CompareTag("Wall") && animator.GetBool("Move_Right") == true)
        {
            animator.SetBool("Move_Left", true);
            animator.SetBool("Move_Right", false);
            Right_Move = true;
            attack_count++;
        }
    }

    //땅에 부딪히면 충격 애니메이션 출력
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.CompareTag("Ground")))
        {
            animator.SetBool("Touch", true);
        }
    }

    //공격 코루틴
    IEnumerator Attack_Pattren()
    {
        attack_count = 0;
        Dir = Vector2.zero;
        Invoke("Boss2_Attack", 0.2f);
        yield return new WaitForSeconds(1.5f);
        //yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_ph3_Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        animator.SetBool("Attack_0", false);
        Dir = Vector2.left;
    }
    //공격할때만 실체 등장
    void Boss2_Attack()
    {
        animator.SetBool("Attack_0",true);
        Invoke("Boss2_Attack_On", 0.7f);
        Invoke("Boss2_Attack_Off", 1f);
    }

    //공격할때만 등장
    void Boss2_Attack_On()
    {
        gameObject.layer = 9;
    }
    //공격 후 퇴장
    void Boss2_Attack_Off()
    {
        gameObject.layer = 7;
    }


}
