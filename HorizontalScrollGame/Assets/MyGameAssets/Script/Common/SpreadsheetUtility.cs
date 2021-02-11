/// <summary>
/// スプレッドシート関連の処理で扱うやつ
/// </summary>
public static class SpreadsheetUtility
{
    /// <summary>
    /// カルムのインデックスからアルファベットに変換
    /// </summary>
    public static string ColumnIndexToAlphabet(int index)
    {
        // FIXME:
        // ひとまず、変換できるのはカラムがA~Zの場合のみ
        // ABとかAABとかカラムがZを超えた場合は考慮しない。
        // 流石にそこまでカラムが増えることはないだろう。

        int a = (int)("A"[0]);
        return ((char)(a + index)).ToString();
    }
}
