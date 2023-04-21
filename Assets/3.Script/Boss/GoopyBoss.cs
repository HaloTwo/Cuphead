using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GoopyBoss : Boss
{
    [SerializeField] private Animator Jump_animator;
    [SerializeField] private float jumpPower = 1300f;
    [SerializeField] private float bossMoveSpeed = 600;
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;
    private CapsuleCollider2D capsuleCollider2D;

    private bool jump_check = false;
    private int jump_count = 3;
    private Vector2 bossMove = Vector2.left;
    [SerializeField]
    private GameObject parrying_obj1;
    [SerializeField]
    private GameObject parrying_obj2;
    [SerializeField]
    private GameObject parrying_obj3;

    IEnumerator coroutine;


    void Start()
    {
        TryGetComponent(out boxCollider2D);
        TryGetComponent(out circleCollider2D);
        TryGetComponent(out capsuleCollider2D);


        coroutine = AttackPattern_co();
        StartCoroutine(coroutine);
        GameManager.Instance.curPhase = 1;
    }

    void Update()
    {
        //ü���� 0 ���ϰ� �Ǹ� �ڷ��� ���� �� �״� ��� ���
        if (Currenthp <= 0)
        {
            StopCoroutine(coroutine);
            animator.SetBool("Ph2_Die", true);
            GameManager.Instance.curPhase = 3;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Jump_animator.SetTrigger("Touch");
        }
        boxCollider2D.enabled = false;
        jump_check = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        boxCollider2D.enabled = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //������ �ñ��
        if (collision.transform.CompareTag("Wall"))
        {
            if (transform.localScale.x == 1.3f)
            {
                transform.localScale = (new Vector2(-1.3f, 1.3f));
                bossMove = Vector2.zero;
                rb.AddForce(new Vector2(600, 0));
                bossMove = Vector2.right;

            }
            else if (transform.localScale.x == -1.3f)
            {
                transform.localScale = (new Vector2(1.3f, 1.3f));
                bossMove = Vector2.zero;
                rb.AddForce(new Vector2(-600, 0));
                bossMove = Vector2.left;
            }

        }
        //3������ �������� �򸮰� 1�� �ڿ� �����
        if (collision.transform.CompareTag("Enemy"))
        {
            circleCollider2D.enabled = false;
            capsuleCollider2D.enabled = false;
            animator.SetTrigger("Ph3_End");
            StartCoroutine(Boss_off_co(2f));
        }

    }

    void Create_Parrying_Obj()
    {
        parrying_obj1.SetActive(true);
        parrying_obj2.SetActive(true);
        parrying_obj3.SetActive(true);
    }

    //����
    private void Jump()
    {
        if (!jump_check)
        {
            animator.SetTrigger("Jump");

            if (transform.localScale.x == 1.3f)
            {
                bossMove = Vector2.left;

            }
            if ((transform.localScale.x == -1.3f))
            {
                bossMove = Vector2.right;
            }
            rb.AddForce(bossMove * bossMoveSpeed);
            rb.AddForce(Vector2.up * jumpPower);
        }
    }

    //��ġ ����
    void PunchAttack()
    {
        forceTarget(target);
        animator.SetTrigger("Punch");
    }

    //1, 2������ ��������
    IEnumerator AttackPattern_co()
    {
        while (true)
        {
            //��Ʈ�� or ���� ��� ��µ�
            yield return new WaitForSeconds(3f);

            //���� ī��Ʈ�� 0���ϸ� ��ġ ���
            while (jump_count >= 0)
            {
                //ü���� 100���Ϸ� �������µ�, 2�䰡 �ƴ� ���¸� 2�� ����
                if (Currenthp <= 50 && animator.GetBool("Ph2_Trans") == false)
                {
                    GameManager.Instance.curPhase = 2;
                    animator.SetBool("Ph2_Trans", true);
                    Jump_animator.SetBool("ph2", true);
                    yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_ph2_Trans") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
                }
                //����
                Jump();
                //���� �Ҷ����� ���� ī��Ʈ�� �ٰ�, ���� �Ŀ����� �������� �ٲ�
                jump_count--;
                jumpPower = UnityEngine.Random.Range(1200f, 1400f);
                //1.5�ʸ��� ���� �ݺ�
                yield return new WaitForSeconds(1.5f);
            }
            //���� ���� �� ������ ���� ī��Ʈ�� �������� �ٲ�
            jump_count = UnityEngine.Random.Range(1, 5);
            //������ ������ ��ġ���� ����
            PunchAttack();
        }
    }






}
