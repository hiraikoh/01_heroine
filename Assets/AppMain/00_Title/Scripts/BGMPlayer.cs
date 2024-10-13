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
        // GameManager�̃p�����[�^�ŉ��ʒ���
        bgmSlider.value = GameManager.instance.bgmVolume;
        Debug.Log("bgmVolume: " + bgmSlider.value);
        AdjustVolume("BGM", bgmSlider.value);

        // �X���C�_�[�𓮂��������A�����ɉ��ʒ��������悤�ɂ���
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            AdjustVolume("BGM", value);
        });

        // 2�b��i�X�v���b�V����ʂ����������j��BGM�J�n
        await UniTask.Delay(2000);
        bgmAudioSource.Play();
    }

    // AudioMixer��"name"�̉��ʂ��A"value"�����Ƃɒ���
    // TODO: ���ʊ֐��ɂ���
    void AdjustVolume(string name, float value)
    {
        value = Mathf.Clamp01(value);
        float decibel = 20f * Mathf.Log10(value);
        audioMixer.SetFloat(name, decibel);
    }

}
