using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class SceneAudio
    {
        public string sceneName;
        public AudioClip backgroundMusic;
    }

    public SceneAudio[] sceneAudios;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private string currentSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSceneName = SceneManager.GetActiveScene().name;
        PlayBackgroundMusic(currentSceneName);
    }

    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != currentSceneName)
        {
            currentSceneName = sceneName;
            PlayBackgroundMusic(currentSceneName);
        }
    }

    private void PlayBackgroundMusic(string sceneName)
    {
        foreach (SceneAudio sceneAudio in sceneAudios)
        {
            if (sceneAudio.sceneName == sceneName)
            {
                if (audioSource.clip != sceneAudio.backgroundMusic)
                {
                    audioSource.Stop();
                    audioSource.clip = sceneAudio.backgroundMusic;
                    audioSource.Play();
                }
                return;
            }
        }
    }
}

