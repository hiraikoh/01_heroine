using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButtons : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    [SerializeField] List<GameObject> selectButtonPrefabs;

    void Start()
    {
        // �v���n�u�̃{�^���̐����A�C���X�^���X����
        for(int i = 0; i < selectButtonPrefabs.Count; i++)
        {
            Instantiate(selectButtonPrefabs[i], buttons.transform);
        }        
    }
}
