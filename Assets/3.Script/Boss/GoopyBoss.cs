using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GoopyBoss : Boss
{
    [SerializeField] private float jumpPower = 1300f;
    [SerializeField] private float bossMoveSpeed = 600;
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;
    private CapsuleCollider2D capsuleCollider2D;
    private AudioSource jumpSource;


    [SerializeField] private Animator Jump_animator;
    public AudioClip introCilp;
    public AudioClip DeathCilp;
    public AudioClip DeathVoice_Cilp;
    public AudioClip JumpCilp;
    public AudioClip Ground;
    public AudioClip PunchCilp;
    public AudioClip TransCilp;
    public AudioClip Ph2_JumpCilp;
    public AudioClip Ph2_GroundCilp;
    public AudioClip Ph2_PunchCilp;


   // private bool jump_check = false;
    private int jump_count = 3;
    private Vector2 bossMove = Vector2.left;
    private bool OnDie = false;

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
        GameObject.Find("Goppy_Jump_Dust").TryGetComponent(out jumpSource);

        coroutine = AttackPattern_co();
        StartCoroutine(coroutine);
        GameManager.Instance.curPhase = 1;
    }

    private void FixedUpdate()
    {
        //ü���� 0 ���ϰ� �Ǹ� �ڷ��� ���� �� �״� ��� ���
        if (Currenthp <= 0 && !OnDie)
        {
            //3������
            GameManager.Instance.curPhase = 3;
            //�ڷ�ƾ ����
            StopCoroutine(coroutine);

            //�ѹ��� ����, �״� �ִϸ��̼� ����
            OnDie = true;
            animator.SetBool("Ph2_Die", true);

            //��Ҹ��� ȿ���� ����
            jumpSource.clip = DeathCilp;
            jumpSource.Play();

            audioSource.loop = true;
            audioSource.clip = DeathVoice_Cilp;
            audioSource.Play();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Jump_animator.SetTrigger("Touch");

            if (animator.GetBool("Ph2_Trans"))
            {
                audioSource.clip = Ph2_GroundCilp;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = Ground;
                audioSource.Play();
            }
        }
        boxCollider2D.enabled = false;
       // jump_check = false;
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

    void Destory_Parrying_Obj()
    {
        parrying_obj1.SetActive(false);
        parrying_obj2.SetActive(false);
        parrying_obj3.SetActive(false);
    }

    void intro_Animation()
    {
        audioSource.clip = introCilp;
        audioSource.Play();
    }

    //����
    private void Jump()
    {
        if (!OnDie)
        {
            animator.SetTrigger("Jump");

            if (animator.GetBool("Ph2_Trans"))
            {
                audioSource.clip = Ph2_JumpCilp;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = JumpCilp;
                audioSource.Play();
            }


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

    void Ph2_Punch_Sound()
    {
        jumpSource.clip = Ph2_PunchCilp;
        jumpSource.Play();
    }

    void Punch_Sound()
    {
        jumpSource.clip = PunchCilp;
        jumpSource.Play();
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
            //yield return new WaitForSeconds(3f);
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

            //���� ī��Ʈ�� 0���ϸ� ��ġ ���
            while (jump_count >= 0)
            {

                //ü���� 200���Ϸ� �������µ�, 2�䰡 �ƴ� ���¸� 2�� ����
                if (Currenthp <= 200 && animator.GetBool("Ph2_Trans") == false)
                {
                    GameManager.Instance.curPhase = 2;
                    forceTarget(target);
                    animator.SetBool("Ph2_Trans", true);
                    Jump_animator.SetBool("ph2", true);
                    audioSource.clip = TransCilp;
                    audioSource.Play();
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
