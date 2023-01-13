using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [System.Serializable]
    public struct scenePrefab
    {
        public SceneType.Type type;
        public GameObject prefab;
    }

    [SerializeField]
    private List<scenePrefab> scenePrefabList;//シーンリスト

    [SerializeField]
    private Camera mainCamera;

    public Camera MainCamera
    {
        get { return mainCamera; }
    }


    [SerializeField]
    private ScreenManager screenManager;

    [SerializeField]
    private GameObject sceneRoot;//シーンのRootとなるオブジェクトの参照

    [SerializeField]
    private SceneType.Type firstScene;//最初に呼ばれるシーン

    private SceneBase currentScene;//現在のシーン

    private SceneType.Type nextSceneType = SceneType.Type.None;//次に遷移するシーン

    private bool isSceneChange;//現在シーン遷移中かのフラグ

    private object deliveryData; //シーン間でデータを受け渡したいときに一時的に保存するための変数

    public object DeliveryData
    {
        set { deliveryData = value;}
        get { return deliveryData; }
    }


    private void Awake()
    {
        ManagerAccessor.Instance.sceneManager = this;
        CreateScene(firstScene);
    }

    /// <summary>
    /// シーン遷移時に呼ぶ関数
    /// </summary>
    /// <param name="sceneType">遷移先のシーン</param>
    public void SceneChange(SceneType.Type sceneType)
    {
        if(isSceneChange)
        {
            //すでにシーン遷移中の場合は無視
            return;
        }
        isSceneChange = true;
        nextSceneType = sceneType;

        //画面のタップを制限
        screenManager.TapGuard(true);

        StartCoroutine("ChangeScene");
    }

    /// <summary>
    /// 実際のシーン遷移の処理
    /// </summary>
    /// <param name="sceneType"></param>
    /// 
    IEnumerator ChangeScene()
    {
        Debug.Log("StartFadeOut");
        yield return StartCoroutine(screenManager.StartFadeOut());

        //現在のシーンを破棄
        Debug.Log("DeleteScene");
        DeleteScene();

        //新しいシーンを作成
        Debug.Log("CreateScene");
        CreateScene(nextSceneType);

        //ViewWillFadeInの実行
        Debug.Log("ViewWillFadeIn");
        yield return StartCoroutine(currentScene.ViewWillFadeIn());

        //viewDidFadeInの実行
        Debug.Log("ViewDidFadeIn");
        currentScene.ViewDidFadeIn();

        nextSceneType = SceneType.Type.None;

        screenManager.TapGuard(false);

        isSceneChange = false;
    }

    //シーンを生成する関数
    void CreateScene(SceneType.Type sceneType)
    {
        //生成するシーンのprefabを検索
        GameObject prefab = scenePrefabList.Find(scenePrefab => scenePrefab.type == sceneType).prefab;

        //sceneRootにシーンを生成する
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, sceneRoot.transform);

        currentScene = obj.GetComponent<SceneBase>();
        currentScene.SceneManager = this;
        currentScene.ScreenManager = screenManager;
        currentScene.Init();
    }


    //現在のシーンを破棄する関数
    void DeleteScene()
    {
        currentScene.Deinit();
        Destroy(currentScene.gameObject);
        currentScene = null;
    }

}
