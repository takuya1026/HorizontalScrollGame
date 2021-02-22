
//============================================================
// @file EnumBlockType
// @brief enum：ブロックタイプ
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enum：ブロックタイプ
/// </summary>
public enum EnumBlockType
{
    NONE,           // なし
    GRASS_SOIL,     // 草土
    GRASS_SAND,     // 草砂
    BREAK_BLOCK,    // ブロック(破壊)
    SOIL,           // 土
    SAND,           // 砂
    COIN_BLOCK,     // ブロック(コイン)
    ITEM_BLOCK,     // ブロック(アイテム)
    GET_BLOCK,      // ブロック(取得済み)
    NUM,            // 番兵
}
