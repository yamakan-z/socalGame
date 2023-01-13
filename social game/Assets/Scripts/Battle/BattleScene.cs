using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : SceneBase
{

    private StageSelectData stageSelectData;

    [SerializeField]
    private Image[] charaimage;

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

        var api = new CharaAPI();
        yield return StartCoroutine(api.PossessionChara((response) =>
        {

            // 編成情報					
            EditData editData = ManagerAccessor.Instance.dataManager.GetEditData();

            //全ステージデータ
            var stageData = ManagerAccessor.Instance.dataManager.GetAllStageData();

            //foreach(var stage in stageData)
            //{
            //    Debug.Log("bhp" + stage.boss_hp);
            //}

            for (int i = 0; i < editData.editCharaList.Count; i++)
            {
                //キャラのユニークIDとIDを変換
                for (int j = 0; j < response.chara_datas.Length; ++j) {
                    if (response.chara_datas[j].uniq_chara_id == editData.editCharaList[i]) {
                        var charadata = ManagerAccessor.Instance.dataManager.GetCharaData(response.chara_datas[j].chara_id);

                        Debug.Log(charadata.name);
                        Debug.Log(charadata.rank);
                       // Debug.Log(charaimage[i]);

                        charaimage[i].sprite = charadata.stand_sprite;//ここで画像を選択したキャラクターの立ち絵にする
                    }
                }
                


            }



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
                Debug.Log(chara.hp);
                //chara.hp -= 1;

               // Debug.Log(chara.hp);
                // Debug.Log(chara.atk);
                // Debug.Log(chara.def);
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
