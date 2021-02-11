
//============================================================
// @file Cell
// @brief セル
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セル
/// </summary>
public class Cell
{
    private int m_x;
    private int m_y;
    private int m_width;
    private int m_height;
    private int m_type;
    private int m_index;

    /// <summary>
    /// 座標 X
    /// </summary>
    public int m_X
    {
        set { m_x = value; }
        get { return m_x; }
    }

    /// <summary>
    /// 座標 Y
    /// </summary>
    public int m_Y
    {
        set { m_y = value; }
        get { return m_y; }
    }

    /// <summary>
    /// 幅
    /// </summary>
    public int m_Width
    {
        set { m_width = value; }
        get { return m_width; }
    }

    /// <summary>
    /// 高さ
    /// </summary>
    public int m_Height
    {
        set { m_height = value; }
        get { return m_height; }
    }

    /// <summary>
    /// セルのタイプ
    /// </summary>
    public int m_Type
    {
        set { m_type = value; }
        get { return m_type; }
    }

    /// <summary>
    /// セルの添え字
    /// </summary>
    public int m_Index
    {
        set { m_index = value; }
        get { return m_index; }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Cell()
    {
        m_x = 0;
        m_y = 0;
        m_width = 0;
        m_height = 0;
        m_type = 0;
        m_index = 0;
    }

    /// <summary>
    /// デストラクタ
    /// </summary>
    ~Cell()
    {
        // 処理なし
    }

    /// <summary>
    /// ポジションの設定
    /// </summary>
    /// <param name="x">座標 X</param>
    /// <param name="y">座標 Y</param>
    public void SetPosition(int x, int y)
    {
        m_x = x;
        m_y = y;
    }

    /// <summary>
    /// ポジションの設定
    /// </summary>
    /// <param name="x">座標 X</param>
    /// <param name="y">座標 Y</param>
    public void SetPosition(float x, float y)
    {
        m_x = (int)x;
        m_y = (int)y;
    }

    /// <summary>
    /// ポジションの取得
    /// </summary>
    public Cell GetPosition()
    {
        Cell cell = new Cell();
        cell.m_x = m_x;
        cell.m_y = m_y;
        return cell;
    }

    /// <summary>
    /// ディープコピー
    /// </summary>
    /// <param name="copy">セル</param>
    public void DeepCopy(Cell copy)
    {
        m_x = copy.m_x;
        m_y = copy.m_y;
        m_type = copy.m_type;
        m_index = copy.m_index;
    }

    /// <summary>
    /// 平方根
    /// </summary>
    public float SqrMagnitude
    {
        get { return (m_x * m_x + m_y * m_y); }
    }

    /// <summary>
    /// operator +
    /// </summary>
    /// <param name="a">セル 1</param>
    /// <param name="b">セル 2</param>
    /// <returns>セル</returns>
    public static Cell operator +(Cell a, Cell b)
    {
        Cell cell = new Cell();
        cell.m_x = a.m_x + b.m_x;
        cell.m_y = a.m_y + b.m_y;
        cell.m_Type = 0;
        return cell;
    }

    /// <summary>
    /// operator -
    /// </summary>
    /// <param name="a">セル 1</param>
    /// <param name="b">セル 2</param>
    /// <returns>セル</returns>
    public static Cell operator -(Cell a, Cell b)
    {
        Cell cell = new Cell();
        cell.m_x = a.m_x - b.m_x;
        cell.m_y = a.m_y - b.m_y;
        cell.m_Type = 0;
        return cell;
    }

    /// <summary>
    /// セルの座標が等しいか
    /// </summary>
    /// <param name="a">セル 1</param>
    /// <param name="b">セル 2</param>
    /// <returns>セルの座標が等しい場合 true</returns>
    public static bool IsEqual(Cell a, Cell b)
    {
        if (a.m_x == b.m_x && a.m_y == b.m_y)
        {
            return true;
        }
        return false;
    }
}
