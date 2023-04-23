using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Super_Beam : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    private void Awake()
    {
        TryGetComponent(out boxCollider2D);
        TryGetComponent(out animator);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    void AnimationEnd()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Boss>().TakeDamage(1);
            if (!collision.CompareTag("Player"))
            {
                Time.timeScale = 0.5f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!collision.CompareTag("Player"))
            {
                Time.timeScale = 1f; // reset the time scale back to normal
            }
        }
    }
}
