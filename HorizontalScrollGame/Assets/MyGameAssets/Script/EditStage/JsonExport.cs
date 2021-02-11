
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

        GameObject childCategoryObject = null;
        for (int categoryIndex = 0, categoryLength = getChildCount(m_parentObj); categoryIndex < categoryLength; categoryIndex++)
        {
            childCategoryObject = getChildObject(m_parentObj, categoryIndex);
            if (childCategoryObject.name == "Stage")
            {
                int totalChildCount = getTotalChildCount(childCategoryObject, 0);
                m_obj.m_resultBlockInfo = new StageInfo.ResultBlockInfo[(totalChildCount - 1)];
                setValueChild(childCategoryObject, 0);
            }
        }

        JsonReadWrite.m_Instance.WriteFile<StageInfo>(m_obj, (COMMON_PATH + m_parentObj.name + ".json"));
    }

    private int setValueChild(GameObject childObject, int index = 0)
    {
        int totalIndex = index;

        if (totalIndex == 0)
        {
            m_obj.m_resultBlockInfo[totalIndex] = new StageInfo.ResultBlockInfo();
            m_obj.m_resultBlockInfo[totalIndex].m_parentName = childObject.name;
            m_obj.m_resultBlockInfo[totalIndex].m_name = childObject.name;
            m_obj.m_resultBlockInfo[totalIndex].m_postion = childObject.transform.position;
            m_obj.m_resultBlockInfo[totalIndex].m_rotation = childObject.transform.rotation;
            m_obj.m_resultBlockInfo[totalIndex].m_scale = childObject.transform.localScale;
            m_obj.m_resultBlockInfo[totalIndex].m_enumBlockType = EnumBlockType.NONE;
            m_obj.m_resultBlockInfo[totalIndex].m_itemId = 0;
            m_obj.m_resultBlockInfo[totalIndex].m_quantity = 0;

            totalIndex++;
        }

        GameObject obj = null;
        BlockInfo blockInfo = null;
        for (int i = 0, length = getChildCount(childObject); i < length; i++)
        {
            obj = getChildObject(childObject, i);

            blockInfo = obj.GetComponent<BlockInfo>();

            if (blockInfo != null)
            {
                m_obj.m_resultBlockInfo[totalIndex] = new StageInfo.ResultBlockInfo();
                m_obj.m_resultBlockInfo[totalIndex].m_parentName = childObject.name;
                m_obj.m_resultBlockInfo[totalIndex].m_name = obj.name;
                m_obj.m_resultBlockInfo[totalIndex].m_postion = blockInfo.m_Postion;
                m_obj.m_resultBlockInfo[totalIndex].m_rotation = blockInfo.m_Rotation;
                m_obj.m_resultBlockInfo[totalIndex].m_scale = blockInfo.m_Scale;
                m_obj.m_resultBlockInfo[totalIndex].m_enumBlockType = blockInfo.m_EnumBlockType;
                m_obj.m_resultBlockInfo[totalIndex].m_itemId = blockInfo.m_ItemId;
                m_obj.m_resultBlockInfo[totalIndex].m_quantity = blockInfo.m_Quantity;

                totalIndex++;
            }
            else
            {
                m_obj.m_resultBlockInfo[totalIndex] = new StageInfo.ResultBlockInfo();
                m_obj.m_resultBlockInfo[totalIndex].m_parentName = childObject.name;
                m_obj.m_resultBlockInfo[totalIndex].m_name = obj.name;
                m_obj.m_resultBlockInfo[totalIndex].m_postion = obj.transform.position;
                m_obj.m_resultBlockInfo[totalIndex].m_rotation = obj.transform.rotation;
                m_obj.m_resultBlockInfo[totalIndex].m_scale = obj.transform.localScale;
                m_obj.m_resultBlockInfo[totalIndex].m_enumBlockType = EnumBlockType.NONE;
                m_obj.m_resultBlockInfo[totalIndex].m_itemId = 0;
                m_obj.m_resultBlockInfo[totalIndex].m_quantity = 0;

                totalIndex++;
                totalIndex = setValueChild(obj, totalIndex);
            }
        }

        return totalIndex;
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

    private int getTotalChildCount(GameObject childObject, int count = 0)
    {
        int totalCount = count;
        int currentCount = getChildCount(childObject);

        GameObject obj = null;
        for (int i = 0; i < currentCount; i++)
        {
            obj = getChildObject(childObject, i);

            BlockInfo blockInfo = obj.GetComponent<BlockInfo>();
            if (blockInfo != null)
            {
                return currentCount;
            }
            else
            {
                totalCount += getTotalChildCount(obj, currentCount);
            }
        }

        totalCount++;
        return totalCount;
    }

    private int getChildCount(GameObject parent)
    {
        return parent.transform.childCount;
    }

    private GameObject getChildObject(GameObject parent, int index)
    {
        return parent.transform.GetChild(index).gameObject;
    }
}
