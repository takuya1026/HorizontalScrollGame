using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    private int m_selectStageNumber = 0;
    public int m_SelectStageNumber { get { return m_selectStageNumber; } set { m_selectStageNumber = value; } }

    private int m_maxStageNumber = 0;
    public int m_MaxStageNumber { get { return m_maxStageNumber; } set { m_maxStageNumber = value; } }

    /// <summary>
    /// 開始処理
    /// </summary>
    protected override void Awake()
    {
        getCurrentFileNumber();
    }

    /// <summary>
    /// ステージ数
    /// </summary>
    /// <returns>現在のファイル数</returns>
    private void getCurrentFileNumber()
    {
        m_maxStageNumber = 0;
        while (JsonReadWrite.m_Instance.IsFileExists(("Assets/MyGameAssets/Data/Stage/Stage_" + m_maxStageNumber.ToString("D5") + ".json")))
        {
            m_maxStageNumber++;
        }
    }

    /// <summary>
    /// 次のステージへ
    /// (最大時は 1 番目に戻る)
    /// </summary>
    public void NextStage()
    {
        m_selectStageNumber++;

        if (m_selectStageNumber > m_MaxStageNumber)
        {
            m_selectStageNumber = 1; 
        }
    }

    /// <summary>
    /// 前のステージへ
    /// (最小時は 1 番目固定)
    /// </summary>
    public void PrevStage()
    {
        m_selectStageNumber--;

        if (m_selectStageNumber < 1)
        {
            m_selectStageNumber = 1;
        }
    }
}
