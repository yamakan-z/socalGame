using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScene : SceneBase
{
    [SerializeField]
    private GameObject contentObj; //スクロールビューの参照

    [SerializeField]
    private GameObject stageCellPrefab;//生成するプレハブデータ


    //取得したステージのボスのステータスを入れる
    public int boss_hp;

    public int boss_atk;

    public int boss_def;

    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        //全ステージデータ
        var stageData = ManagerAccessor.Instance.dataManager.GetAllStageData();

        //クリアしたステージデータ
        var stageClearData = ManagerAccessor.Instance.dataManager.GetStageClearData();

        foreach(var stage in stageData)
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
                        //解放条件を満たしている
                         SelectStage(stage.stage_id);

                         Debug.Log("bhp"+stage.boss_hp);
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