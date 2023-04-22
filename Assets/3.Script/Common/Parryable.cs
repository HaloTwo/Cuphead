using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parryable : MonoBehaviour
{
    public bool isParryable = true;
    private float parrying_time_slow = 0.1f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (other.GetComponent<Animator>().GetBool("isParry"))
        {
            other.GetComponent<PlayerMovement>().OnParraing();

            if (isParryable)
                StartCoroutine(OnParryable());
        }
    }

    public IEnumerator OnParryable()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(parrying_time_slow);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}