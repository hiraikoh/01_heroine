using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigsView : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    void Start()
    {
        // GameManager�̃p�����[�^�Ńg�O���؂�ւ�
        toggle.isOn = GameManager.instance.isOverinterpret;

    }
}
