using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    private SceneManager sceneManager;

    public SceneManager SceneManager
    {
        get { return sceneManager; }
        set { sceneManager = value; }
    }

    private ScreenManager screenManager;
    public ScreenManager ScreenManager
    {
        get { return screenManager; }
        set { screenManager = value; }
    }

    [SerializeField]
    private Canvas canvas;

    //シーンが生成された瞬間に呼ばれる関数
    virtual public void Init()
    {
        canvas.worldCamera = sceneManager.MainCamera;
    }

    //フィードインする前に呼ばれる関数
    virtual public IEnumerator ViewWillFadeIn()
    {
        yield return null;
    }


    //フェードインが完了した際に呼ばれる関数
    virtual public void ViewDidFadeIn()
    {

    }

    //破壊される前に呼ばれる関数
    virtual public void Deinit()
    {

    }
}
