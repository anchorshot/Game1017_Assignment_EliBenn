using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;


    [Header("Music n sounds")]
    [SerializeField] private AudioClip BGMusic;
    [SerializeField] private AudioClip CollisionSound;
    [SerializeField] private AudioClip JumpSound;


    public void PlayCollisionSound()
    {
        if (CollisionSound != null)
        {
            sfxSource.PlayOneShot(CollisionSound);

        }
    }
    public void PlayJumpSound()
    {
        if (JumpSound != null)
        {
            sfxSource.PlayOneShot(JumpSound);

        }
    }
    public void ChangeMusicVolume(float newVolume)
    {
        musicSource.volume = newVolume;
    }
    public void ChangeSFXVolume(float newVolume)
    {
        sfxSource.volume = newVolume;
    }
}