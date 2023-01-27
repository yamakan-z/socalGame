using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //�X�e�[�W�f�[�^����{�X�̃X�e�[�^�X�������Ă���
    public int B_hp;
    public int B_atk;
    public int B_def;

    public int Stage_Id;//���݂̃X�e�[�WID�擾
    public int B_Awakening;//�o���̗L��

    public float boss_atktime;//�{�X�̍U���Ԋu

    public int start_spatk;//��Z�̔����J�n�t���O 

    public bool updatastart;//�A�b�v�f�[�g�̏����𓮂���

    public bool timestart;//�U���^�C�}�[�N��

    public GameObject BattleScene;

    BattleScene battlescene;

    [SerializeField]
    private GameObject AtkAni;

    private  AtkAni atkani;

    public bool get_c;

    public bool judge;//��񂾂�������ʂ�(�����E��������𓯎��ɍs�킹�Ȃ����߁j

    public bool boss_death;//�{�X���S�t���O


    public float total_time = 10.0f;//��Z�������Ԃ������ɓ����
    private int second;//�e�L�X�g���f�p

    public bool s_start;//��Z������



    // Start is called before the first frame update
    void Start()
    {
        get_c = true;
        judge = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        //�o�g���V�[�����������ꂽ��^�C�}�[�J�n
        if (updatastart)
        {
            //�������ꂽ�o�g���V�[���������Ŏ擾����
            if(get_c)
            {
                Debug.Log("�擾");
                BattleScene = GameObject.Find("BattleScene(Clone)");
                battlescene = BattleScene.GetComponent<BattleScene>();
                AtkAni = GameObject.Find("AtkAniManager");
                atkani = AtkAni.GetComponent<AtkAni>();

                

                get_c = false;
            }

            if(timestart && !boss_death && !s_start)
            boss_atktime -= Time.deltaTime;//�����ōU�����Ԃ̃J�E���g�_�E��

            //�{�X��HP�����������Ƒ�Z
            if(B_hp <= battlescene.max_BossHP/2 && !boss_death)
            {
             
                if(start_spatk!=1)
                {
                    SP_Atk_Time();
                }
                
                if (start_spatk == 1 && !boss_death && !s_start)
                {
                    battlescene.spatk_timer.SetActive(true);//�^�C�}�[�\��
                    total_time -= Time.deltaTime;//�����J�E���g�_�E���J�n
                    second = (int)total_time;
                    battlescene.timetext.text = second.ToString();//�e�L�X�g�ɔ��f 

                    if (total_time <= 0)
                    {
                        s_start = true;
                        total_time = 10.0f;//�^�C�����Z�b�g
                        Debug.Log("������");
                        second = 0;
                        battlescene.spatk_timer.SetActive(false);//�^�C�}�[��\��
                        //battlescene.Sp_Atk();//�K�E�Z����
                    }


                }
            }

            //�U�����Ԃ�0�ɂȂ�����U��
            if(boss_atktime<=0)
            battlescene.Boss_Atk();

            if(B_hp<=0 && judge)
            {
                battlescene.spatk_timer.SetActive(false);//�^�C�}�[��\��
                atkani.BossDead();//�{�X���S�����Ɉڍs
            }
            else if(battlescene.deathcount == 3 && judge)
            {
                // �R���[�`���̋N��
                StartCoroutine(LoseAni());
            }


        }
       
    }

    public IEnumerator WinAni()
    {
        Debug.Log("����");
        judge = false;

        yield return new WaitForSeconds(4.5f); //�����A�j���[�V�����I���܂ő҂�

        battlescene.Win();//�����������s��
    }

    private IEnumerator LoseAni()
    {
        judge = false;

        // 3�b�ԑ҂�
        yield return new WaitForSeconds(1.5f);

        battlescene.Lose();//�L�����S�łŔs�k
    }

    public void B_Atk_Time()
    {
        //3�`5�b�̊ԂŃ{�X���U��
        boss_atktime = Random.Range(3.0f, 6.0f);
    }

    public void SP_Atk_Time()
    {
        start_spatk = Random.Range(0, 8);
    }
}
