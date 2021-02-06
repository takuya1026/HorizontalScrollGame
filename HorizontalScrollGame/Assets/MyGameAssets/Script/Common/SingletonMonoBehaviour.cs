
//============================================================
// @file SingletonMonoBehaviour
// @brief シングルトン モノビヘイビア
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// シングルトン モノビヘイビア
/// </summary>
/// <typeparam name="T">class</typeparam>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T m_instance;

    /// <summary>
    /// インスタンス
    /// </summary>
    public static T m_Instance
    {
        get
        {
            if (m_instance == null)
            {
                Type type = typeof(T);

                m_instance = (T)FindObjectOfType(type);
                if (m_instance == null)
                {
                    Debug.LogError(type + " をアタッチしているGameObjectはありません");
                }
            }

            return m_instance;
        }
    }

    /// <summary>
    /// 開始
    /// </summary>
    virtual protected void Awake()
    {
        CheckInstance();
    }

    /// <summary>
    /// 他のゲームオブジェクトにアタッチされているか調べる
    /// </summary>
    /// <returns>オブジェクトにアタッチされていたら true </returns>
    protected bool CheckInstance()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
            return true;
        }
        else if (m_Instance == this)
        {
            return true;
        }

        Destroy(this);
        return false;
    }
}
