using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_get : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;

    void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out capsuleCollider);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.CompareTag("Player"))
        {
            Player_State.Instance.coin++;
            animator.SetBool("Get",true);

            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f && animator.GetCurrentAnimatorStateInfo(0).IsName("Coin_Boom"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
