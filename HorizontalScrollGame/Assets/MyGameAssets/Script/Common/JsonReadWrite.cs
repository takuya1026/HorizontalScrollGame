
//============================================================
// @file JsonReadWrite
// @brief Json形式の読み込みと書き込み
// @autor ochi takuya
//============================================================

using System.IO;
using UnityEngine;

public class JsonReadWrite : SingletonMonoBehaviour<JsonReadWrite>
{
    /// <summary>
    /// Json形式で指定ファイルに書き出し
    /// </summary>
    /// <typeparam name="T">object Type</typeparam>
    /// <param name="obj">object</param>
    /// <param name="path">path</param>
    public void WriteFile<T>(T obj, string path)
    {
        var json = JsonUtility.ToJson(obj, true);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// 指定ファイルをJson形式で読み込み
    /// </summary>
    /// <typeparam name="T">object Type</typeparam>
    /// <param name="path">path</param>
    /// <returns>Json Object</returns>
    public T ReadFile<T>(string path)
    {
        if (! IsFileExists(path))
        {
            return default;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }

    /// <summary>
    /// ファイルは存在するか
    /// </summary>
    /// <param name="path">path</param>
    /// <returns>ファイルは存在する: true</returns>
    public bool IsFileExists(string path)
    {
        if (! File.Exists(path))
        {
            return false;
        }
        return true;
    }
}
