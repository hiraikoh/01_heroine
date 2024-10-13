using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

// スプラッシュ画面表示後、タイトル画面を表示する
public class TitleScene : MonoBehaviour
{
    [SerializeField] List<GameObject> viewList;

    async void Start()
    {
        // スプラッシュ画面のみ表示
        viewList[0].SetActive(true);
        viewList[1].SetActive(false);

        //2秒後、タイトル画面を表示
        await UniTask.Delay(2000);
        viewList[1].SetActive(true);

        //2秒後、スプラッシュ画面を非表示
        await UniTask.Delay(2000);
        viewList[0].SetActive(false);
    }
}
