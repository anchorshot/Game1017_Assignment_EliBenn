using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class SliderAudioController : MonoBehaviour
{
    [SerializeField] private ESoundType soundType;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 1f;
    }

    private void ChangeSoundVolume(float newVolume)
    {
        switch (soundType)
        {
            case ESoundType.Music:
                SoundManager.Instance.ChangeMusicVolume(newVolume);
                break;
            case ESoundType.SFX:
                SoundManager.Instance.ChangeSFXVolume(newVolume);
                break;
        }
    }
    private void OnEnable()
    {
        slider.onValueChanged.AddListener(ChangeSoundVolume);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangeSoundVolume);
    }
    public enum ESoundType
    {
        Music,
        SFX,
        None
    }
}
