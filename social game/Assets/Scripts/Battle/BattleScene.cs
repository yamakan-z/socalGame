using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : SceneBase
{

    private StageSelectData stageSelectData;

    [SerializeField]
    private Image[] charaimage;

    [SerializeField]
    private GameObject battlemanager;//�o�g���}�l�[�W���[�Ăяo��

    BattleManager b_manager;//�o�g���}�l�[�W���[�X�N���v�g


    [NamedArrayAttribute(new string[] { "�L������HP", "�L�����̍U����", "�L�����̖h���", "����������NULL" })]
    public int[] Player1_Status = new int[3];
    [NamedArrayAttribute(new string[] { "�L������HP", "�L�����̍U����", "�L�����̖h���", "����������NULL" })]
    public int[] Player2_Status = new int[3];
    [NamedArrayAttribute(new string[] { "�L������HP", "�L�����̍U����", "�L�����̖h���", "����������NULL" })]
    public int[] Player3_Status = new int[3];

    //�Ăяo���ꂽ�u�ԂɌĂ΂��֐�
    public override void Init()
    {
        base.Init();
        stageSelectData = (StageSelectData)ManagerAccessor.Instance.sceneManager.DeliveryData;
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//�X�N���v�g�l��
    }

    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        // �Ґ��L�����̃X�e�[�^�X					
        List<CharaStatusData> charaList = new List<CharaStatusData>();

        var api = new CharaAPI();
        yield return StartCoroutine(api.PossessionChara((response) =>
        {

            // �Ґ����					
            EditData editData = ManagerAccessor.Instance.dataManager.GetEditData();

            for (int i = 0; i < editData.editCharaList.Count; i++)
            {
                //�L�����̃��j�[�NID��ID��ϊ�
                for (int j = 0; j < response.chara_datas.Length; ++j) {
                    if (response.chara_datas[j].uniq_chara_id == editData.editCharaList[i]) {
                        var charadata = ManagerAccessor.Instance.dataManager.GetCharaData(response.chara_datas[j].chara_id);

                        Debug.Log(charadata.name);
                        Debug.Log(charadata.rank);
                       // Debug.Log(charaimage[i]);

                        charaimage[i].sprite = charadata.stand_sprite;//�����ŉ摜��I�������L�����N�^�[�̗����G�ɂ���
                    }
                }
                


            }



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

    public void BattleButton()
    {
        Debug.Log("�G�Ƀ_���[�W");
        

    }

}
