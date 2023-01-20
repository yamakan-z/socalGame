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
    private GameObject battlemanager;//バトルマネージャー呼び出し

    BattleManager b_manager;//バトルマネージャースクリプト


    [NamedArrayAttribute(new string[] { "キャラのHP", "キャラの攻撃力", "キャラの防御力", "ここから先はNULL" })]
    public int[] Player1_Status = new int[3];
    [NamedArrayAttribute(new string[] { "キャラのHP", "キャラの攻撃力", "キャラの防御力", "ここから先はNULL" })]
    public int[] Player2_Status = new int[3];
    [NamedArrayAttribute(new string[] { "キャラのHP", "キャラの攻撃力", "キャラの防御力", "ここから先はNULL" })]
    public int[] Player3_Status = new int[3];

    public int total_atk;//総攻撃力

    public GameObject[] CharaImage;//キャラの画像データなどが入ったオブジェクト

    int count = 0;//ステータス保存用のカウンター

    //Sliderを入れる
    public Slider bosshp_bar;

    public Slider[] charahp_bar = new Slider[3];

    [SerializeField]
    private int max_BossHP;//ボスの最大HP（HPバー用）

    [SerializeField]
    private int[] max_CharaHP = new int[3];//キャラの最大HP（HPバー用）

    [SerializeField]
    private float boss_atktime;//ボスの攻撃間隔


    //呼び出された瞬間に呼ばれる関数
    public override void Init()
    {
       
        base.Init();

        B_Atk_Time();

        stageSelectData = (StageSelectData)ManagerAccessor.Instance.sceneManager.DeliveryData;
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//スクリプト獲得

        //最大HPを保存
        max_BossHP = b_manager.B_hp;
       
        bosshp_bar.value = 1;//ボスのHPバーを満タンに
        charahp_bar[0].value = 1;//キャラのHPバーを満タンに
        charahp_bar[1].value = 1;//キャラのHPバーを満タンに
        charahp_bar[2].value = 1;//キャラのHPバーを満タンに

    }

    private void Update()
    {
        //どうやらアップデートは使えないので別の方法を探す

        boss_atktime -= Time.deltaTime;

        if(boss_atktime < 0)
        {
            // Debug.Log("ボス攻撃");
            Boss_Atk();
        }
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

                        //ここでステータスやらを受け取る
                        charaList.Add(ManagerAccessor.Instance.dataManager.GetCharaStatusData(response.chara_datas[j].chara_id, response.chara_datas[j].level));
                    }
                }
                


            }

            //ここで持ってきたステータスを各変数に入れる
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
                    max_CharaHP[0] = Player1_Status[0];//最大HP保存
                }

                else if (count == 1)
                {
                    //Debug.Log("P2HP" + chara.hp);
                    //Debug.Log("P2ATK" + chara.atk);
                    //Debug.Log("P2DEF" + chara.def);

                    Player2_Status[0] = chara.hp;
                    Player2_Status[1] = chara.atk;
                    Player2_Status[2] = chara.def;
                    max_CharaHP[1] = Player2_Status[0];//最大HP保存
                }


                else if (count == 2)
                {
                    //Debug.Log("P3HP" + chara.hp);
                    //Debug.Log("P3ATK" + chara.atk);
                    //Debug.Log("P3DEF" + chara.def);

                    Player3_Status[0] = chara.hp;
                    Player3_Status[1] = chara.atk;
                    Player3_Status[2] = chara.def;
                    max_CharaHP[2] = Player3_Status[0];//最大HP保存
                }

                count++;

            }

            //攻撃力を合計する
            total_atk = Player1_Status[1] + Player2_Status[1] + Player3_Status[1];


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
            ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.StageSelectScene);
        }
    }

    public void B_Atk_Time()
    {
        //3～5秒の間でボスが攻撃
        boss_atktime = Random.Range(3.0f, 6.0f);
    }

    public void Boss_Atk()//ボス攻撃
    {
        //ここにキャラのダメージ処理
        int rand_atk = Random.Range(1, 4);

        if(rand_atk==1)
        {
            //ダメージ計算式（敵の攻撃力-（キャラの防御力/2））
            if (b_manager.B_atk - (Player1_Status[2] / 2) < 0)
            {
                Player1_Status[0] -= 1;//最低ダメージ保障

                charahp_bar[0].value = (float)Player1_Status[0] / (float)max_CharaHP[0];//HPバーに反映
            }
            else
            {
                Player1_Status[0] -= b_manager.B_atk - (Player1_Status[2] / 2);

                charahp_bar[0].value = (float)Player1_Status[0] / (float)max_CharaHP[0];//HPバーに反映
            }

            if(Player1_Status[0] <= 0)
            {
                Debug.Log("死");

                Destroy(CharaImage[0]);
            }

        }

        else if (rand_atk == 2)
        {
            //ダメージ計算式（敵の攻撃力-（キャラの防御力/2））
            if (b_manager.B_atk - (Player2_Status[2] / 2) < 0)
            {
                Player2_Status[0] -= 1;//最低ダメージ保障

                charahp_bar[1].value = (float)Player2_Status[0] / (float)max_CharaHP[1];//HPバーに反映
            }
            else
            {
                Player2_Status[0] -= b_manager.B_atk - (Player2_Status[2] / 2);

                charahp_bar[1].value = (float)Player2_Status[0] / (float)max_CharaHP[1];//HPバーに反映
            }

            if (Player2_Status[0] <= 0)
            {
                Debug.Log("2死");

                Destroy(CharaImage[1]);
            }
        }

        else if (rand_atk == 3)
        {
            //ダメージ計算式（敵の攻撃力-（キャラの防御力/2））
            if (b_manager.B_atk - (Player3_Status[2] / 2) < 0)
            {
                Player3_Status[0] -= 1;//最低ダメージ保障

                charahp_bar[2].value = (float)Player3_Status[0] / (float)max_CharaHP[2];//HPバーに反映
            }
            else
            {
                Player3_Status[0] -= b_manager.B_atk - (Player3_Status[2] / 2);

                charahp_bar[2].value = (float)Player3_Status[0] / (float)max_CharaHP[2];//HPバーに反映
            }

            if (Player3_Status[0] <= 0)
            {
                Debug.Log("3死");

                Destroy(CharaImage[2]);
            }

        }

        B_Atk_Time();//攻撃間隔カウント再設定

    }

    public void BattleButton()//攻撃ボタン
    {
        Debug.Log("敵にダメージ");

        b_manager.B_hp -= total_atk;

        boss_atktime--;

        //ボスのHPバーに反映
        bosshp_bar.value = (float)b_manager.B_hp / (float)max_BossHP;

    }

}
