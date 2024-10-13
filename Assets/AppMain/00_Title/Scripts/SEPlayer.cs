using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SEPlayer : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] AudioClip seAudioClip;
    [SerializeField] Slider seSlider;
    [SerializeField] Button seButton;

    void Start()
    {
        // GameManagerのパラメータで音量調整
        seSlider.value = GameManager.instance.seVolume;
        Debug.Log("seVolume: " + seSlider.value);
        AdjustVolume("SE", seSlider.value);

        // ボタンを押した時、音が鳴るようにする
        seButton.onClick.AddListener(() => seAudioSource.PlayOneShot(seAudioClip));

        // スライダーを動かした時、即座に音量調整されるようにする
        seSlider.onValueChanged.AddListener((value) =>
        {
            AdjustVolume("SE", value);
        });
    }

    // AudioMixerの"name"の音量を、"value"をもとに調整
    // TODO: 共通関数にする
    void AdjustVolume(string name, float value)
    {
        value = Mathf.Clamp01(value);
        float decibel = 20f * Mathf.Log10(value);
        audioMixer.SetFloat(name, decibel);
    }
}
