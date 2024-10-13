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
    [SerializeField] TextMeshProUGUI nameText;          // ���O�\���̈�
    //[SerializeField] Text talkText;
    [SerializeField] TextMeshProUGUI talkText;          // �Z���t�\���̈�
    [SerializeField] GameObject next;                   // �i�s�\�A�C�R���i�E���ɂ�����j
    [SerializeField] float timePerCharacter = 0.05f;    // ��������̑���
    [SerializeField] GameObject selectButtons;          // �{�^���I�����
    [SerializeField] GameObject buttons;                // �{�^���̐e�I�u�W�F�N�g
    [SerializeField] UnityEngine.UI.Button nextButton;  // �i�s�{�^���i��ʑS�́j

    // �V�i���I
    // TODO: csv���̃t�@�C���ŊǗ��ł���悤�ɂ���
    [SerializeField] List<ScenarioData> scenarioDatas;
    [SerializeField] List<ScenarioData> scenarioDatas_normal0;
    [SerializeField] List<ScenarioData> scenarioDatas_normal1;
    [SerializeField] List<ScenarioData> scenarioDatas_normal2;
    [SerializeField] List<ScenarioData> scenarioDatas_overint0;
    [SerializeField] List<ScenarioData> scenarioDatas_overint1;
    [SerializeField] List<ScenarioData> scenarioDatas_overint2;

    // �I�[�f�B�I
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] AudioClip seAudioClip;

    // ���݂̃V�i���I
    List<ScenarioData> currentScenarioDatas;

    // �e���[�g�̃V�i���I���X�g
    List<List<ScenarioData>> scenarioList_normal;
    List<List<ScenarioData>> scenarioList_overint;

    // GameManager�̃p�����[�^�i�f�o�b�O�p�j
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
        // INFO: �L���łȂ�TextMeshProUGUI�ł��g����
        //talkText.DOText("�܂��܂�\n�����܂��܂�\n�܂��܂�", 3);  //�f�o�b�O�p

        // �{�^���I����ʂ��\��
        selectButtons.SetActive(false);

        // �ݒ��ʂœ��ꂽ�p�����[�^����
        bgmValue = GameManager.instance.bgmVolume;
        seValue = GameManager.instance.seVolume;
        //isOverinterpret = true; //�f�o�b�O�p
        isOverinterpret = GameManager.instance.isOverinterpret;

        // �e���[�g�p�̃V�i���I���X�g�쐬
        scenarioList_normal = new List<List<ScenarioData>>() { scenarioDatas_normal0, scenarioDatas_normal1, scenarioDatas_normal2 };
        scenarioList_overint = new List<List<ScenarioData>>() { scenarioDatas_overint0, scenarioDatas_overint1, scenarioDatas_overint2 };

        // �ŏ��̃V�i���I���Z�b�g
        currentScenarioDatas = scenarioDatas;
        SetNextScenario(currentScenarioDatas, _index);
        Debug.Log("Talk.Length:" + scenarioDatas[0].Talk.Length);

        // ��ʃN���b�N�Ŏ��̃Z���t���\�������悤�ɂ���
        nextButton.onClick.AddListener(() => NextScenario());

        // ���ʒ�������BGM�J�n
        AdjustVolume("BGM", bgmValue);
        AdjustVolume("SE", seValue);
        bgmAudioSource.Play();
    }

    void Update()
    {
        _time += Time.fixedDeltaTime;

        // ��������
        if (_time > timePerCharacter)
        {
            _time = 0;
            _count++;
            talkText.maxVisibleCharacters = _count;
        }

        // �Z���t�����ׂĕ\�����ꂽ���A�i�s�\�A�C�R����\��
        if (talkText.maxVisibleCharacters > talkText.text.Length)
        {
            next.SetActive(true);
        }    
    }

    async void NextScenario()
    {
        // �Z���t�����ׂĕ\���ς݂̏ꍇ
        if (talkText.maxVisibleCharacters > talkText.text.Length)
        {
            if (currentScenarioDatas[_index].Name == "�I����")
            {
                Debug.Log("name:�I����");

                // �I����ʕ\��
                selectButtons.SetActive(true);

                // �I�����̃e�L�X�g�𕪊�
                _buttonTexts = currentScenarioDatas[_index].Talk.Split(',');

                // transform�i�ʒu�A�e�q�֌W�j���擾
                var buttonsTransform = buttons.transform;

                // �v���n�u�̃C���X�^���X��������������܂ő҂�
                await UniTask.WaitUntil(() => 0 < buttonsTransform.childCount);
                _buttons = new GameObject[buttonsTransform.childCount];
                //Debug.Log("_buttons:" + _buttons.Length);

                for (var i = 0; i < _buttons.Length; i++)
                {
                    var _i = i; // NOTE: ��x������Ȃ���SelectScenario�̈������S��3�ɂȂ�
                    _buttons[i] = buttonsTransform.GetChild(i).gameObject;

                    // �g����߂̏ꍇ�A�����F��Ԃɂ���
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

        Debug.Log("�������{�^����No:" + index);

        // �������{�^���ȊO��񊈐���
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

        // �g����߃��[�g�ւ̕���
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

    // AudioMixer��"name"�̉��ʂ��A"value"�����Ƃɒ���
    // TODO: ���ʊ֐��ɂ���
    void AdjustVolume(string name, float value)
    {
        value = Mathf.Clamp01(value);
        float decibel = 20f * Mathf.Log10(value);
        audioMixer.SetFloat(name, decibel);
    }

}
