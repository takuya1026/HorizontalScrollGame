using Newtonsoft.Json;
using System.IO;
using System.Text;

/// <summary>
/// データテーブルのベース
/// </summary>
public abstract class MasterDataTableBase<Master>
{
    /// <summary>
    /// データへのファイルパス
    /// </summary>
    abstract protected string m_DataFilePath { get; }

    /// <summary>
    /// マスターデータのリスト
    /// </summary>
    protected Master[] m_masterDatas;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public MasterDataTableBase()
    {
        string jsonText = File.ReadAllText(m_DataFilePath,Encoding.UTF8);
        m_masterDatas = JsonConvert.DeserializeObject<Master[]>(jsonText);
    }
}
