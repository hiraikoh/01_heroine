using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] Slider bgmSlider;

    async void Start()
    {
        // GameManagerのパラメータで音量調整
        bgmSlider.value = GameManager.instance.bgmVolume;
        Debug.Log("bgmVolume: " + bgmSlider.value);
        AdjustVolume("BGM", bgmSlider.value);

        // スライダーを動かした時、即座に音量調整されるようにする
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            AdjustVolume("BGM", value);
        });

        // 2秒後（スプラッシュ画面が消えた時）にBGM開始
        await UniTask.Delay(2000);
        bgmAudioSource.Play();
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
