using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public List<TextMeshProUGUI> menus = new List<TextMeshProUGUI>();


    public Color ActiveColor;
    public Color DeactiveColor;

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
        Idx = 0;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (Idx)
            {
                case 0:
                    Time.timeScale = 1f;
                    GameObject player = GameObject.Find("Player");
                    Animator animator = player.GetComponent<Animator>();
                    animator.updateMode = AnimatorUpdateMode.UnscaledTime;
                    gameObject.SetActive(false);
                    break;
                case 1:
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                case 2:
                    Time.timeScale = 1f;
                    SceneManager.LoadSceneAsync("Map");
                    break;
                default:
                    Application.Quit();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Idx = Mathf.Max(Idx - 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Idx = Mathf.Min(Idx + 1, menus.Count - 1);
        }
    }
}
