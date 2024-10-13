using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

// CanvasGroupにのみ適用し、2秒後にフェードアウトさせる
[RequireComponent(typeof(CanvasGroup))]
public class UITransition : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    async void Start()
    {
        await UniTask.Delay(2000);
        canvasGroup.DOFade(0, 1);
        Debug.Log("画面非表示");
    }
}
