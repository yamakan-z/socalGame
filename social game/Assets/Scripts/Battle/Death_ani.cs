using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_ani : MonoBehaviour
{
    [SerializeField]
    private Animator death_ani;//�{�X�����߂���A�j���[�V����

    [SerializeField]
    private GameObject battlemanager;//�o�g���}�l�[�W���[�Ăяo��

    BattleManager b_manager;//�o�g���}�l�[�W���[�X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//�X�N���v�g�l��

        death_ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (b_manager.B_hp <= 0)
        {
            death_ani.SetBool("a_start", true);//�{�X���j���Ƀ{�X�̉摜�������A�j���[�V����
        }
    }
}
