using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Boss
{
    [SerializeField]
    private GameObject[] Beam;
    [SerializeField]
    private GameObject[] Bullet;

    [SerializeField]
    private Transform player;
    private CircleCollider2D circleCollider;
    bool OnDie = false;

    int AttackCount = 0;
    void Start()
    {
        TryGetComponent(out circleCollider);
        GameManager.Instance.curPhase = 3;
        StartCoroutine(Boss_Attack());
    }

    // Update is called once per frame
    void Update()
    {
        if (Currenthp <= 0 && !OnDie)
        {
            OnDie = true;
            StopAllCoroutines();
            animator.SetTrigger("Die");
            GameManager.Instance.SecoundBoss = true;
            GameManager.Instance.CurState = GameState.OUTRO;
        }
    }

    public float SpawnPosY = 4f;

    void Attack()
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

            animator.SetBool("Beam", true);
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < Beam.Length; i++)
            {
                Beam[i].SetActive(false);
                Beam[i].transform.position = transform.position + new Vector3(0f,1.5f,0f);
            }

            for (int i = 0; i < Beam.Length; i++)
            {
                Beam[i].SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
            animator.SetBool("Beam", false);
        }
    }
}