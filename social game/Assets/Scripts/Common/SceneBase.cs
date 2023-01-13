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

    //�V�[�����������ꂽ�u�ԂɌĂ΂��֐�
    virtual public void Init()
    {
        canvas.worldCamera = sceneManager.MainCamera;
    }

    //�t�B�[�h�C������O�ɌĂ΂��֐�
    virtual public IEnumerator ViewWillFadeIn()
    {
        yield return null;
    }


    //�t�F�[�h�C�������������ۂɌĂ΂��֐�
    virtual public void ViewDidFadeIn()
    {

    }

    //�j�󂳂��O�ɌĂ΂��֐�
    virtual public void Deinit()
    {

    }
}
