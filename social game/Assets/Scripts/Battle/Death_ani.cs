using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_ani : MonoBehaviour
{
    [SerializeField]
    private Animator death_ani;//ボスが透過するアニメーション

    [SerializeField]
    private GameObject battlemanager;//バトルマネージャー呼び出し

    BattleManager b_manager;//バトルマネージャースクリプト

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//スクリプト獲得

        death_ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (b_manager.B_hp <= 0)
        {
            death_ani.SetBool("a_start", true);//ボス撃破時にボスの画像を消すアニメーション
        }
    }
}
