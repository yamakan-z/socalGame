using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各マネージャークラスへのアクセスを簡易化するクラス
/// </summary>
public class ManagerAccessor
{
    private static ManagerAccessor instance = null;
    public static ManagerAccessor Instance
    {
        get
        {
            if(instance==null)
            {
                instance = new ManagerAccessor();
            }
            return instance;
        }
    }

    //マネージャーの参照
    public SceneManager sceneManager;
    public ScreenManager screenManager;
    public DataManager dataManager;
    public HeaderManager headerManager;
}
