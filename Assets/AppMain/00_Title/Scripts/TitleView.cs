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
        // 設定画面の初期位置の座標を代入
        vecCanvasGroup = canvasGroup.transform.localPosition;
        
        // 各ボタンに処理を設定
        buttonStart.onClick.AddListener(GameStart); // タイトル画面全体
        buttonConfig.onClick.AddListener(SlideIn);  // タイトル画面の歯車
        buttonBack.onClick.AddListener(SlideOut);   // 設定画面の×
    }

    // 設定のパラメータをGameManagerに代入した後、画面遷移
    void GameStart()
    {
        GameManager.instance.bgmVolume = bgmSlider.value;
        GameManager.instance.seVolume = seSlider.value;
        GameManager.instance.isOverinterpret = toggle.isOn;
        SceneManager.LoadScene("01_Game");
    }

    // 設定画面を右からスライドイン
    void SlideIn()
    {
        canvasGroup.transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
    }

    // 設定画面を元の位置に戻す
    void SlideOut()
    {
        canvasGroup.transform.DOLocalMove(vecCanvasGroup, 1f);
    }
}
