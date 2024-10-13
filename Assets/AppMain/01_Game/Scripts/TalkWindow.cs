using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;

public class TalkWindow : MonoBehaviour
{
    //[SerializeField] EventSystem eventSystem;
    [SerializeField] TextMeshProUGUI nameText;          // 名前表示領域
    //[SerializeField] Text talkText;
    [SerializeField] TextMeshProUGUI talkText;          // セリフ表示領域
    [SerializeField] GameObject next;                   // 進行可能アイコン（右下にある矢印）
    [SerializeField] float timePerCharacter = 0.05f;    // 文字送りの速さ
    [SerializeField] GameObject selectButtons;          // ボタン選択画面
    [SerializeField] GameObject buttons;                // ボタンの親オブジェクト
    [SerializeField] UnityEngine.UI.Button nextButton;  // 進行ボタン（画面全体）

    // シナリオ
    // TODO: csv等のファイルで管理できるようにする
    [SerializeField] List<ScenarioData> scenarioDatas;
    [SerializeField] List<ScenarioData> scenarioDatas_normal0;
    [SerializeField] List<ScenarioData> scenarioDatas_normal1;
    [SerializeField] List<ScenarioData> scenarioDatas_normal2;
    [SerializeField] List<ScenarioData> scenarioDatas_overint0;
    [SerializeField] List<ScenarioData> scenarioDatas_overint1;
    [SerializeField] List<ScenarioData> scenarioDatas_overint2;

    // オーディオ
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] AudioClip seAudioClip;

    // 現在のシナリオ
    List<ScenarioData> currentScenarioDatas;

    // 各ルートのシナリオリスト
    List<List<ScenarioData>> scenarioList_normal;
    List<List<ScenarioData>> scenarioList_overint;

    // GameManagerのパラメータ（デバッグ用）
    float bgmValue;
    float seValue;
    bool isOverinterpret;

    int _count;
    float _time;
    int _index;
    string[] _buttonTexts;
    GameObject[] _buttons;

    void Start()
    {
        // INFO: 有償版ならTextMeshProUGUIでも使える
        //talkText.DOText("まつしまや\nああまつしまや\nまつしまや", 3);  //デバッグ用

        // ボタン選択画面を非表示
        selectButtons.SetActive(false);

        // 設定画面で入れたパラメータを代入
        bgmValue = GameManager.instance.bgmVolume;
        seValue = GameManager.instance.seVolume;
        //isOverinterpret = true; //デバッグ用
        isOverinterpret = GameManager.instance.isOverinterpret;

        // 各ルート用のシナリオリスト作成
        scenarioList_normal = new List<List<ScenarioData>>() { scenarioDatas_normal0, scenarioDatas_normal1, scenarioDatas_normal2 };
        scenarioList_overint = new List<List<ScenarioData>>() { scenarioDatas_overint0, scenarioDatas_overint1, scenarioDatas_overint2 };

        // 最初のシナリオをセット
        currentScenarioDatas = scenarioDatas;
        SetNextScenario(currentScenarioDatas, _index);
        Debug.Log("Talk.Length:" + scenarioDatas[0].Talk.Length);

        // 画面クリックで次のセリフが表示されるようにする
        nextButton.onClick.AddListener(() => NextScenario());

        // 音量調整してBGM開始
        AdjustVolume("BGM", bgmValue);
        AdjustVolume("SE", seValue);
        bgmAudioSource.Play();
    }

    void Update()
    {
        _time += Time.fixedDeltaTime;

        // 文字送り
        if (_time > timePerCharacter)
        {
            _time = 0;
            _count++;
            talkText.maxVisibleCharacters = _count;
        }

        // セリフがすべて表示された時、進行可能アイコンを表示
        if (talkText.maxVisibleCharacters > talkText.text.Length)
        {
            next.SetActive(true);
        }    
    }

    async void NextScenario()
    {
        // セリフがすべて表示済みの場合
        if (talkText.maxVisibleCharacters > talkText.text.Length)
        {
            if (currentScenarioDatas[_index].Name == "選択肢")
            {
                Debug.Log("name:選択肢");

                // 選択画面表示
                selectButtons.SetActive(true);

                // 選択肢のテキストを分割
                _buttonTexts = currentScenarioDatas[_index].Talk.Split(',');

                // transform（位置、親子関係）を取得
                var buttonsTransform = buttons.transform;

                // プレハブのインスタンス生成が完了するまで待つ
                await UniTask.WaitUntil(() => 0 < buttonsTransform.childCount);
                _buttons = new GameObject[buttonsTransform.childCount];
                //Debug.Log("_buttons:" + _buttons.Length);

                for (var i = 0; i < _buttons.Length; i++)
                {
                    var _i = i; // NOTE: 一度代入しないとSelectScenarioの引数が全て3になる
                    _buttons[i] = buttonsTransform.GetChild(i).gameObject;

                    // 拡大解釈の場合、文字色を赤にする
                    if (isOverinterpret)
                    {
                        _buttons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    }
                    else
                    {
                        _buttons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                    }

                    _buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = _buttonTexts[i];
                    _buttons[i].GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(() => SelectScenario(_i));
                }
            }
            else if (currentScenarioDatas[_index].Name == "END")
            {
                SceneManager.LoadScene("00_Title");
            }
            else
            {
                _count = 0;
                SetNextScenario(currentScenarioDatas, _index);
            }
        }        
    }

    async void SelectScenario(int index)
    {
        seAudioSource.PlayOneShot(seAudioClip);

        Debug.Log("押したボタンのNo:" + index);

        // 押したボタン以外を非活性化
        for (var j = 0; j < _buttons.Length; j++)
        {
            if (j != index)
            {
                _buttons[j].GetComponentInChildren<UnityEngine.UI.Button>().interactable = false;
            }
        }

        await UniTask.Delay(1000);
        selectButtons.SetActive(false);
        _count = 0;
        _index = 0;

        // 拡大解釈ルートへの分岐
        if (isOverinterpret)
        {
            currentScenarioDatas = scenarioList_overint[index];            
        }
        else
        {
            currentScenarioDatas = scenarioList_normal[index];
        }

        SetNextScenario(currentScenarioDatas, _index);
    }

    void SetNextScenario(List<ScenarioData> datas,int index)
    {
        next.SetActive(false);
        if (currentScenarioDatas[_index].Place == "RED")
        {
            talkText.color = Color.red;
        }
        else
        {
            talkText.color = Color.black;
        }
        nameText.text = datas[index].Name;
        talkText.text = datas[index].Talk;
        talkText.maxVisibleCharacters = 0;
        _index++;
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
