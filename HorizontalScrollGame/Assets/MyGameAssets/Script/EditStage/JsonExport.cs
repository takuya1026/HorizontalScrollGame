
//============================================================
// @file EditJsonExport
// @brief シーンの読み込みと書き出し
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonExport : MonoBehaviour
{
    private StageInfo m_obj;

    [SerializeField, Tooltip("書き出す親オブジェクト")]
    private GameObject m_parentObj = null;
    public GameObject m_ParentObj { set { m_parentObj = value; } }

    private readonly string COMMON_PATH = "Assets/MyGameAssets/Data/Stage/";

    /// <summary>
    /// Json形式に書き出し
    /// </summary>
    public void SeavStageInfo()
    {
        GameObject gameObject = null;
        GameObject childObject = null;
        int count = 0;

        m_obj = new StageInfo();

        if (m_parentObj == null)
        {
            count = getNextFileNumber();
            m_parentObj = new GameObject("Stage_" + count.ToString("D5"));

            childObject = new GameObject("Stage");
            childObject.transform.parent = m_parentObj.transform;

            childObject = new GameObject("Enemy");
            childObject.transform.parent = m_parentObj.transform;

            childObject = new GameObject("Gimmick");
            childObject.transform.parent = m_parentObj.transform;
        }
        else
        {
            count = getCurrentFileNumber();
            m_obj.m_stageName = "Stage_" + count.ToString("D5");
        }

        for (int i = 0; i < m_parentObj.transform.childCount; i++)
        {
            gameObject = m_parentObj.transform.GetChild(i).gameObject;

            if (gameObject.name == "Stage")
            {
                BlockInfo blockInfo = null;
                m_obj.m_resultBlockInfo = new StageInfo.ResultBlockInfo[gameObject.transform.childCount];
                for (int j = 0; j < gameObject.transform.childCount; j++)
                {

                    childObject = gameObject.transform.GetChild(j).gameObject;
                    blockInfo = childObject.GetComponent<BlockInfo>();

                    m_obj.m_resultBlockInfo[j] = new StageInfo.ResultBlockInfo();

                    m_obj.m_resultBlockInfo[j].m_name = childObject.name;
                    m_obj.m_resultBlockInfo[j].m_postion = blockInfo.m_Postion;
                    m_obj.m_resultBlockInfo[j].m_rotation = blockInfo.m_Rotation;
                    m_obj.m_resultBlockInfo[j].m_scale = blockInfo.m_Scale;
                    m_obj.m_resultBlockInfo[j].m_enumBlockType = blockInfo.m_EnumBlockType;
                    m_obj.m_resultBlockInfo[j].m_itemId = blockInfo.m_ItemId;
                    m_obj.m_resultBlockInfo[j].m_quantity = blockInfo.m_Quantity;
                }
            }
        }

        JsonReadWrite.m_Instance.WriteFile<StageInfo>(m_obj, (COMMON_PATH + m_parentObj.name + ".json"));
    }

    /// <summary>
    /// 現在のファイル数
    /// </summary>
    /// <returns></returns>
    private int getCurrentFileNumber()
    {
        int count = 0;
        while (JsonReadWrite.m_Instance.IsFileExists((COMMON_PATH + "Stage_" + count.ToString("D5") + ".json")))
        {
            count++;
        }

        return (count - 1);
    }

    /// <summary>
    /// 次のファイル番号
    /// </summary>
    /// <returns></returns>
    private int getNextFileNumber()
    {
        int count = 0;
        while (JsonReadWrite.m_Instance.IsFileExists((COMMON_PATH + "Stage_" + count.ToString("D5") + ".json")))
        {
            count++;
        }

        return count;
    }
}
