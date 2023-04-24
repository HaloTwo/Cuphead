using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip Title_cilp;
    public AudioClip House_cilp;
    public AudioClip Tutorial_cilp;
    public AudioClip Goopy_cilp;
    public AudioClip Map_cilp;
    public AudioClip BotanicPanic_cilp;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += ChangeMusic;
    }

    private void ChangeMusic(Scene currentScene, Scene nextScene)
    {
        if (nextScene.name == "Title")
        {
            audioSource.clip = Title_cilp;
        }
        else if (nextScene.name == "MainMenu")
        {
            audioSource.clip = null;
        }
        else if (nextScene.name == "Goopy_Le_Grande")
        {
            audioSource.clip = Goopy_cilp;
        }
        else if (nextScene.name == "Tutorial")
        {
            audioSource.clip = Tutorial_cilp;
        }
        else if (nextScene.name == "House")
        {
            audioSource.clip = House_cilp;
        }
        else if (nextScene.name == "Map")
        {
            audioSource.clip = Map_cilp;
        }
        else if (nextScene.name == "Loading")
        {
            audioSource.clip = null;
        }
        else if (nextScene.name == "BotanicPanic")
        {
            audioSource.clip = BotanicPanic_cilp;
        }
        

        audioSource.Play();
    }
}

