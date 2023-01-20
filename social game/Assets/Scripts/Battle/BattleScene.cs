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

    public int total_atk;//���U����

    public GameObject[] CharaImage;//�L�����̉摜�f�[�^�Ȃǂ��������I�u�W�F�N�g

    int count = 0;//�X�e�[�^�X�ۑ��p�̃J�E���^�[

    //Slider������
    public Slider bosshp_bar;

    public Slider[] charahp_bar = new Slider[3];

    [SerializeField]
    private int max_BossHP;//�{�X�̍ő�HP�iHP�o�[�p�j

    [SerializeField]
    private int[] max_CharaHP = new int[3];//�L�����̍ő�HP�iHP�o�[�p�j

    [SerializeField]
    private float boss_atktime;//�{�X�̍U���Ԋu


    //�Ăяo���ꂽ�u�ԂɌĂ΂��֐�
    public override void Init()
    {
       
        base.Init();

        B_Atk_Time();

        stageSelectData = (StageSelectData)ManagerAccessor.Instance.sceneManager.DeliveryData;
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//�X�N���v�g�l��

        //�ő�HP��ۑ�
        max_BossHP = b_manager.B_hp;
       
        bosshp_bar.value = 1;//�{�X��HP�o�[�𖞃^����
        charahp_bar[0].value = 1;//�L������HP�o�[�𖞃^����
        charahp_bar[1].value = 1;//�L������HP�o�[�𖞃^����
        charahp_bar[2].value = 1;//�L������HP�o�[�𖞃^����

    }

    private void Update()
    {
        //�ǂ����A�b�v�f�[�g�͎g���Ȃ��̂ŕʂ̕��@��T��

        boss_atktime -= Time.deltaTime;

        if(boss_atktime < 0)
        {
            // Debug.Log("�{�X�U��");
            Boss_Atk();
        }
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

                        //�����ŃX�e�[�^�X�����󂯎��
                        charaList.Add(ManagerAccessor.Instance.dataManager.GetCharaStatusData(response.chara_datas[j].chara_id, response.chara_datas[j].level));
                    }
                }
                


            }

            //�����Ŏ����Ă����X�e�[�^�X���e�ϐ��ɓ����
            foreach (var chara in charaList)
            {
               
                if(count==0)
                {
                    //Debug.Log("HP" + chara.hp);
                    //Debug.Log("ATK" + chara.atk);
                    //Debug.Log("DEF" + chara.def);

                    Player1_Status[0] = chara.hp;
                    Player1_Status[1] = chara.atk;
                    Player1_Status[2] = chara.def;
                    max_CharaHP[0] = Player1_Status[0];//�ő�HP�ۑ�
                }

                else if (count == 1)
                {
                    //Debug.Log("P2HP" + chara.hp);
                    //Debug.Log("P2ATK" + chara.atk);
                    //Debug.Log("P2DEF" + chara.def);

                    Player2_Status[0] = chara.hp;
                    Player2_Status[1] = chara.atk;
                    Player2_Status[2] = chara.def;
                    max_CharaHP[1] = Player2_Status[0];//�ő�HP�ۑ�
                }


                else if (count == 2)
                {
                    //Debug.Log("P3HP" + chara.hp);
                    //Debug.Log("P3ATK" + chara.atk);
                    //Debug.Log("P3DEF" + chara.def);

                    Player3_Status[0] = chara.hp;
                    Player3_Status[1] = chara.atk;
                    Player3_Status[2] = chara.def;
                    max_CharaHP[2] = Player3_Status[0];//�ő�HP�ۑ�
                }

                count++;

            }

            //�U���͂����v����
            total_atk = Player1_Status[1] + Player2_Status[1] + Player3_Status[1];


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
            ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.StageSelectScene);
        }
    }

    public void B_Atk_Time()
    {
        //3�`5�b�̊ԂŃ{�X���U��
        boss_atktime = Random.Range(3.0f, 6.0f);
    }

    public void Boss_Atk()//�{�X�U��
    {
        //�����ɃL�����̃_���[�W����
        int rand_atk = Random.Range(1, 4);

        if(rand_atk==1)
        {
            //�_���[�W�v�Z���i�G�̍U����-�i�L�����̖h���/2�j�j
            if (b_manager.B_atk - (Player1_Status[2] / 2) < 0)
            {
                Player1_Status[0] -= 1;//�Œ�_���[�W�ۏ�

                charahp_bar[0].value = (float)Player1_Status[0] / (float)max_CharaHP[0];//HP�o�[�ɔ��f
            }
            else
            {
                Player1_Status[0] -= b_manager.B_atk - (Player1_Status[2] / 2);

                charahp_bar[0].value = (float)Player1_Status[0] / (float)max_CharaHP[0];//HP�o�[�ɔ��f
            }

            if(Player1_Status[0] <= 0)
            {
                Debug.Log("��");

                Destroy(CharaImage[0]);
            }

        }

        else if (rand_atk == 2)
        {
            //�_���[�W�v�Z���i�G�̍U����-�i�L�����̖h���/2�j�j
            if (b_manager.B_atk - (Player2_Status[2] / 2) < 0)
            {
                Player2_Status[0] -= 1;//�Œ�_���[�W�ۏ�

                charahp_bar[1].value = (float)Player2_Status[0] / (float)max_CharaHP[1];//HP�o�[�ɔ��f
            }
            else
            {
                Player2_Status[0] -= b_manager.B_atk - (Player2_Status[2] / 2);

                charahp_bar[1].value = (float)Player2_Status[0] / (float)max_CharaHP[1];//HP�o�[�ɔ��f
            }

            if (Player2_Status[0] <= 0)
            {
                Debug.Log("2��");

                Destroy(CharaImage[1]);
            }
        }

        else if (rand_atk == 3)
        {
            //�_���[�W�v�Z���i�G�̍U����-�i�L�����̖h���/2�j�j
            if (b_manager.B_atk - (Player3_Status[2] / 2) < 0)
            {
                Player3_Status[0] -= 1;//�Œ�_���[�W�ۏ�

                charahp_bar[2].value = (float)Player3_Status[0] / (float)max_CharaHP[2];//HP�o�[�ɔ��f
            }
            else
            {
                Player3_Status[0] -= b_manager.B_atk - (Player3_Status[2] / 2);

                charahp_bar[2].value = (float)Player3_Status[0] / (float)max_CharaHP[2];//HP�o�[�ɔ��f
            }

            if (Player3_Status[0] <= 0)
            {
                Debug.Log("3��");

                Destroy(CharaImage[2]);
            }

        }

        B_Atk_Time();//�U���Ԋu�J�E���g�Đݒ�

    }

    public void BattleButton()//�U���{�^��
    {
        Debug.Log("�G�Ƀ_���[�W");

        b_manager.B_hp -= total_atk;

        boss_atktime--;

        //�{�X��HP�o�[�ɔ��f
        bosshp_bar.value = (float)b_manager.B_hp / (float)max_BossHP;

    }

}
