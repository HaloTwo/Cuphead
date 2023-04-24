using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ollie_Bulb : Boss
{
    CapsuleCollider2D capsuleCollider;
    bool OnDie = false;

    [SerializeField]
    private GameObject[] Bullet;
    [SerializeField]
    private GameObject Ground;

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
            capsuleCollider.enabled = false;
            animator.SetTrigger("Die");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Onion_Die") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Ground.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    IEnumerator Boss_Attack()
    {
 

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Onion_Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.2f)
            {
                for (int i = 0; i < Bullet.Length; i++)
                {
                    Bullet[i].SetActive(true);
                }

                yield return new WaitForSeconds(2f);

                for (int i = 0; i < Bullet.Length; i++)
                {
                    Bullet[i].SetActive(false);
                    Bullet[i].transform.position = transform.position + new Vector3(-0.5f, -2.5f, 0f);
                }
            }



    }


}
