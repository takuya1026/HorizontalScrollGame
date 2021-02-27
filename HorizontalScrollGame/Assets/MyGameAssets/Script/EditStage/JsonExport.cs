
//============================================================
// @file EditJsonExport
// @brief シーンの書き出し
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Json形式に書き出し
/// </summary>
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
        if (m_parentObj == null)
        {
            Debug.Log("ERORR: There is no parent object. (JsonExport#SeavStageInfo)");
            return;
        }

        m_obj = new StageInfo();
        m_obj.m_stageName = m_parentObj.name;

        GameObject childCategoryObject = null;
        for (int categoryIndex = 0, categoryLength = getChildCount(m_parentObj); categoryIndex < categoryLength; categoryIndex++)
        {
            childCategoryObject = getChildObject(m_parentObj, categoryIndex);
            if (childCategoryObject.name == "Stage")
            {
                List<GameObject> objecs = getChildList(childCategoryObject);
                m_obj.m_resultBlockInfo = new StageInfo.ResultBlockInfo[objecs.Count];
                setForExport(objecs);
            }
        }

        string filePath = (m_parentObj.name + ".json");
        fileBackUp(filePath);
        JsonReadWrite.m_Instance.WriteFile<StageInfo>(m_obj, (COMMON_PATH + filePath));
    }

    /// <summary>
    /// 出力ファイルのバックアップ
    /// </summary>
    /// <param name="path">パス</param>
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
    private void setForExport(List<GameObject> objects)
    {
        BlockBase blockBase = null;
        Renderer renderer = null;
        string texName = "";
        for (int i = 0, length = objects.Count; i < length; i++)
        {
            blockBase = objects[i].GetComponent<BlockBase>();

            if (blockBase != null)
            {
                renderer = objects[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    texName = renderer.material.mainTexture.name;
                    renderer = null;
                }

                setForExportValue(blockBase, i, objects[i].transform.parent.name, texName);
            }
            else
            {
                setForExportValue(objects[i], i, objects[i].transform.parent.name);
            }
        }
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
        m_obj.m_resultBlockInfo[index].m_texName        = "";
    }

    /// <summary>
    /// 書き出し用の変数に値を設定
    /// </summary>
    /// <param name="exportObject">書き出しオブジェクト</param>
    /// <param name="index">書き出し用変数の添え字</param>
    /// <param name="parentName">親オブジェクトの名前</param>
    private void setForExportValue(BlockBase exportInfo, int index, string parentName, string texName)
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
        m_obj.m_resultBlockInfo[index].m_texName        = texName;

    }

    /// <summary>
    /// 現在のファイル数
    /// </summary>
    /// <returns>現在のファイル数</returns>
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
    /// <returns>次のファイル番号</returns>
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
    /// 子のリスト
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private List<GameObject> getChildList(GameObject obj)
    {
        List<GameObject> allChildren = new List<GameObject>();
        getChildren(obj, ref allChildren);
        return allChildren;
    }

    /// <summary>
    /// 子の取得
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="allChildren"></param>
    public static void getChildren(GameObject obj, ref List<GameObject> allChildren)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            allChildren.Add(ob.gameObject);
            getChildren(ob.gameObject, ref allChildren);
        }
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
