using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Boss
{
    [SerializeField]
    private GameObject[] Beam;
    [SerializeField]
    private GameObject[] Bullet;

    public AudioClip Carret_Die;
    public AudioClip Beam_Go;
    public AudioClip Beam_Go_fire;

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
            //인트로 3초
            yield return new WaitForSeconds(3f);

            //빔 발사 애니메이션 출력
            animator.SetBool("Beam", true);
            audioSource.clip = Beam_Go;
            audioSource.Play();

            yield return new WaitForSeconds(2f);         
            audioSource.clip = Beam_Go_fire;
            audioSource.loop = true;
            audioSource.Play();

            //총 4번 발사
            int beam_count = 0;
            while (beam_count <= 3)
            {
                beam_count++;
                for (int i = 0; i < Beam.Length; i++)
                {
                    Beam[i].SetActive(false);
                    Beam[i].transform.position = transform.position + new Vector3(0f, 1.5f, 0f);
                }
                for (int i = 0; i < Beam.Length; i++)
                {
                    Beam[i].SetActive(true);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1.5f);
            }        
            animator.SetBool("Beam", false);
            audioSource.loop = false;

            //빔 발사 종료 후 추격 미사일 발사
            for (int i = 0; i < Bullet.Length; i++)
            {
                Bullet[i].SetActive(false);
            }
            AttackCount = 0;
            for (int i = 0; i < Bullet.Length; i++)
            {
                Attack();
                yield return new WaitForSeconds(1f);
            }


        }
    }
}