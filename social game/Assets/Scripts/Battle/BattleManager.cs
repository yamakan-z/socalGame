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

    public bool updatastart;//�A�b�v�f�[�g�̏����𓮂���

    public bool timestart;//�U���^�C�}�[�N��

    public GameObject BattleScene;

    BattleScene battlescene;

    [SerializeField]
    private GameObject AtkAni;

    private  AtkAni atkani;

    public bool get_c;

    public bool judge;//��񂾂�������ʂ�(�����E��������𓯎��ɍs�킹�Ȃ����߁j

    public bool boss_death;
    
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

            if(timestart && !boss_death)
            boss_atktime -= Time.deltaTime;

            //�U�����Ԃ�0�ɂȂ�����U��
            if(boss_atktime<=0)
            battlescene.Boss_Atk();

            if(B_hp<=0 && judge)
            {
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

        // 3�b�ԑ҂�
        yield return new WaitForSeconds(4.5f);

        battlescene.Win();//�{�XHP0�ŏ���
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
}
