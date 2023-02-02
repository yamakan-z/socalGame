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
        //�{�X��HP�������؂�����,�{�^���`�F���W
        if (b_manager.Stage_Id == 2 && b_manager.B_hp <= max_BossHP / 2)
        {
            Debug.Log("�Ђ�");
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
            Debug.Log("�Ђ�2");

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
        int randnum = Random.Range(0, 2);//0,1�̐���Ԃ�

        ShaffleButton[randnum].SetActive(true);
    }

}
