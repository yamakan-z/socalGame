using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    // Image �R���|�[�l���g���i�[����ϐ�
    [SerializeField] private Image m_Image;

    // �X�v���C�g�I�u�W�F�N�g���i�[����z��
    public Sprite[] m_Sprite;

    [SerializeField]
    private GameObject battlemanager;//�o�g���}�l�[�W���[�Ăяo��

    BattleManager b_manager;//�o�g���}�l�[�W���[�X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//�X�N���v�g�l��

        //�����Ŋe�X�e�[�W�̔w�i�`�F���W
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
