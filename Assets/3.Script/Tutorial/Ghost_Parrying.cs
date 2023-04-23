using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Parrying : Parryable
{
    private Animator ghost;
    private float timer;
    private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        TryGetComponent(out ghost);
        TryGetComponent(out circleCollider2D);
        TryGetComponent(out audioSource);

    }

    private void Update()
    {
        gameObject.transform.Translate(Vector2.up * Time.deltaTime * 3f);

        Debug.Log(isParryable);
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            timer = 0;
            isParryable = true;
            transform.position = new Vector2(102f, -7f);
            ghost.SetBool("Parry", false);
            circleCollider2D.enabled = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (collision.GetComponent<Animator>().GetBool("isParry") && isParryable)
        {
            isParryable = false;
            audioSource.Play();
            collision.GetComponent<PlayerMovement>().OnParraing();
            StartCoroutine(Ghost_co());

        }
    }

    public IEnumerator Ghost_co()
    {
        Time.timeScale = 0.1f;
        ghost.SetBool("Parry", true);
        circleCollider2D.enabled = false;
        yield return new WaitForSecondsRealtime(parrying_time_slow);
        Time.timeScale = 1f;
    }
    //public override void OnParryable()
    //{
    //    gameObject.SetActive(false);
    //}

}
