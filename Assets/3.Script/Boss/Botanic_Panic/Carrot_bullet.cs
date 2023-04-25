using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot_bullet : MonoBehaviour
{
    [SerializeField]
    private int bullet_hp = 3;
    public float speed;
    public Transform player;
    Vector3 dir;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CapsuleCollider2D capsuleCollider2;
    private void Awake()
    {
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out animator);
        TryGetComponent(out capsuleCollider2);
    }

    private void OnEnable()
    {
        capsuleCollider2.enabled = true;
        animator.Rebind();
        bullet_hp = 3;
    }


    public void Update()
    {
        if (player != null)
        {
            dir = player.position - transform.position;
            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 270f, Vector3.forward);
            transform.rotation = angleAxis;
        }
        else
        {
            Destroy(gameObject);
        }

        if (bullet_hp <= 0)
        {
            animator.SetTrigger("Destroy");
            capsuleCollider2.enabled = false;
            Invoke("bullet_Die", 0.2f);
        }

        transform.position += dir * speed * Time.deltaTime;
    }

    void bullet_Die()
    {
        gameObject.SetActive(false);
    }
    IEnumerator HitAnimation_co()
    {
        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            bullet_hp--;
            StartCoroutine(HitAnimation_co());
            StopCoroutine(HitAnimation_co());
        }
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            animator.SetTrigger("Destroy");
            capsuleCollider2.enabled = false;
            Invoke("bullet_Die", 0.2f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            bullet_hp--;
            StartCoroutine(HitAnimation_co());
            StopCoroutine(HitAnimation_co());
        }
    }
}
