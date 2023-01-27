using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //ステージデータからボスのステータスを持ってくる
    public int B_hp;
    public int B_atk;
    public int B_def;

    public int Stage_Id;//現在のステージID取得
    public int B_Awakening;//覚醒の有無

    public float boss_atktime;//ボスの攻撃間隔

    public int start_spatk;//大技の発動開始フラグ 

    public bool updatastart;//アップデートの処理を動かす

    public bool timestart;//攻撃タイマー起動

    public GameObject BattleScene;

    BattleScene battlescene;

    [SerializeField]
    private GameObject AtkAni;

    private  AtkAni atkani;

    public bool get_c;

    public bool judge;//一回だけ処理を通す(勝ち・負け判定を同時に行わせないため）

    public bool boss_death;//ボス死亡フラグ


    public float total_time = 10.0f;//大技発動時間をここに入れる
    private int second;//テキスト反映用

    public bool s_start;//大技発動中



    // Start is called before the first frame update
    void Start()
    {
        get_c = true;
        judge = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        //バトルシーンが生成されたらタイマー開始
        if (updatastart)
        {
            //生成されたバトルシーンをここで取得する
            if(get_c)
            {
                Debug.Log("取得");
                BattleScene = GameObject.Find("BattleScene(Clone)");
                battlescene = BattleScene.GetComponent<BattleScene>();
                AtkAni = GameObject.Find("AtkAniManager");
                atkani = AtkAni.GetComponent<AtkAni>();

                

                get_c = false;
            }

            if(timestart && !boss_death && !s_start)
            boss_atktime -= Time.deltaTime;//ここで攻撃時間のカウントダウン

            //ボスのHPが一定を下回ると大技
            if(B_hp <= battlescene.max_BossHP/2 && !boss_death)
            {
             
                if(start_spatk!=1)
                {
                    SP_Atk_Time();
                }
                
                if (start_spatk == 1 && !boss_death && !s_start)
                {
                    battlescene.spatk_timer.SetActive(true);//タイマー表示
                    total_time -= Time.deltaTime;//発動カウントダウン開始
                    second = (int)total_time;
                    battlescene.timetext.text = second.ToString();//テキストに反映 

                    if (total_time <= 0)
                    {
                        s_start = true;
                        total_time = 10.0f;//タイムリセット
                        Debug.Log("いいい");
                        second = 0;
                        battlescene.spatk_timer.SetActive(false);//タイマー非表示
                        //battlescene.Sp_Atk();//必殺技発動
                    }


                }
            }

            //攻撃時間が0になったら攻撃
            if(boss_atktime<=0)
            battlescene.Boss_Atk();

            if(B_hp<=0 && judge)
            {
                battlescene.spatk_timer.SetActive(false);//タイマー非表示
                atkani.BossDead();//ボス死亡処理に移行
            }
            else if(battlescene.deathcount == 3 && judge)
            {
                // コルーチンの起動
                StartCoroutine(LoseAni());
            }


        }
       
    }

    public IEnumerator WinAni()
    {
        Debug.Log("勝ち");
        judge = false;

        yield return new WaitForSeconds(4.5f); //爆発アニメーション終了まで待つ

        battlescene.Win();//勝利処理を行う
    }

    private IEnumerator LoseAni()
    {
        judge = false;

        // 3秒間待つ
        yield return new WaitForSeconds(1.5f);

        battlescene.Lose();//キャラ全滅で敗北
    }

    public void B_Atk_Time()
    {
        //3～5秒の間でボスが攻撃
        boss_atktime = Random.Range(3.0f, 6.0f);
    }

    public void SP_Atk_Time()
    {
        start_spatk = Random.Range(0, 8);
    }
}
