using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アセットにSEManagerを追加する
// NOTE: アセットのオブジェクト（ScriptableObject）のメソッドは、プレハブのOnclickに設定できる
[CreateAssetMenu]
public class SEManager : ScriptableObject
{
    // ゲームオブジェクトのOnAwake用
    public AudioSource audioSource { get; set; }

    // プレハブのOnClick用
    public void PlayOneShot(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
