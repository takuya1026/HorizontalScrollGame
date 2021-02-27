
//============================================================
// @file BlockManager
// @brief ステージマネージャ
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : SingletonMonoBehaviour<BlockManager>
{
    private List<GameObject> m_objects = new List<GameObject>(100);
    private List<BlockBase> m_blocks = new List<BlockBase>(100);

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Init()
    {
        m_blocks.Clear();

        BlockBase blockBase = null;
        for (int i = 0, count = m_objects.Count; i < count; i++)
        {
            blockBase = m_objects[i].GetComponent<BlockBase>();

            if (blockBase == null)
            {
                continue;
            }

            blockBase.Init();
            m_blocks.Add(blockBase);

            blockBase = null;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Update()
    {
        for (int i = 0, count = m_objects.Count; i < count; i++)
        {
            m_blocks[i].Execute();
        }
    }

    /// <summary>
    /// マネージャ管理下のオブジェクトを追加
    /// </summary>
    /// <param name="gameObject">オブジェクト</param>
    public void AddObjects(GameObject gameObject)
    {
        m_objects.Add(gameObject);
    }

}
