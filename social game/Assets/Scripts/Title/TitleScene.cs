using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TitleScene : SceneBase
{

    [SerializeField]
    private Text uidText;//UID表示用のテキスト

    [SerializeField] private GameObject B_manager;//BGMマネージャー呼び出し

    BGMManager bgmmanager;//BGMスクリプト


    override public void Init()
    {
        base.Init();

        B_manager = GameObject.Find("BGMManager");
        bgmmanager = B_manager.GetComponent<BGMManager>();//スクリプト獲得

        string uid = ManagerAccessor.Instance.dataManager.GetUID();
        if(string.IsNullOrEmpty(uid))
        {
            //UIDがない場合
            uidText.text = "0";
        }
        else
        {
            //UIDがある場合
            uidText.text = uid;
        }
    }

    public void OnTapDeleteUIDButton()
    {

        Debug.Log("削除");
        //UIDを削除
        ManagerAccessor.Instance.dataManager.DeleteUID();

        //表示も戻す
        uidText.text = "0";

        //他にも初期化するものを入れる
        ManagerAccessor.Instance.dataManager.DeleteEditData();
        ManagerAccessor.Instance.dataManager.DeleteStageClearData();

    }



    public void OnTapButton()
    {
        //UIDがあるかチェック
        string uid = ManagerAccessor.Instance.dataManager.GetUID();
        if(string.IsNullOrEmpty(uid))
        {
            //UIDがない場合
            var api = new LoginAPI();
            ManagerAccessor.Instance.screenManager.TapGuard(true);
            ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
            StartCoroutine(api.GetUID((response)=> 
            {

                ManagerAccessor.Instance.screenManager.TapGuard(false);
                ManagerAccessor.Instance.screenManager.LoadingAnimation(false);

                //通信が成功した場合
                if (response.status.Contains("NG"))
                {
                    //エラー
                    Debug.LogError(response.error);
                    return;
                }

                //正常な場合

                //UIDを保存
                ManagerAccessor.Instance.dataManager.SaveUID(response.uid);

                //ログイン画面へ
                LoginProcess();

            }));
        }
        else
        {
            //UIDがある場合
            LoginProcess();
        }
    }

    /// <summary>
    /// ログインの処理
    /// ログインを行いホームシーンに遷移
    /// </summary>
    private void LoginProcess()
    {
        var api = new LoginAPI();
        ManagerAccessor.Instance.screenManager.TapGuard(true);
        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
        StartCoroutine(api.Login((response)=>
        {

            ManagerAccessor.Instance.screenManager.TapGuard(false);
            ManagerAccessor.Instance.screenManager.LoadingAnimation(false);
            bgmmanager.BGM_Start();//ここでBGMをシーンBGMに

            //通信が成功した場合
            if (response.status.Contains("NG"))
            {
                //エラー
                Debug.LogError(response.error);
                return;
            }

            ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.HomeScene);



        }));
    }

}

