using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject Stage1Button;

    [SerializeField]  private GameObject battlemanager;//バトルマネージャー呼び出し

    BattleManager b_manager;//バトルマネージャースクリプト

    [SerializeField] private GameObject[] ShaffleButton;//位置が違うボタンを用意

    [SerializeField] private float changetime;//ボタン変更時間

    //各ステージの変更時間
    [SerializeField] private float stage2;
    [SerializeField] private float stage3;
    [SerializeField] private float stage4;
    [SerializeField] private float stage5;

    private float use_time;//このステージに使う時間

    [SerializeField] private int randnum;

    private bool second;//2回目以降のボタン変更処理

   private bool onechange;//一回の処理につき一回ボタンチェンジ

    public int max_BossHP;//ボスの最大HP



    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//スクリプト獲得

        //最大HPを保存
        max_BossHP = b_manager.B_hp;

        if (b_manager.Stage_Id == 1)
        {
            Stage1Button.SetActive(true);
        }
        else if (b_manager.Stage_Id == 2)
        {
            Stage1Button.SetActive(true);
            use_time = stage2;
            changetime = stage2;
        }
        else if (b_manager.Stage_Id == 3)
        {
            Stage1Button.SetActive(true);
            use_time = stage3;
            changetime = stage3;
        }
        else if (b_manager.Stage_Id == 4)
        {
            Stage1Button.SetActive(true);
            use_time = stage4;
            changetime = stage4;
        }
        else if (b_manager.Stage_Id == 5)
        {
            Stage1Button.SetActive(true);
            use_time = stage5;
            changetime = stage5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ボスのHPが半分切った時,ボタンチェンジ
        if (b_manager.Stage_Id == 2 && b_manager.B_hp <= max_BossHP / 2)
        {
          
            changetime -= Time.deltaTime;

            if (changetime <= 0 && !onechange)
            {
                Debug.Log("ひか");
                onechange = true;

                //チェンジ回数によって処理を変える
                if(!second)
                {
                    F_ButtonChange();
                }
                else
                {
                    ButtonChange();
                }
               
            }

        }
        else if (b_manager.Stage_Id >= 3)
        {
            Debug.Log("ひか2");

            changetime -= Time.deltaTime;

            if (changetime <= 0 && !onechange)
            {
                onechange = true;

                //チェンジ回数によって処理を変える
                if (!second)
                {
                    F_ButtonChange();
                }
                else
                {
                    ButtonChange();
                }
            }
                

        }
    }

    public void F_ButtonChange()
    {
        randnum = Random.Range(0, 2);//0,1の数を返す

        ShaffleButton[randnum].SetActive(false);
        changetime = use_time;
        onechange = false;//またカウント
        second = true;//一回目の処理終了
    }

    public void ButtonChange()
    {
        //randnum = Random.Range(0, 2);//0,1の数を返す

        if(randnum == 0)//右
        {
            ShaffleButton[0].SetActive(true);
            ShaffleButton[1].SetActive(false);
            randnum = 1;
        }
        else if (randnum == 1)//左
        {
            ShaffleButton[1].SetActive(true);
            ShaffleButton[0].SetActive(false);
            randnum = 0;
        }

        changetime = use_time;
        onechange = false;//またカウント
    }
}
