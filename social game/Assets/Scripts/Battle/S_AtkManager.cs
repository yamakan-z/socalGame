using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AtkManager : MonoBehaviour
{

    public GameObject SpAtkiImage;

    [SerializeField]  private GameObject battlemanager;//バトルマネージャー呼び出し

    BattleManager b_manager;//バトルマネージャースクリプト

    public GameObject BattleScene;

    [SerializeField]  private GameObject AtkAni;

    private AtkAni atkani;

    BattleScene battlescene;

    //大技ボイス（ステージ毎）
    [SerializeField] private AudioSource stage1;
    [SerializeField] private AudioSource stage2;
    [SerializeField] private AudioSource stage3;
    [SerializeField] private AudioSource stage4;
    [SerializeField] private AudioSource stage5;

    [SerializeField] private AudioSource UseVoice;//使用するボイス

    public bool SpAtkVoice_End;//大技ボイス終了フラグ

    private bool se_one;

    // Start is called before the first frame update
    void Start()
    {
        battlemanager = GameObject.Find("BattleManager");
        b_manager = battlemanager.GetComponent<BattleManager>();//スクリプト獲得

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
               // battlescene.Sp_Atk();//必殺技発動
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
            atkani.se_audio.PlayOneShot(atkani.SE[2]);//打撃音
            se_one = true;
        }
        

        yield return new WaitForSeconds(2.5f); //爆発アニメーション終了まで待つ

        SpAtkiImage.SetActive(false);

        battlescene.Sp_Atk();//必殺技発動

        //大技再発動可能に
       // b_manager.s_start = false;
        b_manager.start_spatk = 0;
        SpAtkVoice_End = false;
    }
}
