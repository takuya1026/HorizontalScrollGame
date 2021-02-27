
//============================================================
// @file JsonImport
// @brief ファイルの読み込み
// @autor ochi.takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ファイルの読み込み
/// </summary>
public class JsonImport : MonoBehaviour
{
    private StageInfo m_obj;

    [SerializeField, Tooltip("読み込むファイル名")]
    private string m_importFile = null;

    private readonly string COMMON_PATH = "Assets/MyGameAssets/Data/Stage/";

    /// <summary>
    /// ファイルの読み込み
    /// </summary>
    public void GetStageInfo(System.Action<StageInfo> coalback = null)
    {
        if (m_importFile == "")
        {
            Debug.Log("file name does not exist. (EditSceneReadWrite#JsonImport)");
            return;
        }

        m_obj = JsonReadWrite.m_Instance.ReadFile<StageInfo>((COMMON_PATH + m_importFile + ".json"));
        if (m_obj == default)
        {
            Debug.Log("the file cannot be read. (EditSceneReadWrite#JsonImport)");
            return;
        }

        coalback?.Invoke(m_obj);

        JsonExport editJsonExport = gameObject.GetComponent<JsonExport>();
        if (editJsonExport != null)
        {
            editJsonExport.m_ParentObj = GameObject.Find(m_importFile);
        }
    }
}
