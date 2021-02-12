
//============================================================
// @file EditJsonExport
// @brief シーンの読み込みと書き出し
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using System.IO;
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
                setForExport(childCategoryObject, 0);
            }
        }

        string filePath = (m_parentObj.name + ".json");
        fileBackUp(filePath);
        JsonReadWrite.m_Instance.WriteFile<StageInfo>(m_obj, (COMMON_PATH + filePath));
    }

    /// <summary>
    /// 出力ファイルのバックアップ
    /// </summary>
    /// <param name="path"></param>
    private void fileBackUp(string path)
    {
        string filePath = (COMMON_PATH + path);
        if (File.Exists(filePath))
        {
            string backUpPath = (COMMON_PATH + "Backup/" + path);
            if (File.Exists(backUpPath))
            {
                File.Delete(backUpPath);
            }

            File.Copy(filePath, backUpPath);
        }
    }

    /// <summary>
    /// 書き出し用の変数に値を割り当てる
    /// </summary>
    /// <param name="childObject">子オブジェクト</param>
    /// <param name="index">再帰用：現在の書き出し用変数の添え字</param>
    /// <returns>再帰用：現在の書き出し用変数の添え字</returns>
    private int setForExport(GameObject childObject, int index = 0)
    {
        int totalIndex = index;

        if (totalIndex == 0)
        {
            setForExportValue(childObject, totalIndex, m_parentObj.name);

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
                setForExportValue(blockInfo, totalIndex, childObject.name);

                totalIndex++;
            }
            else
            {
                setForExportValue(obj, totalIndex, childObject.name);

                totalIndex++;
                totalIndex = setForExport(obj, totalIndex);
            }
        }

        return totalIndex;
    }

    /// <summary>
    /// 書き出し用の変数に値を設定
    /// </summary>
    /// <param name="exportObject">書き出しオブジェクト</param>
    /// <param name="index">書き出し用変数の添え字</param>
    /// <param name="parentName">親オブジェクトの名前</param>
    private void setForExportValue(GameObject exportObject, int index, string parentName)
    {
        m_obj.m_resultBlockInfo[index] = new StageInfo.ResultBlockInfo();
        m_obj.m_resultBlockInfo[index].m_parentName     = parentName;
        m_obj.m_resultBlockInfo[index].m_name           = exportObject.name;
        m_obj.m_resultBlockInfo[index].m_postion        = exportObject.transform.position;
        m_obj.m_resultBlockInfo[index].m_rotation       = exportObject.transform.rotation;
        m_obj.m_resultBlockInfo[index].m_scale          = exportObject.transform.localScale;
        m_obj.m_resultBlockInfo[index].m_enumBlockType  = EnumBlockType.NONE;
        m_obj.m_resultBlockInfo[index].m_itemId         = 0;
        m_obj.m_resultBlockInfo[index].m_quantity       = 0;
    }

    /// <summary>
    /// 書き出し用の変数に値を設定
    /// </summary>
    /// <param name="exportObject">書き出しオブジェクト</param>
    /// <param name="index">書き出し用変数の添え字</param>
    /// <param name="parentName">親オブジェクトの名前</param>
    private void setForExportValue(BlockInfo exportInfo, int index, string parentName)
    {
        m_obj.m_resultBlockInfo[index] = new StageInfo.ResultBlockInfo();
        m_obj.m_resultBlockInfo[index].m_parentName     = parentName;
        m_obj.m_resultBlockInfo[index].m_name           = exportInfo.name;
        m_obj.m_resultBlockInfo[index].m_postion        = exportInfo.transform.position;
        m_obj.m_resultBlockInfo[index].m_rotation       = exportInfo.transform.rotation;
        m_obj.m_resultBlockInfo[index].m_scale          = exportInfo.transform.localScale;
        m_obj.m_resultBlockInfo[index].m_enumBlockType  = exportInfo.m_EnumBlockType;
        m_obj.m_resultBlockInfo[index].m_itemId         = exportInfo.m_ItemId;
        m_obj.m_resultBlockInfo[index].m_quantity       = exportInfo.m_Quantity;
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

    /// <summary>
    /// 全ての子供の総数を取得する
    /// </summary>
    /// <param name="childObject">子オブジェクト</param>
    /// <param name="count">再帰用：現在のオブジェクト数</param>
    /// <returns>全ての子供の総数</returns>
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

    /// <summary>
    /// 子オブジェクトの総数
    /// </summary>
    /// <param name="parent">親オブジェクト</param>
    /// <returns>子オブジェクトの総数</returns>
    private int getChildCount(GameObject parent)
    {
        return parent.transform.childCount;
    }

    /// <summary>
    /// 子オブジェクトの取得
    /// </summary>
    /// <param name="parent">親オブジェクト</param>
    /// <param name="index">子オブジェクトの添え字</param>
    /// <returns>子オブジェクトの取得</returns>
    private GameObject getChildObject(GameObject parent, int index)
    {
        return parent.transform.GetChild(index).gameObject;
    }
}
