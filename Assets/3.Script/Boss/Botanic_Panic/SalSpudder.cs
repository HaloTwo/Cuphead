using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalSpudder : Boss
{
    CapsuleCollider2D capsuleCollider;
    bool OnDie = false;

    [SerializeField]
    private GameObject[] Bullet;

    public AudioClip Attack_clip;
    public AudioClip Die;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.curPhase = 1;
        TryGetComponent(out capsuleCollider);
        StartCoroutine(Boss_Attack_co());

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (Currenthp <= 0 && !OnDie)
        {
            OnDie = true;
            audioSource.clip = Die;
            audioSource.Play();
            StopCoroutine(Boss_Attack_co());

            StartCoroutine(Boss_Out_co());
        }
    }

    IEnumerator Boss_Out_co()
    {
        animator.SetTrigger("Die");
        capsuleCollider.enabled = false;
        yield return new WaitForSeconds(2f);

        float outTime = 0f; // 떨어지는 시간

        while (outTime <= 5) // 2초 동안 아래로 이동
        {
            outTime += Time.deltaTime;
            transform.Translate(new Vector2(0, -2f * Time.deltaTime));
            yield return null;
        }
        gameObject.SetActive(false);
    }

    IEnumerator Boss_Attack_co()
    {
        int Attack_count = 0;
        while (true)
        {
            yield return new WaitForSeconds(3f);

            while (Attack_count < 4)
            {
                animator.SetTrigger("Attack");
                yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("Potato_Attack"));
                audioSource.clip = Attack_clip;
                audioSource.Play();
                Bullet[Attack_count].SetActive(true);
                Attack_count++;
                yield return new WaitForSeconds(1f); // 총알 발사 후 대기 시간
            }

            Attack_count = 0;

            for (int i = 0; i < Bullet.Length; i++)
            {
                Bullet[i].SetActive(false);
                Bullet[i].transform.position = transform.position + new Vector3(-0.5f, -2.5f, 0f);
            }
        }
    }
}
