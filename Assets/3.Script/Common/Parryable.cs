using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parryable : MonoBehaviour
{
    public bool isParryable = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (other.GetComponent<Animator>().GetBool("isParry"))
        {
            other.GetComponent<PlayerMovement>().OnParraing();

            if (isParryable)
                OnParryable();
        }
    }

    public virtual void OnParryable()
    {
        gameObject.SetActive(false);
    }
}