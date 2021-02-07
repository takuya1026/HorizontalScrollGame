
//============================================================
// @file StageController
// @brief ステージコントローラー
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージコントローラー (シングルトン)
/// </summary>
public class StageController : SingletonMonoBehaviour<StageController>
{
    private int m_width = 0;
    private int m_height = 0;
    private Cell[] m_cell = null;

    // private readonly
    private readonly Vector2Int OFF_SET_UP = new Vector2Int(0, 1);
    private readonly Vector2Int OFF_SET_DOWN = new Vector2Int(0, -1);
    private readonly Vector2Int OFF_SET_LEFT = new Vector2Int(-1, 0);
    private readonly Vector2Int OFF_SET_RIGHT = new Vector2Int(1, 0);

    /// <summary>
    /// 幅
    /// </summary>
    public int m_Width
    {
        get { return m_width; }
    }

    /// <summary>
    /// 高さ
    /// </summary>
    public int m_Height
    {
        get { return m_height; }
    }

    /// <summary>
    /// 長さ
    /// </summary>
    public int m_Length
    {
        get { return m_cell.Length; }
    }

    /// <summary>
    /// 作成
    /// </summary>
    public void Create(LoadDataFile dataFile)
    {
        m_width = dataFile.m_Width;
        m_height = dataFile.m_Height;
        int index = (m_width * m_height);
        m_cell = new Cell[index];

        // 各要素のコンストラクタを明示的に呼ぶ
        for (int i = 0; i < index; i++)
        {
            m_cell[i] = new Cell();
        }

        for (int y = 0; y < m_height; y++)
        {
            for (int x = 0; x < m_width; x++)
            {
                if (IsOutOfRange(x, y))
                {
                    Debug.Log("position [ x: " + x + ", y: " + y + " ] is out of rangr. (SetMapData#MapController)");
                    return;
                }

                int cellIndex = GetIndexByPosition(x, y);
                m_cell[cellIndex].m_X = x;
                m_cell[cellIndex].m_Y = y;
                m_cell[cellIndex].m_Type = int.Parse((dataFile.m_Datas[cellIndex] != null ? dataFile.m_Datas[cellIndex] : "0"));
                m_cell[cellIndex].m_Index = index;
            }
        }
    }

    /// <summary>
    /// 座標からインデックスを返す
    /// </summary>
    /// <param name="x">座標 X</param>
    /// <param name="y">座標 Y</param>
    /// <returns>インデックスを返す</returns>
    public int GetIndexByPosition(int x, int y)
    {
        return x + (y * m_width);
    }

    /// <summary>
    /// 指定座標が範囲内か
    /// </summary>
    /// <param name="x">座標 X</param>
    /// <param name="y">座標 Y</param>
    /// <returns>範囲外: true, 範囲内: false</returns>
    public bool IsOutOfRange(int x, int y)
    {
        if (x < 0 || x >= m_width)
        {
            return true;
        }

        if (y < 0 || y >= m_height)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// インデックスで指定した座標が範囲内か
    /// </summary>
    /// <param name="index">インデックス</param>
    /// <returns>範囲外: true, 範囲内: false</returns>
    public bool IsOutOfRange(int index)
    {
        var x = m_cell[index].m_X;
        var y = m_cell[index].m_Y;

        if (x < 0 || x >= m_width)
        {
            return true;
        }

        if (y < 0 || y >= m_height)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 指定座標がタイプの取得
    /// </summary>
    /// <param name="x">座標 X</param>
    /// <param name="y">座標 Y</param>
    /// <returns>セルのタイプ</returns>
    public int GetType(int x, int y)
    {
        if (IsOutOfRange(x, y))
        {
            Debug.Log("position [ x: " + x + ", y: " + y + " ] is out of rangr. (StageController#GetType)");
            return -1;
        }

        int index = GetIndexByPosition(x, y);
        return m_cell[index].m_Type;
    }

    /// <summary>
    /// 指定座標がタイプの取得
    /// </summary>
    /// <param name="index">インデックス</param>
    /// <returns>セルのタイプ</returns>
    public int GetType(int index)
    {
        var cell = GetCell(index);
        if (IsOutOfRange(index))
        {
            Debug.Log("index [" + index + "] is out of rangr. (StageController#GetCell)");
            return -1;
        }

        int cellIndex = GetIndexByPosition(cell.m_X, cell.m_Y);
        return m_cell[cellIndex].m_Type;
    }

    /// <summary>
    /// 指定座標がセルの取得
    /// </summary>
    /// <param name="x">座標 X</param>
    /// <param name="y">座標 Y</param>
    /// <returns>指定座標のセル</returns>
    public Cell GetCell(int x, int y)
    {
        if (IsOutOfRange(x, y))
        {
            Debug.Log("position [ x: " + x + ", y: " + y + " ] is out of rangr. (StageController#GetCell)");
            return null;
        }

        int index = GetIndexByPosition(x, y);
        return m_cell[index];
    }

    /// <summary>
    /// 指定座標がセルの取得
    /// </summary>
    /// <param name="index">インデックス</param>
    /// <returns></returns>
    public Cell GetCell(int index)
    {
        if (IsOutOfRange(index))
        {
            Debug.Log("index [" + index + "] is out of rangr. (StageController#GetCell)");
            return null;
        }

        return m_cell[index];
    }

    /// <summary>
    /// 指定座標の周辺を取得
    /// </summary>
    /// <param name="x">座標 X</param>
    /// <param name="y">座標 Y</param>
    /// <returns></returns>
    public List<Cell> GetAdjacences(int x, int y)
    {
        var adjacences = new List<Cell>();
        var offsets = new Vector2Int[] { OFF_SET_LEFT, OFF_SET_UP, OFF_SET_RIGHT, OFF_SET_DOWN };
        for (int i = 0; i < offsets.Length; i++)
        {
            Cell cell = GetCell(x + offsets[i].x, y + offsets[i].y);
            if (cell != null)
            {
                adjacences.Add(cell);
            }
        }
        return adjacences;
    }
}
