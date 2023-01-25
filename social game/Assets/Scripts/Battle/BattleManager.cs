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

    public bool updatastart;//アップデートの処理を動かす

    public bool timestart;//攻撃タイマー起動

    public GameObject BattleScene;

    BattleScene battlescene;

    [SerializeField]
    private GameObject AtkAni;

    private  AtkAni atkani;

    public bool get_c;

    public bool judge;//一回だけ処理を通す(勝ち・負け判定を同時に行わせないため）

    public bool boss_death;
    
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

            if(timestart && !boss_death)
            boss_atktime -= Time.deltaTime;

            //攻撃時間が0になったら攻撃
            if(boss_atktime<=0)
            battlescene.Boss_Atk();

            if(B_hp<=0 && judge)
            {
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

        // 3秒間待つ
        yield return new WaitForSeconds(4.5f);

        battlescene.Win();//ボスHP0で勝利
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
}
