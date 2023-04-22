using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Super_Beam : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private GameObject player;


    void Start()
    {
        TryGetComponent(out boxCollider2D);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position= player.transform.position;
        
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
        }
    }
}
