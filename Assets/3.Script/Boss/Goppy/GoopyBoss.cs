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
        //체력이 0 이하가 되면 코루팅 중지 후 죽는 모션 출력
        if (Currenthp <= 0 && !OnDie)
        {
            //3페이즈
            GameManager.Instance.curPhase = 3;
            //코루틴 정지
            StopCoroutine(coroutine);

            //한번만 실행, 죽는 애니메이션 실행
            OnDie = true;
            animator.SetBool("Ph2_Die", true);

            //목소리와 효과음 실행
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
        //벽에서 팅기기
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
        //3페이즈 보스한테 깔리고 1초 뒤에 사라짐
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

    //점프
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

    //펀치 공격
    void PunchAttack()
    {
        forceTarget(target);
        animator.SetTrigger("Punch");
    }

    //1, 2페이즈 공격패턴
    IEnumerator AttackPattern_co()
    {
        while (true)
        {
            //인트로 or 공격 모션 출력됨
            //yield return new WaitForSeconds(3f);
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

            //점프 카운트가 0이하면 펀치 출격
            while (jump_count >= 0)
            {

                //체력이 200이하로 내려갔는데, 2페가 아닌 상태면 2페 시작
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
                //점프
                Jump();
                //점프 할때마다 점프 카운트가 줄고, 점프 파워값이 랜덤으로 바뀜
                jump_count--;
                jumpPower = UnityEngine.Random.Range(1200f, 1400f);
                //1.5초마다 점프 반복
                yield return new WaitForSeconds(1.5f);
            }
            //점프 패턴 다 끝나면 점프 카운트가 랜덤으로 바뀜
            jump_count = UnityEngine.Random.Range(1, 5);
            //점프가 끝나면 펀치공격 실행
            PunchAttack();
        }
    }








}
