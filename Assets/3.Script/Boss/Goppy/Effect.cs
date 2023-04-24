using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator Boss_animator;
    [SerializeField] private BoxCollider2D Boss_BoxCollider2D;

    void Start()
    {
        TryGetComponent(out animator);
    }

    // Update is called once per frame
    void Update()
    {
        if (Boss_animator.GetBool("Move_Left") == true)
        {
            animator.SetBool("Move", true);
            transform.localScale = new Vector2(1f, 1f);
        }
        if (Boss_animator.GetBool("Move_Right") == true)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        if (Boss_animator.GetBool("Attack_0") == true)
        {
            animator.SetBool("Move", false);
            Invoke("Attack", 0.9f);
            Invoke("Attack_end", 1.8f);
        }
        if (Boss_animator.GetBool("Move_Right") == true)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        if (Boss_animator.GetBool("Touch") == true)
        {
            animator.SetBool("Touch", true);
        }
        if (Boss_animator.GetBool("Die") == true)
        {
            animator.SetBool("End", true);
            animator.SetBool("Move", false);
        }
    }

    void Attack()
    {
        animator.SetBool("Attack",true);
    }
    void Attack_end()
    {
        animator.SetBool("Attack", false);
    }
    
}
