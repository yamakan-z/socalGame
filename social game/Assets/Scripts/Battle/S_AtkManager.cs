using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AtkManager : MonoBehaviour
{

    public GameObject SpAtkiImage;

    [SerializeField]  private GameObject battlemanager;//�o�g���}�l�[�W���[�Ăяo��

    BattleManager b_manager;//�o�g���}�l�[�W���[�X�N���v�g

    public GameObject BattleScene;

    [SerializeField]  private GameObject AtkAni;

    private AtkAni atkani;

    BattleScene battlescene;

    //��Z�{�C�X�i�X�e�[�W���j
    [SerializeField] private AudioSource stage1;
    [SerializeField] private AudioSource stage2;
    [SerializeField] private AudioSource stage3;
    [SerializeField] private AudioSource stage4;
    [SerializeField] private AudioSource stage5;

    [SerializeField] private AudioSource UseVoice;//�g�p����{�C�X

    public bool SpAtkVoice_End;//��Z�{�C�X�I���t���O

    private bool se_one;

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//�X�N���v�g�l��

        BattleScene = GameObject.Find("BattleScene(Clone)");
        battlescene = BattleScene.GetComponent<BattleScene>();

        AtkAni = GameObject.Find("AtkAniManager");
        atkani = AtkAni.GetComponent<AtkAni>();


        if ( b_manager.Stage_Id==1)
        {
            UseVoice = stage1;
        }
       else if(b_manager.Stage_Id == 2)
        {
            UseVoice = stage2;
        }
        else if (b_manager.Stage_Id == 3)
        {
            UseVoice = stage3;
        }
        else if (b_manager.Stage_Id == 4)
        {
            UseVoice = stage4;
        }
        else if (b_manager.Stage_Id == 5)
        {
            UseVoice = stage5;
        }

        SpAtkiImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (b_manager.s_start == true)
        {
            if(!SpAtkVoice_End)
            {
                UseVoice.Play();
                SpAtkVoice_End = true;
            }

            if(SpAtkVoice_End&&!UseVoice.isPlaying)
            {
                Debug.Log("qq");
                SpAtkiImage.SetActive(true);
               // battlescene.Sp_Atk();//�K�E�Z����
                StartCoroutine(EndSpAtk());
            }

          
        }
        else
        {
            Debug.Log("q3q");
            SpAtkiImage.SetActive(false);
        }

    }

    public IEnumerator EndSpAtk()
    {
        Debug.Log("de");

        if(!se_one)
        {
            atkani.se_audio.PlayOneShot(atkani.SE[2]);//�Ō���
            se_one = true;
        }
        

        yield return new WaitForSeconds(2.5f); //�����A�j���[�V�����I���܂ő҂�

        SpAtkiImage.SetActive(false);

        battlescene.Sp_Atk();//�K�E�Z����

        //��Z�Ĕ����\��
       // b_manager.s_start = false;
        b_manager.start_spatk = 0;
        SpAtkVoice_End = false;
    }
}
