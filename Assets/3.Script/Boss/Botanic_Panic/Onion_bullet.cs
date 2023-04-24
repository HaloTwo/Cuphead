using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion_bullet : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        TryGetComponent(out animator);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Player"))
        {
            animator.SetTrigger("Destroy");
        }
    }
}
