using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScene : SceneBase
{
    [SerializeField]
    private GameObject contentObj; //スクロールビューの参照

    [SerializeField]
    private GameObject stageCellPrefab;//生成するプレハブデータ

    [SerializeField]
    private GameObject battlemanager;//バトルマネージャー呼び出し

    [SerializeField] private GameObject B_manager;//BGMマネージャー呼び出し

    BGMManager bgmmanager;//BGMスクリプト

    private void Start()
    {
        B_manager = GameObject.Find("BGMManager");
        bgmmanager = B_manager.GetComponent<BGMManager>();//スクリプト獲得

        battlemanager = GameObject.Find("BattleManager");
    }

    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        //全ステージデータ
        var stageData = ManagerAccessor.Instance.dataManager.GetAllStageData();

        //クリアしたステージデータ
        var stageClearData = ManagerAccessor.Instance.dataManager.GetStageClearData();

        BattleManager battleManager = battlemanager.GetComponent<BattleManager>();//スクリプト獲得


        foreach (var stage in stageData)
        {
            GameObject obj = Instantiate(stageCellPrefab, Vector3.zero, Quaternion.identity, contentObj.transform);
            StageCell cell = obj.GetComponent<StageCell>();

            cell.SetName(stage.name);

            if (stage.unlock_stage_id == 0)
            {
                //初期ステージは必ず開放
                cell.SetLockStage(false);
            }
            else
            {
                if (stageClearData.stageClearList.Contains(stage.unlock_stage_id))
                {
                    //ステージ解放
                    cell.SetLockStage(false);
                }
                else
                {
                   // Debug.Log("ぉｃｌ");
                    //まだロック中
                    cell.SetLockStage(true);
                }

                //cell.SetLockStage(stageClearData.stageClearList.Contains(stage.unlock_stage_id))
            }



            cell.SetOnTapEvent(() =>
            {
                    //ステージタップされた状態
                    if(stage.unlock_stage_id == 0||stageClearData.stageClearList.Contains(stage.unlock_stage_id))
                    {
                    
                    //ボスのステータスをバトルマネージャーに入れる
                    battleManager.B_hp  = stage.boss_hp;
                    battleManager.B_atk = stage.boss_atk;
                    battleManager.B_def = stage.boss_def;
                    //ステージIDをバトルマネージャーに入れる
                    battleManager.Stage_Id = stage.stage_id;

                    battleManager.B_Awakening = stage.boss_awakening;//覚醒状態を入れる

                    //解放条件を満たしている
                    SelectStage(stage.stage_id);

                         //Debug.Log("bhp"+stage.boss_hp);
                    }


            });
            
        }

    }

    //ステージが選択されたときに呼ぶ
    private void SelectStage(int stageId)
    {
        var stageSelectData = new StageSelectData();
        stageSelectData.selectStageId = stageId;
        ManagerAccessor.Instance.sceneManager.DeliveryData = (object)stageSelectData;

        bgmmanager.BGM_Stop();//シーンBGMを止める

        //バトルシーンに遷移
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.BattleScene);
    }

    public void OnTapReturnButton()
    {
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.HomeScene);
    }

}

//ステージクリア状態を維持するクラス
[System.Serializable]
public class StageClearData
{
    public List<int> stageClearList;//クリアしたことのあるステージIDを保存する
}

//バトルシーンに送るためのデータ
[System.Serializable]
public class StageSelectData
{
    public int selectStageId;//選択したステージID
}