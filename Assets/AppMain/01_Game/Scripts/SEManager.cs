using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �A�Z�b�g��SEManager��ǉ�����
// NOTE: �A�Z�b�g�̃I�u�W�F�N�g�iScriptableObject�j�̃��\�b�h�́A�v���n�u��Onclick�ɐݒ�ł���
[CreateAssetMenu]
public class SEManager : ScriptableObject
{
    // �Q�[���I�u�W�F�N�g��OnAwake�p
    public AudioSource audioSource { get; set; }

    // �v���n�u��OnClick�p
    public void PlayOneShot(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
