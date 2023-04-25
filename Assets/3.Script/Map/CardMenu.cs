using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardMenu : MonoBehaviour
{
    private AudioSource audioSource;
    public List<TextMeshProUGUI> menus = new List<TextMeshProUGUI>();

    public Color ActiveColor;
    public Color DeactiveColor;
    public GameObject HighLight;

    public AudioClip Choice_Cilp;
    public AudioClip Start_Cilp;

    int idx = 0;
    public int Idx
    {
        get { return idx; }
        set
        {
            menus[idx].color = DeactiveColor;
            idx = value;
            menus[idx].color = ActiveColor;
        }
    }

    void Start()
    {
        TryGetComponent(out audioSource);
        Idx = 0;
    }

    private void OnEnable()
    {
        audioSource.clip = Start_Cilp;
        audioSource.Play();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    switch (Idx)
        //    {
        //        case 0:
        //
        //            break;
        //        case 1:
        //
        //            break;
        //        default:
        //            Application.Quit();
        //            break;
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            audioSource.clip = Choice_Cilp;
            audioSource.Play();
            Idx = Mathf.Max(Idx - 1, 0);
            HighLight.transform.localPosition = new Vector2(-177.3919f, -177.9441f);
            HighLight.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            audioSource.clip = Choice_Cilp;
            audioSource.Play();
            Idx = Mathf.Min(Idx + 1, menus.Count - 1);
            HighLight.transform.localPosition = new Vector2(156f, -177.9441f);
            HighLight.transform.localScale = new Vector3(1.25f, 1f, 1f);
        }
    }
}
