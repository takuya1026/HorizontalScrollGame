using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスターデータ管理クラス
/// </summary>
public class MasterDataManager : SingletonMonoBehaviour<MasterDataManager>
{
    public EnemyMasterDataTable EnemyMasterDataTable { get; private set; } = new EnemyMasterDataTable();
}
