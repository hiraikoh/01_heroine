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
        // GameManager�̃p�����[�^�ŉ��ʒ���
        seSlider.value = GameManager.instance.seVolume;
        Debug.Log("seVolume: " + seSlider.value);
        AdjustVolume("SE", seSlider.value);

        // �{�^�������������A������悤�ɂ���
        seButton.onClick.AddListener(() => seAudioSource.PlayOneShot(seAudioClip));

        // �X���C�_�[�𓮂��������A�����ɉ��ʒ��������悤�ɂ���
        seSlider.onValueChanged.AddListener((value) =>
        {
            AdjustVolume("SE", value);
        });
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
