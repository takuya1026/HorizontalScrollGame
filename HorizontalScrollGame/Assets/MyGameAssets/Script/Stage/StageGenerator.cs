
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

    [SerializeField, Tooltip("デフォルトブロックオブジェクト")]
    private GameObject DEFAULT_BLOCK_OBJECT = null;

    /// <summary>
    /// ステージオブジェクトの生成
    /// </summary>
    public void Generate(StageInfo stageInfo)
    {
        // NOTE: エディタ実行をしないため、毎回初期化
        m_gropObjectIndex.Clear();
        m_gropObject.Clear();

        if (DEFAULT_BLOCK_OBJECT == null)
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

        createIntermediaryObject(stageInfo);

        createBlockObject(stageInfo);
    }

    /// <summary>
    /// 各仲業者オブジェクトの生成
    /// </summary>
    /// <param name="stageInfo">ステージ情報</param>
    private void createIntermediaryObject(StageInfo stageInfo)
    {
        for (int i = 0, length = stageInfo.m_resultBlockInfo.Length; i < length; i++)
        {
            if (m_parentObject == null)
            {
                continue;
            }

            if (createParentLinkObject(stageInfo, i, "Stage", m_parentObject))
            {
                continue;
            }

            if (! m_gropObject.ContainsKey(stageInfo.m_resultBlockInfo[i].m_parentName))
            {
                continue;
            }

            if (createParentLinkObject(stageInfo, i, "Group", m_gropObject[stageInfo.m_resultBlockInfo[i].m_parentName]))
            {
                continue;
            }

            if (createParentLinkObject(stageInfo, i, "Element", m_gropObject[stageInfo.m_resultBlockInfo[i].m_parentName]))
            {
                continue;
            }
        }
    }

    /// <summary>
    /// 各ブロックオブジェクトの生成
    /// </summary>
    /// <param name="stageInfo">ステージ情報</param>
    private void createBlockObject(StageInfo stageInfo)
    {
        GameObject newObject = null;
        BlockBase blockBase = null;
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

            newObject = Instantiate(DEFAULT_BLOCK_OBJECT);
            newObject.name = stageInfo.m_resultBlockInfo[i].m_name;
            newObject.transform.position = stageInfo.m_resultBlockInfo[i].m_postion;
            newObject.transform.rotation = stageInfo.m_resultBlockInfo[i].m_rotation;
            newObject.transform.localScale = stageInfo.m_resultBlockInfo[i].m_scale;

            switch (stageInfo.m_resultBlockInfo[i].m_enumBlockType)
            {
                case EnumBlockType.NORMAL_BLOCK:
                    newObject.AddComponent<BlockNormal>();
                    blockBase = newObject.GetComponent<BlockNormal>();
                    break;

                case EnumBlockType.BREAK_BLOCK:
                    newObject.AddComponent<BlockBreak>();
                    blockBase = newObject.GetComponent<BlockBreak>();
                    break;

                case EnumBlockType.COIN_BLOCK:
                case EnumBlockType.ITEM_BLOCK:
                    newObject.AddComponent<BlockDrop>();
                    blockBase = newObject.GetComponent<BlockDrop>();
                    break;

                default:
                    newObject.AddComponent<BlockBase>();
                    blockBase = newObject.GetComponent<BlockBase>();
                    break;
            }

            blockBase.m_EnumBlockType   = stageInfo.m_resultBlockInfo[i].m_enumBlockType;
            blockBase.m_ItemId          = stageInfo.m_resultBlockInfo[i].m_itemId;
            blockBase.m_Quantity        = stageInfo.m_resultBlockInfo[i].m_quantity;
            blockBase.m_TexName         = stageInfo.m_resultBlockInfo[i].m_texName;

            imageIndex = (int)stageInfo.m_resultBlockInfo[i].m_enumBlockType - 1;
            newObject.GetComponent<Renderer>().sharedMaterial = (Material)Resources.Load("Texture/Stage/Materials/" + blockBase.m_TexName);

            newObject.transform.parent = m_gropObject[stageInfo.m_resultBlockInfo[i].m_parentName].transform;

            BlockManager.m_Instance.AddObjects(newObject);
        }

        BlockManager.m_Instance.Init();
    }

    /// <summary>
    /// 親にリンクさせたオブジェクトを作成する
    /// </summary>
    /// <param name="stageInfo">ステージ情報</param>
    /// <param name="index">ステージ情報の添え字</param>
    /// <param name="searchName">検索オブジェクト</param>
    /// <param name="parentObject">親の名前</param>
    /// <returns>true：オブジェクト生成</returns>
    private bool createParentLinkObject(StageInfo stageInfo, int index, string searchName, GameObject parentObject)
    {
        if (stageInfo.m_resultBlockInfo[index].m_name.IndexOf(searchName) >= 0)
        {
            GameObject newObject = new GameObject(stageInfo.m_resultBlockInfo[index].m_name);
            newObject.transform.parent = parentObject.transform;
            m_gropObjectIndex.Add(index);
            m_gropObject.Add(stageInfo.m_resultBlockInfo[index].m_name, newObject);
            return true;
        }
        return false;
    }
}
