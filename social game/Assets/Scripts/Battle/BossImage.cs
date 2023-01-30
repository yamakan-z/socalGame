using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class BossImage : MonoBehaviour
{

    // Image �R���|�[�l���g���i�[����ϐ�
    [SerializeField] private Image m_Image;

    // �X�v���C�g�I�u�W�F�N�g���i�[����z��
    public Sprite[] m_Sprite;

    [SerializeField]
    private GameObject battlemanager;//�o�g���}�l�[�W���[�Ăяo��

    BattleManager b_manager;//�o�g���}�l�[�W���[�X�N���v�g

    [SerializeField] private GameObject Stage1Button;//�����߂�ǂ��̂ł����Ń{�^���̏���������

    [SerializeField] private GameObject[] CreateButton;//��������{�^��

    [SerializeField] private GameObject[] ButtonGene;

    [SerializeField] private float changetime;//�{�^���ύX����

    public int max_BossHP;//�{�X�̍ő�HP

    private bool onechange;//���̏����ɂ����{�^���`�F���W

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//�X�N���v�g�l��

        //�ő�HP��ۑ�
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
        //�{�X��HP�������؂�����,�{�^���`�F���W
        if(b_manager.Stage_Id == 2 && b_manager.B_hp <= max_BossHP / 2)
        {
            Debug.Log("�Ђ�");
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
        int randnum = Random.Range(0, 2);//0,1�̐���Ԃ�

        Instantiate(CreateButton[randnum], ButtonGene[randnum].transform.position, Quaternion.identity);
    }


}


