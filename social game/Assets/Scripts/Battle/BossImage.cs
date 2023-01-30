using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class BossImage : MonoBehaviour
{

    // Image コンポーネントを格納する変数
    [SerializeField] private Image m_Image;

    // スプライトオブジェクトを格納する配列
    public Sprite[] m_Sprite;

    [SerializeField]
    private GameObject battlemanager;//バトルマネージャー呼び出し

    BattleManager b_manager;//バトルマネージャースクリプト

    [SerializeField] private GameObject Stage1Button;//もうめんどいのでここでボタンの処理もする

    [SerializeField] private GameObject[] CreateButton;//生成するボタン

    [SerializeField] private GameObject[] ButtonGene;

    [SerializeField] private float changetime;//ボタン変更時間

    public int max_BossHP;//ボスの最大HP

    private bool onechange;//一回の処理につき一回ボタンチェンジ

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//スクリプト獲得

        //最大HPを保存
        max_BossHP = b_manager.B_hp;

        if (b_manager.Stage_Id==1)
        {
            m_Image.sprite = m_Sprite[0];
            Stage1Button.SetActive(true);
        }
        else if (b_manager.Stage_Id == 2)
        {
            m_Image.sprite = m_Sprite[1];
            Stage1Button.SetActive(true);
            //Instantiate(CreateButton[0], Legs[randnum].transform.position, Quaternion.identity);
        }
        else if (b_manager.Stage_Id == 3)
        {
            m_Image.sprite = m_Sprite[2];
        }
        else if (b_manager.Stage_Id == 4)
        {
            m_Image.sprite = m_Sprite[3];
        }
        else if (b_manager.Stage_Id == 5)
        {
            m_Image.sprite = m_Sprite[4];
        }

    }

    // Update is called once per frame
    void Update()
    {
        //ボスのHPが半分切った時,ボタンチェンジ
        if(b_manager.Stage_Id == 2 && b_manager.B_hp <= max_BossHP / 2)
        {
            Debug.Log("ひか");
            changetime -= Time.deltaTime;

            if(changetime<=0 && !onechange)
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

        Instantiate(CreateButton[randnum], ButtonGene[randnum].transform.position, Quaternion.identity);
    }


}


