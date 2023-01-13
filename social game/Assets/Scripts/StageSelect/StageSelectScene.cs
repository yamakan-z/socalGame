using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScene : SceneBase
{
    [SerializeField]
    private GameObject contentObj; //�X�N���[���r���[�̎Q��

    [SerializeField]
    private GameObject stageCellPrefab;//��������v���n�u�f�[�^


    //�擾�����X�e�[�W�̃{�X�̃X�e�[�^�X������
    public int boss_hp;

    public int boss_atk;

    public int boss_def;

    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        //�S�X�e�[�W�f�[�^
        var stageData = ManagerAccessor.Instance.dataManager.GetAllStageData();

        //�N���A�����X�e�[�W�f�[�^
        var stageClearData = ManagerAccessor.Instance.dataManager.GetStageClearData();

        foreach(var stage in stageData)
        {
            GameObject obj = Instantiate(stageCellPrefab, Vector3.zero, Quaternion.identity, contentObj.transform);
            StageCell cell = obj.GetComponent<StageCell>();

            cell.SetName(stage.name);

            if (stage.unlock_stage_id == 0)
            {
                //�����X�e�[�W�͕K���J��
                cell.SetLockStage(false);
            }
            else
            {
                if (stageClearData.stageClearList.Contains(stage.unlock_stage_id))
                {
                    //�X�e�[�W���
                    cell.SetLockStage(false);
                }
                else
                {
                   // Debug.Log("������");
                    //�܂����b�N��
                    cell.SetLockStage(true);
                }

                //cell.SetLockStage(stageClearData.stageClearList.Contains(stage.unlock_stage_id))
            }



            cell.SetOnTapEvent(() =>
            {
                    //�X�e�[�W�^�b�v���ꂽ���
                    if(stage.unlock_stage_id == 0||stageClearData.stageClearList.Contains(stage.unlock_stage_id))
                    {
                        //��������𖞂����Ă���
                         SelectStage(stage.stage_id);

                         Debug.Log("bhp"+stage.boss_hp);
                    }


            });
            
        }

    }

    //�X�e�[�W���I�����ꂽ�Ƃ��ɌĂ�
    private void SelectStage(int stageId)
    {
        var stageSelectData = new StageSelectData();
        stageSelectData.selectStageId = stageId;
        ManagerAccessor.Instance.sceneManager.DeliveryData = (object)stageSelectData;

        //�o�g���V�[���ɑJ��
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.BattleScene);
    }

    public void OnTapReturnButton()
    {
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.HomeScene);
    }

}

//�X�e�[�W�N���A��Ԃ��ێ�����N���X
[System.Serializable]
public class StageClearData
{
    public List<int> stageClearList;//�N���A�������Ƃ̂���X�e�[�WID��ۑ�����
}

//�o�g���V�[���ɑ��邽�߂̃f�[�^
[System.Serializable]
public class StageSelectData
{
    public int selectStageId;//�I�������X�e�[�WID
}