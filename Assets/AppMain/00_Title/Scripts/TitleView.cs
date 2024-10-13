using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Toggle toggle;
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonConfig;
    [SerializeField] Button buttonBack;

    Vector3 vecCanvasGroup;

    void Start()
    {
        // �ݒ��ʂ̏����ʒu�̍��W����
        vecCanvasGroup = canvasGroup.transform.localPosition;
        
        // �e�{�^���ɏ�����ݒ�
        buttonStart.onClick.AddListener(GameStart); // �^�C�g����ʑS��
        buttonConfig.onClick.AddListener(SlideIn);  // �^�C�g����ʂ̎���
        buttonBack.onClick.AddListener(SlideOut);   // �ݒ��ʂ́~
    }

    // �ݒ�̃p�����[�^��GameManager�ɑ��������A��ʑJ��
    void GameStart()
    {
        GameManager.instance.bgmVolume = bgmSlider.value;
        GameManager.instance.seVolume = seSlider.value;
        GameManager.instance.isOverinterpret = toggle.isOn;
        SceneManager.LoadScene("01_Game");
    }

    // �ݒ��ʂ��E����X���C�h�C��
    void SlideIn()
    {
        canvasGroup.transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
    }

    // �ݒ��ʂ����̈ʒu�ɖ߂�
    void SlideOut()
    {
        canvasGroup.transform.DOLocalMove(vecCanvasGroup, 1f);
    }
}
