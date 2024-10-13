using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

// �X�v���b�V����ʕ\����A�^�C�g����ʂ�\������
public class TitleScene : MonoBehaviour
{
    [SerializeField] List<GameObject> viewList;

    async void Start()
    {
        // �X�v���b�V����ʂ̂ݕ\��
        viewList[0].SetActive(true);
        viewList[1].SetActive(false);

        //2�b��A�^�C�g����ʂ�\��
        await UniTask.Delay(2000);
        viewList[1].SetActive(true);

        //2�b��A�X�v���b�V����ʂ��\��
        await UniTask.Delay(2000);
        viewList[0].SetActive(false);
    }
}
