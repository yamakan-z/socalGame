using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject Stage1Button;

    [SerializeField]  private GameObject battlemanager;//�o�g���}�l�[�W���[�Ăяo��

    BattleManager b_manager;//�o�g���}�l�[�W���[�X�N���v�g

    [SerializeField] private GameObject[] ShaffleButton;//�ʒu���Ⴄ�{�^����p��

    [SerializeField] private float changetime;//�{�^���ύX����

    //�e�X�e�[�W�̕ύX����
    [SerializeField] private float stage2;
    [SerializeField] private float stage3;
    [SerializeField] private float stage4;
    [SerializeField] private float stage5;

    private float use_time;//���̃X�e�[�W�Ɏg������

    [SerializeField] private int randnum;

    private bool second;//2��ڈȍ~�̃{�^���ύX����

   private bool onechange;//���̏����ɂ����{�^���`�F���W

    public int max_BossHP;//�{�X�̍ő�HP



    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//�X�N���v�g�l��

        //�ő�HP��ۑ�
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
        //�{�X��HP�������؂�����,�{�^���`�F���W
        if (b_manager.Stage_Id == 2 && b_manager.B_hp <= max_BossHP / 2)
        {
          
            changetime -= Time.deltaTime;

            if (changetime <= 0 && !onechange)
            {
                Debug.Log("�Ђ�");
                onechange = true;

                //�`�F���W�񐔂ɂ���ď�����ς���
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
            Debug.Log("�Ђ�2");

            changetime -= Time.deltaTime;

            if (changetime <= 0 && !onechange)
            {
                onechange = true;

                //�`�F���W�񐔂ɂ���ď�����ς���
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
        randnum = Random.Range(0, 2);//0,1�̐���Ԃ�

        ShaffleButton[randnum].SetActive(false);
        changetime = use_time;
        onechange = false;//�܂��J�E���g
        second = true;//���ڂ̏����I��
    }

    public void ButtonChange()
    {
        //randnum = Random.Range(0, 2);//0,1�̐���Ԃ�

        if(randnum == 0)//�E
        {
            ShaffleButton[0].SetActive(true);
            ShaffleButton[1].SetActive(false);
            randnum = 1;
        }
        else if (randnum == 1)//��
        {
            ShaffleButton[1].SetActive(true);
            ShaffleButton[0].SetActive(false);
            randnum = 0;
        }

        changetime = use_time;
        onechange = false;//�܂��J�E���g
    }
}
