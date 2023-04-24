using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot_Beam : MonoBehaviour
{
    public float speed;
    public Transform player;
    Vector3 dir;

    private void OnEnable()
    {
        if (player != null)
        {
            dir = player.position - transform.position;
            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = angleAxis;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
