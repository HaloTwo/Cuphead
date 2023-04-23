using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject BigBullet;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource Bulletaudio;
    [SerializeField] private float Attack_Rate = 0.3f;

    public AudioClip bulletClips;

    [SerializeField]
    private Transform player_location;
    //private PlayerMovement player;

    Vector3 Direction = Vector3.right;



    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player_location);
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out animator);
        TryGetComponent(out Bulletaudio);
    }

    private void Start()
    {
  
    }


    private void FixedUpdate()
    {

        if (player_location.transform.localScale.x == -1.3f)
        {
            transform.localPosition = new Vector3(0.75f, 0.75f, 0);
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        if (player_location.transform.localScale.x == 1.3f)
        {
            transform.localPosition = new Vector3(0.75f, 0.75f, 0);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shoot_Up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Aim_Up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Up"))
        {
            transform.localPosition = new Vector3(0.3f, 1.7f, 0);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shoot_Down") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Aim_Down"))
        {
            transform.localPosition = new Vector3(0.3f, 0.1f, 0);
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shoot_Side_Up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Aim_Side_Up") 
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Run_Shoot_Sideup") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Side_Up"))
        {
            transform.localPosition = new Vector3(0.75f, 1.2f, 0);

            if (player_location.transform.localScale.x == -1.3f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 135f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 45f);
            }
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Shoot_Side_Down") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Aim_Side_Down") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Side_Down"))
        {

            transform.localPosition = new Vector3(0.75f, 0.4f, 0);


            if (player_location.transform.localScale.x == -1.3f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -135f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Duck_Shoot") || animator.GetCurrentAnimatorStateInfo(0).IsName("Player_duck_Idle"))
        {
            transform.localPosition = new Vector3(0.75f, 0.45f, 0);
        }


    }


    private IEnumerator TryAttack_co()
    {

        while (true)
        {
       
            Instantiate(Bullet, transform.position, transform.rotation);

            yield return new WaitForSeconds(Attack_Rate);

        }
    }

    public void startFire()
    {
        Bulletaudio.loop = true;
        Bulletaudio.clip = bulletClips;
        Bulletaudio.Play();

        StartCoroutine("TryAttack_co");
    }
    public void stopFire()
    {
        Bulletaudio.Stop();

        StopCoroutine("TryAttack_co");
    }

    private IEnumerator Try_Special_Attack_co()
    {

        //yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Side_Up") ||
        //    animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Straight") ||
        //    animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Up") ||
        //    animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Side_Down") ||
        //    animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Special_Attack_Down") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
      
        yield return new WaitForSeconds(0.2f);

        Instantiate(BigBullet, transform.position, transform.rotation);
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
