using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Movement2D movement2D;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Weapon weapon;


    [SerializeField] private Animator jump_animator;
    [SerializeField] private GameObject jump_transform;
    [SerializeField] private Animator Attack_animator;


    [SerializeField] private float jump_Force = 1400f;
    [SerializeField] private GameObject[] HP_count;
    //private AudioSource Playeraudio;

    //��ư Ŭ��
    //private bool isParry= false;

    [SerializeField]
    private int Power_Gauge;
    public int power_gauge
    {
        get
        {
            return Power_Gauge;
        }

        set
        {
            Power_Gauge = value;

            if (Power_Gauge >= 100)
            {
                Power_Gauge = 100;
            }
        }
    }


    [SerializeField]
    private int MaxHp = Player_State.Instance.MaxLife;
    [SerializeField]
    private int CurrentLife;
    public int currenthp
    {
        get
        {
            return CurrentLife;
        }
        set
        {
            CurrentLife = value;

            if (CurrentLife == 2)
            {
                HP_count[3].SetActive(false);
                HP_count[2].SetActive(true);
            }
            else if (CurrentLife == 1)
            {
                HP_count[2].SetActive(false);
                HP_count[1].SetActive(true);
            }
            else if (CurrentLife <= 0)
            {
                HP_count[1].SetActive(false);
                HP_count[0].SetActive(true);
                animator.SetBool("Die", true);
                Die = true;
            }
        }
    }

    //���Ȯ��
    bool Die = false;

    //x��ǥ
    private float x = 0;
    private float y = 0;

    //�뽬
    bool dashInput = false;
    float dashTimer;
    public float dashDelayTime = 0.5f;
    public float dashPower = 15f;
    public float dashTime = 0.2f;

    //�и�
    public float perryPower = 10f;
    public float perryTime = 0.3f;

    //������
    float special_Attack_Timer;
    public float special_Attack_Delay = 0.5f;

    //�÷��� �ν�
    [SerializeField]
    bool onPlatform = false;
    bool isjump = false;


    private void Awake()
    {
        GameManager.Instance.player = gameObject;

        TryGetComponent(out weapon);
        TryGetComponent(out boxCollider2D);
        TryGetComponent(out movement2D);
        TryGetComponent(out rb);
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);

        //TryGetComponent(out Playeraudio);

        currenthp = MaxHp;

    }

    private void OnEnable()
    {
        GameObject.Find("Jump_Dust").TryGetComponent(out jump_animator);
    }

    void Start()
    {

        //gameObject.SetActive(false);
    }


    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        //��� ��
        if (Die)
        {
            gameObject.layer = 11;
            transform.Translate(new Vector2(0f, 0.5f) * Time.deltaTime);
            x = 0;
            y = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow) && onPlatform)
        {
            StartCoroutine(Down_Jump_co());
        }

        //���� �̵�
        if (x < 0)
        {
            transform.localScale = new Vector3(-1.3f, 1.3f, 0);
        }

        //������ �̵�
        else if (x > 0)
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 0);
        }

        //�Ʒ� ����
        var currentAnimation = animator.GetCurrentAnimatorStateInfo(0);
        if (currentAnimation.IsName("Player_Duck_Shoot") || currentAnimation.IsName("Player_duck_Idle"))
        {
            animator.SetBool("isDuck", false);

        }
        else
        {
            animator.SetBool("isDuck", true);
        }

        //����
        if (Input.GetKeyDown(KeyCode.X))
        {
            weapon.startFire();
            animator.SetBool("isAttack", true);
            Attack_animator.SetBool("Shooting", true);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            weapon.stopFire();
            animator.SetBool("isAttack", false);
            Attack_animator.SetBool("Shooting", false);
        }



        //������
        special_Attack_Timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.V) && power_gauge >= 20 && special_Attack_Timer >= special_Attack_Delay)
        {
            power_gauge -= 20;
            animator.SetTrigger("Special");
            weapon.startAttack();
        }

        if (currentAnimation.IsName("Player_Special_Attack_Side_Up") ||
            currentAnimation.IsName("Player_Special_Attack_Straight") ||
            currentAnimation.IsName("Player_Special_Attack_Up") ||
            currentAnimation.IsName("Player_Special_Attack_Side_Down") ||
            currentAnimation.IsName("Player_Special_Attack_Down"))
        {
            animator.SetBool("isJump", false); 
            x = 0;
            y = 0;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            special_Attack_Timer = 0f;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        //����
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("C", true);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            animator.SetBool("C", false);
        }

        //�뽬
        dashTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer >= dashDelayTime)
        {
            dashInput = true;
            animator.SetBool("isDash", true);
            dashTimer = 0;
        }


        // ����
        if (Input.GetKeyDown(KeyCode.Z) && !animator.GetBool("isJump"))
        {
            animator.SetBool("isJump", true);

            rb.velocity = Vector2.zero;

            //�÷��̾� ������ �ٵ� �������� ���ֱ�
            rb.AddForce(new Vector2(0, jump_Force));

            //���� �� ��, �Ҹ� �����
            //Playeraudio.Play();

        }
        else if (Input.GetKeyDown(KeyCode.Z) && animator.GetBool("isJump"))
        {
            animator.SetBool("isParry", true);
            Del turnOffPerry = delegate
            {
                animator.SetBool("isParry", false);
            };
            StartCoroutine(CoWait(perryTime, turnOffPerry));
        }

        //�̵� �� �ޱ�
        animator.SetInteger("Horizontal", GetIntAxis(x));
        animator.SetInteger("Vertical", GetIntAxis(y));
    }

    void LateUpdate()
    {
        //�뽬
        if (dashInput)
        {
            //�Ĵٺ��� �������� �뽬
            rb.velocity = Vector2.right * transform.localScale.x * dashPower;
            //�Ҷ� �߷��� 0����
            rb.gravityScale = 0;
            dashInput = false;
            StartCoroutine(CoWait(dashTime, OnDashEnd));
        }

        if (animator.GetBool("isDash"))
            return;



        // Vector2 velocity = new Vector2(0, rb.velocity.y);
        //
        // if (jumpKeystate == KeyState.DOWN)
        // {
        //     velocity.y = jumpPower;
        //     jumpKeystate = KeyState.HOLD;
        // }
        // if (jumpKeystate == KeyState.UP)
        // {
        //     velocity.y *= 0.5f;
        //     jumpKeystate = KeyState.NONE;
        // }


        //c��ư�� �����ų� ���̰� 0���ϰ� �Ǹ� (���帮��)
        if (animator.GetBool("C") || animator.GetInteger("Vertical") < 0)
        {
            movement2D.MoveTo(new Vector3(0, 0, 0));
            return;
        }

        movement2D.MoveTo(new Vector3(x, 0, 0));
    }

    //���� ���� ��ȯ���ִ�
    public int GetIntAxis(float x)
    {
        if (x > 0)
        {
            return 1;
        }
        else if (x < 0)
        {
            return -1;
        }
        return 0;
    }

    //�뽬
    public void OnDashEnd()
    {
        rb.gravityScale = 1f;
        animator.SetBool("isDash", false);
    }

    //�и�
    public void OnParraing()
    {
        power_gauge += 20;
        Vector2 velocity = rb.velocity;
        velocity.y = perryPower;
        rb.velocity = velocity;
    }


    //������ �޾�����
    void TakeDamage(Vector2 targetPos)
    {
        currenthp--;
        animator.SetBool("Attacked", true);
        gameObject.layer = 11;

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : 1;
        rb.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        Invoke("HitEnd", 0.5f);
        Invoke("OffDamaged", 2f);
    }

    //�ǰ� ��� ������, �����ð�
    void HitEnd()
    {                                                                          //
        animator.SetBool("Attacked", false);                                   //
        spriteRenderer.color = new Color(1, 1, 1, 0.7f);
    }

    //�����ð� ����
    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = 10;
    }

    public void Power_Gauge_Up(int gauge)
    {
        power_gauge += gauge;
    }


    //�Ʒ�����
    IEnumerator Down_Jump_co()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Platform"), LayerMask.NameToLayer("Player"), true);
        rb.AddForce(new Vector2(0, -jump_Force));
        yield return new WaitForSeconds(0.2f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Platform"), LayerMask.NameToLayer("Player"), false);
    }


    //Enemy �±׿� �ε�������, ������ ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.transform.position);
        }
        boxCollider2D.enabled = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isjump = true;
            animator.SetBool("isJump", isjump);
        }
        else if (collision.gameObject.CompareTag("PlatForm"))
        {
            isjump = true;
            animator.SetBool("isJump", isjump);
            onPlatform = false;
            //trigerCollider.enabled = false;
        }
    }

   private void OnCollisionStay2D(Collision2D collision)
   {
        if ((collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("PlatForm")) && animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump") && !animator.IsInTransition(0))
        {
            isjump = false;
            animator.SetBool("isJump", isjump);
            animator.SetBool("isParry", isjump);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.transform.position);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            jump_transform.transform.position = gameObject.transform.position;
            jump_animator.SetTrigger("Ground");

            isjump = false;
            animator.SetBool("isJump", isjump);
            animator.SetBool("isParry", isjump);
        }
        else if (collision.gameObject.CompareTag("PlatForm") && rb.velocity.y == 0)
        {
            jump_transform.transform.position = gameObject.transform.position;
            jump_animator.SetTrigger("Ground");

            onPlatform = true;
            isjump= false;
            animator.SetBool("isJump", isjump);
            animator.SetBool("isParry", isjump);
        }

        boxCollider2D.enabled = false;
    }



    public delegate void Del();

    public void LocateUIAtPos(GameObject pos, GameObject uiObj)
    {
        uiObj.transform.position = Camera.main.WorldToScreenPoint(pos.transform.position);
    }

    public IEnumerator CoWait(float time, Del func)
    {
        yield return new WaitForSeconds(time);

        func();
    }
}
