using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource musicSound, sfxSound;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private AudioMixer mixer;

    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
               instance = FindFirstObjectByType<SoundManager>();

                if (instance == null)
                {
                    GameObject soundManagerObject = new GameObject(nameof(SoundManager));
                    instance = soundManagerObject.AddComponent<SoundManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [ContextMenu("Change Music Volume")]
    public void ChangeMusicVolume(float newVolume)
    {
    
        musicSound.volume = newVolume;
    }

    [ContextMenu("Change SFX Volume")]
    public void ChangeSFXVolume(float newVolume)
    {
        sfxSound.volume = newVolume;
    }

    public void PlaySFX()
    {
        sfxSound.PlayOneShot(soundEffect);
    }
}
