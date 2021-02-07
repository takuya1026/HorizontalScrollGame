using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

/// <summary>
/// フェードを再生する
/// </summary>
public class FadePlayer : SingletonMonoBehaviour<FadePlayer>
{
    /// <summary>
    /// フェードのカンバスグループ
    /// </summary>
    [SerializeField]
    private CanvasGroup m_canvasGroup = null;

    /// <summary>
    /// フェード画像
    /// </summary>
    [SerializeField]
    private Image m_fadeImage = null;

    /// <summary>
    /// 開始
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        m_canvasGroup.alpha = 0;
    }

    /// <summary>
    /// フェード再生
    /// </summary>
    public void Play(float from,float to,float duration,Color? fadeColor = null,Action onEndFade = null)
    {
        // カラー設定
        if (fadeColor != null)
        {
            m_fadeImage.color = fadeColor.Value;
        }

        m_canvasGroup.alpha = from;
        m_canvasGroup.DOFade(to, duration).OnComplete(() =>
        {
            onEndFade?.Invoke();
        });
    }
}
