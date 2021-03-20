
//============================================================
// @file ReadUtility
// @brief 読み込み系 ユーティリティ
//============================================================

using System.IO;
using UnityEngine;

/// <summary>
/// ユーティリティ
/// </summary>
namespace Utility
{
    /// <summary>
    /// 読み込み系
    /// </summary>
    public static class Read
    {
        /// <summary>
        /// テクスチャ読み込み
        /// </summary>
        /// <param name="path">画像のパス</param>
        /// <returns>テクスチャ</returns>
        public static Texture ReadTexture(string path)
        {
            byte[] readBinary = Utility.Read.ReadTextureFile(path);

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(readBinary);

            return texture;
        }

        /// <summary>
        /// テクスチャファイル読み込み
        /// </summary>
        /// <param name="path">画像のパス</param>
        /// <returns>テクスチャファイル</returns>
        public static byte[] ReadTextureFile(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            byte[] values = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);

            binaryReader.Close();

            return values;
        }
    }

}
