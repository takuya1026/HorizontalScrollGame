
//============================================================
// @file LoadDataFile
// @brief データファイルの読み込み
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// データファイルの読み込み
/// </summary>
public class LoadDataFile
{
    private int m_width = 0;
    private int m_height = 0;
    private string m_fileName = "";
    private List<string> m_datas = null;

    /// <summary>
    /// 幅
    /// </summary>
    public int m_Width { get { return m_width; } }

    /// <summary>
    /// 高さ
    /// </summary>
    public int m_Height { get { return m_height; } }

    /// <summary>
    /// ファイル名
    /// </summary>
    public string m_FileName { get { return m_fileName; } }

    /// <summary>
    /// データ
    /// </summary>
    public List<string> m_Datas { get { return m_datas; } }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public LoadDataFile()
    {
        m_width = 0;
        m_height = 0;
        m_fileName = "";
        m_datas = new List<string>();
    }

    /// <summary>
    /// デストラクタ
    /// </summary>
    ~LoadDataFile()
    {
        // 処理なし
    }

    /// <summary>
    /// データの削除
    /// </summary>
    public void Remove()
    {
        m_width = 0;
        m_height = 0;
        m_fileName = "";

        if (m_datas != null)
        {
            m_datas.Clear();
        }
        else
        {
            m_datas = new List<string>();
        }
    }

    /// <summary>
    /// ファイルの読み込み
    /// </summary>
    /// <param name="fileName"></param>
    public void LoadFile(string fileName)
    {
        m_fileName = fileName;
        StreamReader streamReader = new StreamReader(m_fileName);

        if (streamReader == null)
        {
            return;
        }

        string line = "";
        string[] values = null;

        while (! streamReader.EndOfStream)
        {
            line = streamReader.ReadLine();
            values = line.Split(',');

            foreach (var value in values)
            {
                m_datas.Add(value);
            }

            m_height++;
        }

        m_width = values.Length;
    }

    /// <summary>
    /// データの表示
    /// </summary>
    public void PrintData()
    {
        Debug.Log("width: " + m_width + " | height: " + m_height);

        int height = 0;
        int width = 0;
        string stringData = "";

        foreach (var data in m_datas)
        {
            if (width >= m_width)
            {
                height++;

                Debug.Log(height + " | " + stringData);

                width = 0;
                stringData = "";
            }

            width++;
            stringData += data + ",";
        }
    }
}
