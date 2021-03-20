
//============================================================
// @file MathUtility
// @brief 数学系 ユーティリティ
//============================================================

using UnityEngine;

/// <summary>
///  ユーティリティ
/// </summary>
namespace Utility
{
    /// <summary>
    /// 数学系
    /// </summary>
    public static class Math
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

        /// <summary>
        /// 角度をベクトルに変換
        /// </summary>
        /// <param name="degree">角度</param>
        /// <returns>変換後のベクトル</returns>
        public static Vector3 AngleToVector(float degree)
        {
            float radAngle = degree * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0);
        }
    }
}
