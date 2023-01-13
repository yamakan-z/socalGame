using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : SceneBase
{

    private StageSelectData stageSelectData;

    //呼び出された瞬間に呼ばれる関数
   public override void Init()
    {
        base.Init();
        stageSelectData = (StageSelectData)ManagerAccessor.Instance.sceneManager.DeliveryData;
    }

    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        // 編成キャラのステータス					
        List<CharaStatusData> charaList = new List<CharaStatusData>();

        // 編成情報					
        EditData editData = ManagerAccessor.Instance.dataManager.GetEditData();

        var api = new CharaAPI();
        yield return StartCoroutine(api.PossessionChara((response) =>
        {
            // 所有しているキャラの数だけループ					
            foreach (var chara in response.chara_datas)
            {
                // 編成されているかチェック					
                if (editData.editCharaList.Contains(chara.uniq_chara_id))
                {
                    // 編成されているものは追加					
                    charaList.Add(ManagerAccessor.Instance.dataManager.GetCharaStatusData(chara.chara_id, chara.level));
                }
            }

            foreach (var chara in charaList)
            {
                Debug.Log(chara.atk);
            }
        }));
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 80, 80), "勝利"))
        {
            //勝利したときの処理

            //ステージクリア情報を取得
            var stageClearData = ManagerAccessor.Instance.dataManager.GetStageClearData();

            if(!stageClearData.stageClearList.Contains(stageSelectData.selectStageId))
            {
                //初めてそのステージをクリアした
                stageClearData.stageClearList.Add(stageSelectData.selectStageId);
                ManagerAccessor.Instance.dataManager.SaveStageClearData(stageClearData);
            }

            //報酬の設定
            int amount = 1000;
            var api = new UserAPI();
            StartCoroutine(api.AddMoney(amount, (response) =>
                 {
                     ManagerAccessor.Instance.headerManager.SetMoneyNum(response.money);
                     ManagerAccessor.Instance.dataManager.Money = response.money;
                     ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.StageSelectScene);
                 }));
        }

        if (GUI.Button(new Rect(100, 200, 80, 80), "敗北"))
        {
            //敗北したときの処理
        }
    }

}
