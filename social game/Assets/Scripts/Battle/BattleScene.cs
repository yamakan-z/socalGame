using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : SceneBase
{

    private StageSelectData stageSelectData;

    //�Ăяo���ꂽ�u�ԂɌĂ΂��֐�
   public override void Init()
    {
        base.Init();
        stageSelectData = (StageSelectData)ManagerAccessor.Instance.sceneManager.DeliveryData;
    }

    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        // �Ґ��L�����̃X�e�[�^�X					
        List<CharaStatusData> charaList = new List<CharaStatusData>();

        // �Ґ����					
        EditData editData = ManagerAccessor.Instance.dataManager.GetEditData();

        var api = new CharaAPI();
        yield return StartCoroutine(api.PossessionChara((response) =>
        {
            // ���L���Ă���L�����̐��������[�v					
            foreach (var chara in response.chara_datas)
            {
                // �Ґ�����Ă��邩�`�F�b�N					
                if (editData.editCharaList.Contains(chara.uniq_chara_id))
                {
                    // �Ґ�����Ă�����̂͒ǉ�					
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
        if (GUI.Button(new Rect(100, 100, 80, 80), "����"))
        {
            //���������Ƃ��̏���

            //�X�e�[�W�N���A�����擾
            var stageClearData = ManagerAccessor.Instance.dataManager.GetStageClearData();

            if(!stageClearData.stageClearList.Contains(stageSelectData.selectStageId))
            {
                //���߂Ă��̃X�e�[�W���N���A����
                stageClearData.stageClearList.Add(stageSelectData.selectStageId);
                ManagerAccessor.Instance.dataManager.SaveStageClearData(stageClearData);
            }

            //��V�̐ݒ�
            int amount = 1000;
            var api = new UserAPI();
            StartCoroutine(api.AddMoney(amount, (response) =>
                 {
                     ManagerAccessor.Instance.headerManager.SetMoneyNum(response.money);
                     ManagerAccessor.Instance.dataManager.Money = response.money;
                     ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.StageSelectScene);
                 }));
        }

        if (GUI.Button(new Rect(100, 200, 80, 80), "�s�k"))
        {
            //�s�k�����Ƃ��̏���
        }
    }

}
