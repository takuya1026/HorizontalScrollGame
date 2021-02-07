using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

/// <summary>
/// シーン管理クラス（シングルトン）
/// </summary>
public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    /// <summary>
    /// シーンヒストリー
    /// </summary>
    private Stack<string> m_sceneHistory = new Stack<string>();

    // FIXME : 
    // LoadSceneでシーンをロードしたときにキャッシュしておき、
    // LoadPrevSceneで戻るときにキャッシュしておいたシーンをアクティブにするような流れのほうが処理負荷を抑えられるため理想ではある。
    // が、ひとまずキャッシュは行わずに毎回ロードする流れで対応。
    // 今後、シーンのロードが重たくなってきたら考える。

    /// <summary>
    /// 開始
    /// </summary>
    private void Start()
    {
        // 最初のシーンをヒストリーに登録
        string sceneName = UnitySceneManager.GetActiveScene().path;
        m_sceneHistory.Push(sceneName);
    }

    /// <summary>
    /// シーンをロードする
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, (component) =>
        {
            component.Initialize();
        }));
    }

    /// <summary>
    /// シーンをロードする（パラメータ付き）
    /// </summary>
    /// <typeparam name="Parameter">パラメータの型</typeparam>
    /// <param name="sceneName">シーン名</param>
    /// <param name="parameter">シーンに渡すパラメータ</param>
    public void LoadScene<Parameter>(string sceneName,Parameter parameter)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, (component) =>
        {
            ISceneParameter<Parameter> param = (ISceneParameter<Parameter>)component;
            component.Initialize();
            param?.Initialize(parameter);
        }));
    }

    /// <summary>
    /// 前回のシーンをロード
    /// </summary>
    public void LoadPrevScene()
    {
        // 前回のシーンを取得
        m_sceneHistory.Pop();
        string prevSceneName = m_sceneHistory.Pop();
        
        // ロード
        LoadScene(prevSceneName);
    }

    /// <summary>
    /// 前回のシーンをロード（パラメータ付き）
    /// </summary>
    public void LoadPrevScene<Parameter>(Parameter parameter)
    {
        // 前回のシーンを取得
        m_sceneHistory.Pop();
        string prevSceneName = m_sceneHistory.Pop();
        
        // ロード
        LoadScene<Parameter>(prevSceneName,parameter);
    }

    /// <summary>
    /// シーンをロードする（コルーチン）
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    /// <param name="onComplete">ロード完了時のコールバック</param>
    private IEnumerator LoadSceneCoroutine(string sceneName, Action<SceneComponent> onComplete)
    {
        // シーンロード
        AsyncOperation loadTask = UnitySceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // ロード完了まで待機
        while (!loadTask.isDone) 
        {
            yield return null;
        }

        // ロード完了

        // シーンのルートオブジェクトを取得
        Scene activeScene = UnitySceneManager.GetActiveScene();
        GameObject[] rootObjects = activeScene.GetRootGameObjects();
        
        // ルートオブジェクトからSceneComponentを探して、ロード完了を通知
        foreach (var root in rootObjects)
        {
            SceneComponent component = root.GetComponent<SceneComponent>();

            if (component != null)
            {
                // 通知
                m_sceneHistory.Push(sceneName);
                onComplete?.Invoke(component);
                break;
            }
        }
    }
}
