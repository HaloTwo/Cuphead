using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject BulletSpawner;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject BigBullet;
    [SerializeField] private Animator animator;
    [SerializeField] private float Attack_Rate = 0.3f;

    [SerializeField]
    private Transform player;
    //private PlayerMovement player;

    Vector3 Direction = Vector3.right;

    private void Start()
    {
        TryGetComponent(out player);
        TryGetComponent(out animator);
       
    }


    private void FixedUpdate()
    {

        if (player.transform.localScale.x == -1.3f)
        {
            BulletSpawner.transform.localPosition = new Vector3(0.75f, 0.75f, 0);
            BulletSpawner.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        if (player.transform.localScale.x == 1.3f)
        {
            BulletSpawner.transform.localPosition = new Vector3(0.75f, 0.75f, 0);
            BulletSpawner.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shoot_Up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Aim_Up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Up"))
        {
            BulletSpawner.transform.localPosition = new Vector3(0.3f, 1.7f, 0);
            BulletSpawner.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shoot_Down") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Aim_Down"))
        {
            BulletSpawner.transform.localPosition = new Vector3(0.3f, 0.1f, 0);
            BulletSpawner.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shoot_Side_Up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Aim_Side_Up") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Run_Shoot_Sideup") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Side_Up"))
        {
            BulletSpawner.transform.localPosition = new Vector3(0.75f, 1.2f, 0);

            if (player.transform.localScale.x == -1.3f)
            {
                BulletSpawner.transform.rotation = Quaternion.Euler(0f, 0f, 135f);
            }
            else
            {
                BulletSpawner.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
            }
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shoot_Side_Down") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Aim_Side_Down") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Side_Down"))
        {

            BulletSpawner.transform.localPosition = new Vector3(0.75f, 0.4f, 0);


            if (player.transform.localScale.x == -1.3f)
            {
                BulletSpawner.transform.rotation = Quaternion.Euler(0f, 0f, -135f);
            }
            else
            {
                BulletSpawner.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Duck_Shoot") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_duck_Idle"))
        {
            BulletSpawner.transform.localPosition = new Vector3(0.75f, 0.45f, 0);
        }


    }


    private IEnumerator TryAttack_co()
    {
        while (true)
        {
            Instantiate(Bullet, BulletSpawner.transform.position, BulletSpawner.transform.rotation);

            yield return new WaitForSeconds(Attack_Rate);
        }
    }

    public void startFire()
    {
        StartCoroutine("TryAttack_co");
    }
    public void stopFire()
    {
        StopCoroutine("TryAttack_co");
    }

    private IEnumerator Try_Special_Attack_co()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        Instantiate(BigBullet, BulletSpawner.transform.position, BulletSpawner.transform.rotation);
    }

    public void startAttack()
    {
        StartCoroutine("Try_Special_Attack_co");
    }
    public void StopAttack()
    {
        StopCoroutine("Try_Special_Attack_co");
    }
}
