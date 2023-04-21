using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected GameObject target;

    //보스 HP
    //-------------------
    [SerializeField] private int MaxHP;
    [SerializeField] private int CurrentHP;

    public int MAXHP => MaxHP;
    public int Currenthp => CurrentHP;


    //-----------------------------

    public delegate void Del();
    

    public void Awake()
    {
        TryGetComponent(out animator);
        TryGetComponent(out rb);
        TryGetComponent(out spriteRenderer);
        target = GameObject.FindGameObjectWithTag("Player");

        CurrentHP = MAXHP;
    }

    public void moveTarget(GameObject target, float speed)
    {
        float dir = target.transform.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * speed * Time.deltaTime);
    }


    public void forceTarget(GameObject target)
    {
        if (target.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = (new Vector2(1.3f, 1.3f));
        }
        else
        {
            transform.localScale = (new Vector2(-1.3f, 1.3f));
        }
    }

    public void TakeDamage(int Damage)
    {
        CurrentHP -= Damage;

        StartCoroutine(HitAnimation_co());
        StopCoroutine(HitAnimation_co());
    }

    public IEnumerator Boss_off_co(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }


    IEnumerator HitAnimation_co()
    {
        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}