
//============================================================
// @file ByteFlags
// @brief ビットフラグ
// @autor ochi takuya
//============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ビットフラグ
/// </summary>
public class ByteFlags
{
    private byte _flags;
    private Dictionary<string, byte> _registration;

    /// <summary>
    /// フラグステータス
    /// </summary>
    public enum FlagState
    {
        FALSE = 0,
        TRUE = 1,
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ByteFlags()
    {
        _registration = new Dictionary<string, byte>();
    }

    /// <summary>
    /// フラグを登録
    /// </summary>
    /// <param name="flagNames">フラグ名</param>
    public void RecordFlags(params string[] flagNames)
    {
        foreach (string flagName in flagNames)
        {
            _registration.Add(flagName, (byte)(1 << _registration.Count));
        }
    }

    /// <summary>
    /// 引数指定のフラグの状態を取得
    /// </summary>
    /// <param name="flagNames">フラグ名</param>
    /// <returns></returns>
    public bool GetFlags(string flagName)
    {
        // Keyが存在しない
        if (! _registration.ContainsKey(flagName))
        {
            return false;
        }

        return ((_flags & _registration[flagName]) == _registration[flagName]);
    }

    /// <summary>
    /// 引数指定のフラグをfalseにする
    /// </summary>
    /// <param name="flagNames">フラグ名</param>
    public void DelFlags(string flagName)
    {
        _flags &= (byte)~_registration[flagName];
    }

    /// <summary>
    /// 引数指定のフラグをtrueにする
    /// </summary>
    /// <param name="flagNames">フラグ名</param>
    public void AddFlags(string flagName)
    {
        _flags |= _registration[flagName];
    }

    /// <summary>
    /// 引数指定のフラグを任意の状態に変更する
    /// </summary>
    /// <param name="flagNames">フラグ名</param>
    /// <param name="flagState"></param> (int) 1 or 0 [true:1/false:0]
    public void SetFlags(string flagName, int flagState)
    {
        switch (flagState)
        {
            case (int)FlagState.FALSE:
                DelFlags(flagName);
                break;

            case (int)FlagState.TRUE:
                AddFlags(flagName);
                break;
        }
    }

    /// <summary>
    /// 引数指定のフラグを任意の状態に変更する
    /// </summary>
    /// <param name="flagNames">フラグ名</param>
    /// <param name="flagState"></param> (bool) true or false
    public void SetFlags(string flagName, bool flagState)
    {
        if (flagState)
        {
            AddFlags(flagName);
        }
        else
        {
            DelFlags(flagName);
        }
    }

    /// <summary>
    /// 引数指定のフラグを任意の状態に変更する
    /// </summary>
    /// <param name="flagNames">フラグ名</param>
    /// <param name="flagState"></param> FlagState.TRUE or FlagState.FALSE
    public void SetFlags(string flagName, FlagState flagState)
    {
        switch (flagState)
        {
            case FlagState.FALSE:
                DelFlags(flagName);
                break;

            case FlagState.TRUE:
                AddFlags(flagName);
                break;
        }
    }

    /// <summary>
    /// 引数指定のフラグの状態を切り替える
    /// </summary>
    /// <param name="flagNames">フラグ名</param>
    public void ChangeFlags(string flagName)
    {
        if (GetFlags(flagName))
        {
            DelFlags(flagName);
        }
        else
        {
            AddFlags(flagName);
        }
    }

    /// <summary>
    /// 引数以外のフラグを削除
    /// 引数が無しですべて削除
    /// </summary>
    /// <param name="flagNames">フラグ名</param>
    public void DelEverythingElse(string flagName = "")
    {
        foreach (var key in _registration.Keys)
        {
            if (flagName != "" && flagName == key)
            {
                continue;
            }
            DelFlags(key);
        }
    }
}
