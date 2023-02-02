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
            changetime = 3;
            //Instantiate(CreateButton[0], Legs[randnum].transform.position, Quaternion.identity);
        }
        else if (b_manager.Stage_Id == 3)
        {
            Stage1Button.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ボスのHPが半分切った時,ボタンチェンジ
        if (b_manager.Stage_Id == 2 && b_manager.B_hp <= max_BossHP / 2)
        {
            Debug.Log("ひか");
            changetime -= Time.deltaTime;

            if (changetime <= 0 && !onechange)
            {
                Stage1Button.SetActive(false);
                onechange = true;
                ButtonChange();
            }

        }
        else if (b_manager.Stage_Id == 3 && b_manager.B_hp <= max_BossHP / 2)
        {
            Debug.Log("ひか2");

            changetime -= Time.deltaTime;

            if (changetime <= 0 && !onechange)
            {
                Stage1Button.SetActive(false);
                onechange = true;
                ButtonChange();
            }

        }
    }

    public void ButtonChange()
    {
        int randnum = Random.Range(0, 2);//0,1の数を返す

        ShaffleButton[randnum].SetActive(true);
    }

}
