
//============================================================
// @file StageGenerator
// @brief ステージ生成
// @autor ochi.takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ生成
/// </summary>
public class StageGenerator : SingletonMonoBehaviour<StageGenerator>
{
    private GameObject m_parentObject = null;

    private List<int> m_gropObjectIndex = new List<int>();
    private Dictionary<string, GameObject> m_gropObject = new Dictionary<string, GameObject>();

    [SerializeField, Tooltip("デフォルトブロック")]
    private GameObject DEFAULT_BLOCK_OBJECT = null;

    /// <summary>
    /// 描画
    /// </summary>
    public void Generate(StageInfo stageInfo)
    {
        // NOTE: エディタ実行をしないため、毎回初期化
        m_gropObjectIndex.Clear();
        m_gropObject.Clear();

        if (! DEFAULT_BLOCK_OBJECT)
        {
            Debug.Log("ERROR: default block object is null. (StageController#placement)");
            return;
        }

        m_parentObject = GameObject.Find(stageInfo.m_stageName);
        if (m_parentObject != null)
        {
            Debug.Log("stage object 【 " + stageInfo.m_stageName  + " 】is Exists on the field. (StageGenerator#Generate)");
            return;
        }

        m_parentObject = new GameObject(stageInfo.m_stageName);
        GameObject groupObject = null;
        GameObject elementObject = null;
        GameObject blockObject = null;
        BlockInfo blockInfo = null;

        string oldGroupObjectName = "";
        string oldElementObjectName = "";

        for (int i = 0, length = stageInfo.m_resultBlockInfo.Length; i < length; i++)
        {
            if (m_parentObject == null)
            {
                continue;
            }

            if (stageInfo.m_resultBlockInfo[i].m_name.IndexOf("Group") >= 0
             && stageInfo.m_resultBlockInfo[i].m_name != oldGroupObjectName)
            {
                if (m_gropObject.ContainsKey(stageInfo.m_resultBlockInfo[i].m_name))
                {
                    continue;
                }

                groupObject = new GameObject(stageInfo.m_resultBlockInfo[i].m_name);
                groupObject.transform.parent = m_parentObject.transform;
                oldGroupObjectName = stageInfo.m_resultBlockInfo[i].m_name;
                m_gropObjectIndex.Add(i);
                m_gropObject.Add(stageInfo.m_resultBlockInfo[i].m_name, groupObject);
                continue;
            }

            if (stageInfo.m_resultBlockInfo[i].m_name.IndexOf("Element") >= 0
             && stageInfo.m_resultBlockInfo[i].m_name != oldElementObjectName)
            {
                if (m_gropObject.ContainsKey(stageInfo.m_resultBlockInfo[i].m_name))
                {
                    continue;
                }

                elementObject = new GameObject(stageInfo.m_resultBlockInfo[i].m_name);
                elementObject.transform.parent = groupObject.transform;
                oldElementObjectName = stageInfo.m_resultBlockInfo[i].m_name;
                m_gropObjectIndex.Add(i);
                m_gropObject.Add(stageInfo.m_resultBlockInfo[i].m_name, elementObject);
                continue;
            }
        }

        int imageIndex = 0;
        for (int i = 0, length = stageInfo.m_resultBlockInfo.Length; i < length; i++)
        {
            if (m_gropObjectIndex.IndexOf(i) >= 0)
            {
                continue;
            }

            if (!m_gropObject.ContainsKey(stageInfo.m_resultBlockInfo[i].m_parentName))
            {
                continue;
            }

            blockObject = Instantiate(DEFAULT_BLOCK_OBJECT);
            blockObject.name = stageInfo.m_resultBlockInfo[i].m_name;
            blockObject.transform.position = stageInfo.m_resultBlockInfo[i].m_postion;
            blockObject.transform.rotation = stageInfo.m_resultBlockInfo[i].m_rotation;
            blockObject.transform.localScale = stageInfo.m_resultBlockInfo[i].m_scale;

            blockInfo = blockObject.GetComponent<BlockInfo>();
            blockInfo.m_EnumBlockType = stageInfo.m_resultBlockInfo[i].m_enumBlockType;
            blockInfo.m_ItemId = stageInfo.m_resultBlockInfo[i].m_itemId;
            blockInfo.m_Quantity = stageInfo.m_resultBlockInfo[i].m_quantity;

            imageIndex = (int)stageInfo.m_resultBlockInfo[i].m_enumBlockType;
            blockObject.GetComponent<Renderer>().sharedMaterial = (Material)Resources.Load("Texture/Stage/Materials/MapTexture" + imageIndex.ToString("D2"));

            blockObject.transform.parent = m_gropObject[stageInfo.m_resultBlockInfo[i].m_parentName].transform;
        }


    }
}
