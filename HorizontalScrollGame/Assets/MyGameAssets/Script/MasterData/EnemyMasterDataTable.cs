using System.Collections.Generic;
using System.Linq;

/// <summary>
/// エネミーマスターのデータテーブル
/// </summary>
public class EnemyMasterDataTable : MasterDataTableBase<EnemyMaster>
{
    /// <summary>
    /// パス
    /// </summary>
    protected override string m_DataFilePath => $"../HorizontalScrollGame.Server/Master/{nameof(EnemyMaster)}.json ";

    /// <summary>
    /// データテーブル（ID）
    /// </summary>
    private Dictionary<int, EnemyMaster> m_enemyMasterById;

    /// <summary>
    /// データテーブル（敵の名前）
    /// </summary>
    private Dictionary<string, EnemyMaster> m_enemyMasterByName;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public EnemyMasterDataTable() : base()
    {
        m_enemyMasterById = m_masterDatas.ToDictionary(elem => elem.id);
        m_enemyMasterByName = m_masterDatas.ToDictionary(elem => elem.name);
    }

    /// <summary>
    /// IDで検索
    /// </summary>
    public EnemyMaster FindById(int id) => m_enemyMasterById[id];

    /// <summary>
    /// 敵の名前で検索
    /// </summary>
    public EnemyMaster FindByName(string name) => m_enemyMasterByName[name];
}
