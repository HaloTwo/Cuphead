using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;

public class GoopyBoss2 : Boss
{
    [SerializeField] private GoopyBoss Boss;
    [SerializeField] private float bossMoveSpeed = 10f;
    Vector2 Dir = Vector2.left;

    [SerializeField] private int attack_count = 0;
    [SerializeField] private int Randomcount = 3;

    //�����Ǹ� �����̱� ����
    private void OnEnable()
    {
        Invoke("Boss2_Move_Start", 2.5f);
    }

    private void Update()
    {
        //ü���� 0�����϶�
        if (Currenthp <= 0)
        {
            animator.SetBool("Die", true);
            Dir = Vector2.zero;
            GameManager.Instance.FirstBoss = true;
            GameManager.Instance.CurState = GameState.OUTRO;
        }
    }

    private void FixedUpdate()
    {
        //�������� �����϶�
        if (animator.GetBool("Move_Left") == true)
        {
            transform.Translate(Dir * bossMoveSpeed * Time.deltaTime);
        }
        //���������� �����϶�
        if (animator.GetBool("Move_Right") == true)
        {
            transform.Translate(-Dir * bossMoveSpeed * Time.deltaTime);
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
            attack_count++;
        }
        else if (collision.CompareTag("Wall") && animator.GetBool("Move_Right") == true)
        {
            animator.SetBool("Move_Left", true);
            animator.SetBool("Move_Right", false);
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
