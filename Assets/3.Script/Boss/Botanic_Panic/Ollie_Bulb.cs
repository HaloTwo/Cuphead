using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ollie_Bulb : Boss
{
    CapsuleCollider2D capsuleCollider;
    bool OnDie = false;
    int AttackCount = 0;

    public AudioClip Crying_cilp;
    public AudioClip Die_cilp;

    [SerializeField]
    private GameObject[] tears = new GameObject[2];

    [SerializeField]
    private GameObject[] Bullet;
    [SerializeField]
    private GameObject Ground;
    [SerializeField]
    private GameObject Boss;

    private void Start()
    {
        TryGetComponent(out capsuleCollider);
        GameManager.Instance.curPhase = 2;
        StartCoroutine(Boss_Attack());
    }


    // Update is called once per frame
    void Update()
    {

        if (Currenthp <= 0 && !OnDie)
        {
            OnDie = true;
            StopCoroutine(Boss_Attack());
            capsuleCollider.enabled = false;
            animator.SetTrigger("Die");

            audioSource.clip = Die_cilp;
            audioSource.Play();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Onion_Die") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Ground.SetActive(false);
            gameObject.SetActive(false);
            Boss.SetActive(false);
        }

    }
    public float SpawnPosY = 4f;

    public void Attack()
    {
        Vector3 spawnPos = Vector3.zero;
        spawnPos.x = RandomTearPosition();
        spawnPos.y = SpawnPosY;

        Bullet[AttackCount].transform.position = spawnPos;
        Bullet[AttackCount].SetActive(true);
        AttackCount++;

        if (AttackCount >= Bullet.Length)
        {
            AttackCount = 0;
        }
    }


    float RandomTearPosition()
    {
        int screenWidthHalf = (int)Camera.main.orthographicSize * Screen.width / Screen.height;
        float scaleHalf = transform.localScale.x * 0.5f;

        if (Random.Range(0, 2) == 0)
        {
            return Random.Range(screenWidthHalf * (-1), scaleHalf * (-1));
        }
        return Random.Range(scaleHalf, screenWidthHalf);
    }


    IEnumerator Boss_Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);

            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < tears.Length; i++)
            {
                tears[i].GetComponent<Animator>().Rebind();
                tears[i].SetActive(true);
            }
            audioSource.clip = Crying_cilp;
            audioSource.Play();

            for (int i = 0; i < Bullet.Length; i++)
            {
                Bullet[i].SetActive(false);
            }

            AttackCount = 0;

            for (int i = 0; i < Bullet.Length; i++)
            {
                Attack();
                yield return new WaitForSeconds(0.5f);
            }

            for (int i = 0; i < tears.Length; i++)
            {
                audioSource.Stop();
                tears[i].GetComponent<Animator>().SetTrigger("turnOff");
            }
            animator.SetBool("Attack", false);
        }
    }


}
