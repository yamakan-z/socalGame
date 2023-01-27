using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkAni : MonoBehaviour
{

    public BattleScene battlescene;

    public GameObject[] DamegeImage;

    public GameObject DeathAnim;//�{�X���j�A�j���[�V����

    public int Ani_num;//�A�j���[�V�����𗬂��L�����ԍ�

    private bool one;//��񂾂�������ʂ�

    private bool se_one;//��񂾂�SE���o��

    [SerializeField]
    private GameObject battlemanager;//�o�g���}�l�[�W���[�Ăяo��

    BattleManager b_manager;//�o�g���}�l�[�W���[�X�N���v�g


    public AudioSource se_audio;//AudioSource�^�̕ϐ�a��錾 �g�p����AudioSource�R���|�[�l���g���A�^�b�`�K�v

    public AudioSource start_se;//�o��{�C�X�𗬂�

    //�ėpSE
    [NamedArrayAttribute(new string[] { "�U��SE","�U���{�^��SE",  "��ZSE","����������NULL" })]
    public AudioClip[] SE;

    //�e�{�X��SE
    [NamedArrayAttribute(new string[] { "�o��{�C�X", "�U���{�C�X", "��Z�{�C�X", "�����{�C�X","����������NULL" })]
    public AudioClip[] BossSE1;

    public AudioClip[] UseBossSE;//���̃X�e�[�W�Ŏg�p����{�XSE

    public bool StartBossSE_End;//�o��{�C�X�I���t���O

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//�X�N���v�g�l��

        Set_BossSE();//�{�X��SE�ݒ�

        if(!StartBossSE_End)
        {
            start_se.Play();//�{�X�{�C�X�F�o��
            StartBossSE_End = true;
        }

       

        se_one = true;
        one = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("�����"+start_se.isPlaying);

        //�o��{�C�X���I����Ă���Ώ����J�n
        if (!start_se.isPlaying && StartBossSE_End && se_one)
        {
            Debug.Log("�͂���");
            b_manager.timestart = true;
            se_one = false;
        }

        if (!StartBossSE_End)
        {
            start_se.Play();//�{�X�{�C�X�F�U��
            StartBossSE_End = true;
        }

        if (Ani_num==1 && one)
        {
            one = false;

            DamegeImage[0].SetActive(true);//�U���A�j���[�V������\��

            se_audio.PlayOneShot(UseBossSE[1]);//�{�X�{�C�X�F�U��

            se_audio.PlayOneShot(SE[0]);//�{�X�U��SE

            Invoke(nameof(Ani_Reset), 1.0f);//1�b��ɃA�j���[�V���������Z�b�g

        }
        else if(Ani_num == 2 && one)
        {
            one = false;

            DamegeImage[1].SetActive(true);

            se_audio.PlayOneShot(UseBossSE[1]);//�{�X�{�C�X�F�U��

            se_audio.PlayOneShot(SE[0]);//�{�X�U��SE

            Invoke(nameof(Ani_Reset), 1.0f);//1�b��ɃA�j���[�V���������Z�b�g
        }
        else if (Ani_num == 3 && one)
        {
            one = false;

            DamegeImage[2].SetActive(true);

            se_audio.PlayOneShot(UseBossSE[1]);//�{�X�{�C�X�F�U��


            se_audio.PlayOneShot(SE[0]);//�{�X�U��SE

            Invoke(nameof(Ani_Reset), 1.0f);//1�b��ɃA�j���[�V���������Z�b�g
        }


    }

    public void Set_BossSE()
    {
        //�X�e�[�W���Ƀ{�X��SE��ݒ肷��
        if (b_manager.Stage_Id == 1)
        {
            UseBossSE = BossSE1;
        }
        //else if (b_manager.Stage_Id == 2)
        //{
        //    UseBossSE = BossSE2;
        //}
        //else if (b_manager.Stage_Id == 3)
        //{
        //    UseBossSE = BossSE3;
        //}
        //else if (b_manager.Stage_Id == 4)
        //{
        //    UseBossSE = BossSE4;
        //}
        //else if (b_manager.Stage_Id == 5)
        //{
        //    UseBossSE = BossSE5;
        //}
    }

    public void BossDead()
    {
        b_manager.boss_death = true;//�{�X���S�t���OON

        DeathAnim.SetActive(true);//�A�j���[�V�����N��

        //�{�X�����ʂƂ�
        se_audio.PlayOneShot(UseBossSE[3]);//�{�X�{�C�X�F���S
        // �R���[�`���̋N��
        StartCoroutine(b_manager.WinAni());
    }

    //�A�j���[�V�����̃��Z�b�g
    public void Ani_Reset()
    {
        one = true;
        Ani_num = 0;
        DamegeImage[0].SetActive(false);
        DamegeImage[1].SetActive(false);
        DamegeImage[2].SetActive(false);
    }
}
