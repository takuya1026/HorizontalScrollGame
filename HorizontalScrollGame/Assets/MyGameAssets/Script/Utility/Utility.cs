﻿
//============================================================
// @file Utility
// @brief ユーティリティ
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static int ROTATE360 = 360;

    /// <summary>
    /// 2点の座標から角度を求める
    /// </summary>
    /// <param name="start">現在の座標</param>
    /// <param name="target">目的の座標</param>
    /// <returns>角度</returns>
    public static float AskAngle(Vector2 start, Vector2 target)
    {
        Vector2 diff = target - start;
        float rad = Mathf.Atan2(diff.y, diff.x);
        float deg = rad * Mathf.Rad2Deg;
        deg += ROTATE360;
        deg %= ROTATE360;
        return deg;
    }

    /// <summary>
    /// 2点の座標から角度を求める (xy軸)
    /// </summary>
    /// <param name="start">現在の座標</param>
    /// <param name="target">目的の座標</param>
    /// <returns>角度</returns>
    public static float AskAngle(Vector3 start, Vector3 target)
    {
        float diffX = target.x - start.x;
        float diffY = target.y - start.y;
        float rad = Mathf.Atan2(diffY, diffX);
        float deg = rad * Mathf.Rad2Deg;
        deg += ROTATE360;
        deg %= ROTATE360;
        return deg;
    }

    /// <summary>
    /// 2点の座標から角度を求める
    /// </summary>
    /// <param name="startX">現在の座標 - X</param>
    /// <param name="startY">現在の座標 - Y</param>
    /// <param name="targetX">目的の座標 - X</param>
    /// <param name="targetY">目的の座標 - Y</param>
    /// <returns>角度</returns>
    public static float AskAngle(float startX, float startY, float targetX, float targetY)
    {
        float diffX = targetX - startX;
        float diffY = targetY - startY;
        float rad = Mathf.Atan2(diffY, diffX);
        float deg = rad * Mathf.Rad2Deg;
        deg += ROTATE360;
        deg %= ROTATE360;
        return deg;
    }

}
