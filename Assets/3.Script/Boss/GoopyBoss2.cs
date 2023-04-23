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
    //�����Ǹ� �����̱� ����
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
        //ü���� 0�����϶�
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
        //�������� �����϶�
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
        //���������� �����϶�
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

        //���� 3�� �����̰� �� ����
        if (attack_count >= Randomcount)
        {
            //�÷��̾�� ����������
            if (target.transform.position.x - transform.position.x >= -2f && target.transform.position.x - transform.position.x <= 2f)
            {
                //���� ����
                StartCoroutine(Attack_Pattren());
                StopCoroutine(Attack_Pattren());
            }
        }
    }

    //���� ������ ���� �޼���
    private void Boss2_Move_Start()
    {
        animator.SetBool("Touch", false);
        animator.SetBool("Move_Left", true);
    }

    //�� �浹�� �ݴ��� �̵�
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

    //���� �ε����� ��� �ִϸ��̼� ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.transform.CompareTag("Ground")))
        {
            animator.SetBool("Touch", true);
        }
    }

    //���� �ڷ�ƾ
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
    //�����Ҷ��� ��ü ����
    void Boss2_Attack()
    {
        animator.SetBool("Attack_0",true);
        Invoke("Boss2_Attack_On", 0.7f);
        Invoke("Boss2_Attack_Off", 1f);
    }

    //�����Ҷ��� ����
    void Boss2_Attack_On()
    {
        gameObject.layer = 9;
    }
    //���� �� ����
    void Boss2_Attack_Off()
    {
        gameObject.layer = 7;
    }


}
