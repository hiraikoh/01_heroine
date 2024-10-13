using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigsView : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    void Start()
    {
        // GameManagerのパラメータでトグル切り替え
        toggle.isOn = GameManager.instance.isOverinterpret;

    }
}
