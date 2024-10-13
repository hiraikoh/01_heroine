using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲーム起動時に生成される、シングルトンなゲーム管理オブジェクト
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Configで設定するパラメータ
    public float bgmVolume;
    public float seVolume;
    public bool isOverinterpret;    // ルート分岐に使用

    void Awake()
    {
        // シーン移動時、破壊されないようにする
        DontDestroyOnLoad(this.gameObject);

        // 既にインスタンスが存在した場合、自分を破壊
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
