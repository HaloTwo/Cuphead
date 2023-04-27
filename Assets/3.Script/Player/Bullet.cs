using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float bulletspeed = 50f;
    private float currentSpeed;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private int gauge;
    //private PlayerMovement player;
    //[SerializeField]
    //private GameObject player;
    //[SerializeField]
    //private Transform playerTransform;
    //private Vector3 playerPosition;


    Vector3 Direction = Vector3.right;


    private void Awake()
    {
        TryGetComponent(out animator);
    }


    private void OnEnable()
    {
        currentSpeed = bulletspeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(Direction * currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Ground") || collision.CompareTag("Wall") || collision.CompareTag("Finish"))
        {
            animator.SetTrigger("Destroy");
            StartCoroutine(DestroyBullet_co());
        }

         if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Boss>().TakeDamage(Damage);
            FindObjectOfType<PlayerMovement>().Power_Gauge_Up(gauge);
            animator.SetTrigger("Destroy");
            StartCoroutine(DestroyBullet_co());
        }

    }

    IEnumerator DestroyBullet_co()
    {
        currentSpeed = 0;
        yield return new WaitForSeconds(0.5f); //new WaitUntil(() => animator.GetNextAnimatorStateInfo(0).normalizedTime == 1.0f && animator.GetNextAnimatorStateInfo(0).IsName("bullet_destroy"));
        gameObject.SetActive(false);
    }

}

