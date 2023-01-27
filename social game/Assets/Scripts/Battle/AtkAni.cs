using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkAni : MonoBehaviour
{

    public BattleScene battlescene;

    public GameObject[] DamegeImage;

    public GameObject DeathAnim;//ボス撃破アニメーション

    public int Ani_num;//アニメーションを流すキャラ番号

    private bool one;//一回だけ処理を通す

    private bool se_one;//一回だけSEを出す

    [SerializeField]
    private GameObject battlemanager;//バトルマネージャー呼び出し

    BattleManager b_manager;//バトルマネージャースクリプト


    public AudioSource se_audio;//AudioSource型の変数aを宣言 使用するAudioSourceコンポーネントをアタッチ必要

    public AudioSource start_se;//登場ボイスを流す

    //汎用SE
    [NamedArrayAttribute(new string[] { "攻撃SE","攻撃ボタンSE",  "大技SE","ここから先はNULL" })]
    public AudioClip[] SE;

    //各ボスのSE
    [NamedArrayAttribute(new string[] { "登場ボイス", "攻撃ボイス", "大技ボイス", "討伐ボイス","ここから先はNULL" })]
    public AudioClip[] BossSE1;

    public AudioClip[] UseBossSE;//このステージで使用するボスSE

    public bool StartBossSE_End;//登場ボイス終了フラグ

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//スクリプト獲得

        Set_BossSE();//ボスのSE設定

        if(!StartBossSE_End)
        {
            start_se.Play();//ボスボイス：登場
            StartBossSE_End = true;
        }

       

        se_one = true;
        one = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("おわり"+start_se.isPlaying);

        //登場ボイスが終わっていれば処理開始
        if (!start_se.isPlaying && StartBossSE_End && se_one)
        {
            Debug.Log("はじめ");
            b_manager.timestart = true;
            se_one = false;
        }

        if (!StartBossSE_End)
        {
            start_se.Play();//ボスボイス：攻撃
            StartBossSE_End = true;
        }

        if (Ani_num==1 && one)
        {
            one = false;

            DamegeImage[0].SetActive(true);//攻撃アニメーションを表示

            se_audio.PlayOneShot(UseBossSE[1]);//ボスボイス：攻撃

            se_audio.PlayOneShot(SE[0]);//ボス攻撃SE

            Invoke(nameof(Ani_Reset), 1.0f);//1秒後にアニメーションをリセット

        }
        else if(Ani_num == 2 && one)
        {
            one = false;

            DamegeImage[1].SetActive(true);

            se_audio.PlayOneShot(UseBossSE[1]);//ボスボイス：攻撃

            se_audio.PlayOneShot(SE[0]);//ボス攻撃SE

            Invoke(nameof(Ani_Reset), 1.0f);//1秒後にアニメーションをリセット
        }
        else if (Ani_num == 3 && one)
        {
            one = false;

            DamegeImage[2].SetActive(true);

            se_audio.PlayOneShot(UseBossSE[1]);//ボスボイス：攻撃


            se_audio.PlayOneShot(SE[0]);//ボス攻撃SE

            Invoke(nameof(Ani_Reset), 1.0f);//1秒後にアニメーションをリセット
        }


    }

    public void Set_BossSE()
    {
        //ステージ毎にボスのSEを設定する
        if (b_manager.Stage_Id == 1)
        {
            UseBossSE = BossSE1;
        }
        //else if (b_manager.Stage_Id == 2)
        //{
        //    UseBossSE = BossSE2;
        //}
        //else if (b_manager.Stage_Id == 3)
        //{
        //    UseBossSE = BossSE3;
        //}
        //else if (b_manager.Stage_Id == 4)
        //{
        //    UseBossSE = BossSE4;
        //}
        //else if (b_manager.Stage_Id == 5)
        //{
        //    UseBossSE = BossSE5;
        //}
    }

    public void BossDead()
    {
        b_manager.boss_death = true;//ボス死亡フラグON

        DeathAnim.SetActive(true);//アニメーション起動

        //ボスが死ぬとき
        se_audio.PlayOneShot(UseBossSE[3]);//ボスボイス：死亡
        // コルーチンの起動
        StartCoroutine(b_manager.WinAni());
    }

    //アニメーションのリセット
    public void Ani_Reset()
    {
        one = true;
        Ani_num = 0;
        DamegeImage[0].SetActive(false);
        DamegeImage[1].SetActive(false);
        DamegeImage[2].SetActive(false);
    }
}
