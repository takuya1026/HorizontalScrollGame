
//============================================================
// @file StageGenerator
// @brief ステージ生成
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ生成
/// </summary>
public class StageGenerator : MonoBehaviour
{
    private LoadDataFile m_loadDataFile = null;
    private GameObject m_emptyObject = null;
    private bool isA = false;

    // private readonly
    private readonly string COMMON_PATH = "Assets/MyGameAssets/Stage/DataFiles/";

    [SerializeField, Tooltip("草土")]
    private GameObject GRASS_SOIL_OBJECT = null;

    [SerializeField, Tooltip("草砂")]
    private GameObject GRASS_SAND_OBJECT = null;

    [SerializeField, Tooltip("ブロック(破壊)")]
    private GameObject BREAK_BLOCK_OBJECT = null;

    [SerializeField, Tooltip("土")]
    private GameObject SOIL_OBJECT = null;

    [SerializeField, Tooltip("砂")]
    private GameObject SAND_OBJECT = null;

    [SerializeField, Tooltip("ブロック(コイン)")]
    private GameObject COIN_BLOCK_OBJECT = null;

    [SerializeField, Tooltip("ブロック(アイテム)")]
    private GameObject ITEM_BLOCK_OBJECT = null;

    [SerializeField, Tooltip("ブロック(取得済み)")]
    private GameObject GET_BLOCK_OBJECT = null;

    /// <summary>
    /// ロードファイル
    /// </summary>
    public LoadDataFile m_LoadDataFile { get { return m_loadDataFile; } }

    /// <summary>
    /// データ
    /// </summary>
    public List<string> m_Datas { get { return m_loadDataFile.m_Datas; } }

    /// <summary>
    /// 開始
    /// </summary>
    private void Awake()
    {
        m_loadDataFile = new LoadDataFile();
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        if (isA)
        {
            return;
        }
        isA = true;

        m_loadDataFile.Remove();
        m_loadDataFile.LoadFile(COMMON_PATH + "Stage01.csv");

        if (m_loadDataFile.m_FileName == "" || m_loadDataFile.m_Datas == null)
        {
            Debug.LogError("ERROR: there are no files. (StageController#Create)");
            return;
        }

        StageController.m_Instance.Create(m_loadDataFile);

        drow();
    }

    //==================================================
    //  @brief drow
    //==================================================
    public void drow()
    {
        for (int i = 0, length = StageController.m_Instance.m_Length; i < length; i++)
        {
            var cell = StageController.m_Instance.GetCell(i);
            switch (cell.m_Type)
            {
                case (int)StageInfo.GRASS_SOIL:
                    placement(GRASS_SOIL_OBJECT, new Vector3(cell.m_X, cell.m_Y, 0.0f), ("GrassSoil_" + i));
                    break;

                case (int)StageInfo.GRASS_SAND:
                    placement(GRASS_SAND_OBJECT, new Vector3(cell.m_X, cell.m_Y, 0.0f), ("GrassSand_" + i));
                    break;

                case (int)StageInfo.BREAK_BLOCK:
                    placement(BREAK_BLOCK_OBJECT, new Vector3(cell.m_X, cell.m_Y, 0.0f), ("BreakBlock_" + i));
                    break;

                case (int)StageInfo.SOIL:
                    placement(SOIL_OBJECT, new Vector3(cell.m_X, cell.m_Y, 0.0f), ("Soil_" + i));
                    break;

                case (int)StageInfo.SAND:
                    placement(SAND_OBJECT, new Vector3(cell.m_X, cell.m_Y, 0.0f), ("Sand_" + i));
                    break;

                case (int)StageInfo.COIN_BLOCK:
                    placement(COIN_BLOCK_OBJECT, new Vector3(cell.m_X, cell.m_Y, 0.0f), ("CoinBlock_" + i));
                    break;

                case (int)StageInfo.ITEM_BLOCK:
                    placement(ITEM_BLOCK_OBJECT, new Vector3(cell.m_X, cell.m_Y, 0.0f), ("ItemBlock_" + i));
                    break;

                case (int)StageInfo.GET_BLOCK:
                    placement(GET_BLOCK_OBJECT, new Vector3(cell.m_X, cell.m_Y, 0.0f), ("GetBlock_" + i));
                    break;
            }
        }

        GameObject stageData = GameObject.Find("_StageData");
        stageData.transform.Rotate(new Vector3(0.0f, 0.0f, 180.0f));
    }

    //==================================================
    //  @brief placement
    //==================================================
    private void placement(GameObject originalObject, Vector3 position, string name)
    {
        if (! originalObject)
        {
            Debug.Log("ERROR: wall object is null. (StageController#placement)");
            return;
        }

        if (! m_emptyObject)
        {
            m_emptyObject = new GameObject("_StageData");
        }

        GameObject newObject = Instantiate(originalObject, position, Quaternion.identity);
        newObject.name = name;
        newObject.transform.Rotate(new Vector3(0.0f, 0.0f, -180.0f));
        newObject.transform.parent = m_emptyObject.transform;
    }
}
