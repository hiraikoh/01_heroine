using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[���N�����ɐ��������A�V���O���g���ȃQ�[���Ǘ��I�u�W�F�N�g
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Config�Őݒ肷��p�����[�^
    public float bgmVolume;
    public float seVolume;
    public bool isOverinterpret;    // ���[�g����Ɏg�p

    void Awake()
    {
        // �V�[���ړ����A�j�󂳂�Ȃ��悤�ɂ���
        DontDestroyOnLoad(this.gameObject);

        // ���ɃC���X�^���X�����݂����ꍇ�A������j��
        CheckInstance();
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
