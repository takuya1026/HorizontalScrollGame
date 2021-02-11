
//============================================================
// @file StageGenerator
// @brief ステージ生成
// @autor 天才が作った
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ生成
/// </summary>
public class StageGenerator : SingletonMonoBehaviour<StageGenerator>
{
    private GameObject m_baseObject = null;
    private GameObject m_parentObject = null;

    [SerializeField, Tooltip("デフォルトブロック")]
    private GameObject DEFAULT_BLOCK_OBJECT = null;

    /// <summary>
    /// 描画
    /// </summary>
    public void Generate(StageInfo stageInfo)
    {
        if (! DEFAULT_BLOCK_OBJECT)
        {
            Debug.Log("ERROR: default block object is null. (StageController#placement)");
            return;
        }

        m_baseObject = GameObject.Find(stageInfo.m_stageName);
        if (m_baseObject != null)
        {
            Debug.Log("stage object 【 " + stageInfo.m_stageName  + " 】is Exists on the field. (StageGenerator#Generate)");
            return;
        }

        GameObject newObject = null;
        BlockInfo blockInfo = null;
        int imageIndex = 0;
        for (int i = 0, length = stageInfo.m_resultBlockInfo.Length; i < length; i++)
        {
            if (m_baseObject == null)
            {
                m_baseObject = new GameObject(stageInfo.m_stageName);
            }
            if (m_parentObject == null)
            {
                m_parentObject = new GameObject("Stage");
                m_parentObject.transform.parent = m_baseObject.transform;
            }

            newObject = Instantiate(DEFAULT_BLOCK_OBJECT);
            newObject.name = stageInfo.m_resultBlockInfo[i].m_name;
            newObject.transform.position = stageInfo.m_resultBlockInfo[i].m_postion;
            newObject.transform.rotation = stageInfo.m_resultBlockInfo[i].m_rotation;
            newObject.transform.localScale = stageInfo.m_resultBlockInfo[i].m_scale;

            blockInfo = newObject.GetComponent<BlockInfo>();
            blockInfo.m_EnumBlockType = stageInfo.m_resultBlockInfo[i].m_enumBlockType;
            blockInfo.m_ItemId = stageInfo.m_resultBlockInfo[i].m_itemId;
            blockInfo.m_Quantity = stageInfo.m_resultBlockInfo[i].m_quantity;

            imageIndex = (int)stageInfo.m_resultBlockInfo[i].m_enumBlockType;
            newObject.GetComponent<Renderer>().sharedMaterial = (Material)Resources.Load("Texture/Stage/Materials/MapTexture" + imageIndex.ToString("D2"));

            newObject.transform.parent = m_parentObject.transform;
        }
    }
}
