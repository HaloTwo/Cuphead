using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parryable : MonoBehaviour
{
    [SerializeField]
    protected AudioSource audioSource;
    public bool isParryable = true;
    [SerializeField]
    protected float parrying_time_slow = 0.1f;
    [SerializeField]
    protected CircleCollider2D circleCollider2D;

    private void Awake()
    {
        TryGetComponent(out audioSource);
        TryGetComponent(out circleCollider2D);
    }

    private void OnEnable()
    {
        isParryable = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (other.GetComponent<Animator>().GetBool("isParry") && isParryable)
        {
            isParryable = false;
            audioSource.Play();
            other.GetComponent<PlayerMovement>().OnParraing();
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