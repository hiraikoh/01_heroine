using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シナリオのテンプレートクラス
[System.Serializable]
public class ScenarioData
{
    //名前
    public string Name = "";
    //会話内容
    [Multiline(3)] public string Talk = "";
    //場所
    public string Place = "";
    //キャラ
    public string Chara = "";
}
