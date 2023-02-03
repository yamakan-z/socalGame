using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    // Image コンポーネントを格納する変数
    [SerializeField] private Image m_Image;

    // スプライトオブジェクトを格納する配列
    public Sprite[] m_Sprite;

    [SerializeField]
    private GameObject battlemanager;//バトルマネージャー呼び出し

    BattleManager b_manager;//バトルマネージャースクリプト

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//スクリプト獲得

        //ここで各ステージの背景チェンジ
        if (b_manager.Stage_Id == 1)
        {
            m_Image.sprite = m_Sprite[0];
        }
        else if (b_manager.Stage_Id == 2)
        {
            m_Image.sprite = m_Sprite[1];
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
        
    }
}
