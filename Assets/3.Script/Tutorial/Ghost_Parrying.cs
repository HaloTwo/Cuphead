using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Parrying : Parryable
{
    [SerializeField]
    private Animator ghost;
    private float timer;
    private CircleCollider2D circleCollider2D;

    private void Start()
    {
        TryGetComponent(out ghost);
        TryGetComponent(out circleCollider2D);

    }

    private void Update()
    {
        gameObject.transform.Translate(Vector2.up * Time.deltaTime * 3f);


        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            timer = 0;
            transform.position = new Vector2(102f, -7f);
            ghost.SetBool("Parry", false);
            circleCollider2D.enabled = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (collision.GetComponent<Animator>().GetBool("isParry"))
        {
            collision.GetComponent<PlayerMovement>().OnParraing();

            if (isParryable)
                ghost.SetBool("Parry", true);
            circleCollider2D.enabled = false;
        }
    }


    //public override void OnParryable()
    //{
    //    gameObject.SetActive(false);
    //}

}
