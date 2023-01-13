using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharaListScene : SceneBase
{
    [SerializeField]
    private GameObject contentObj;//�X�N���[���r���[�̐�������ꏊ

    [SerializeField]
    private GameObject cellPrefab;//�A�C�R���Z���̃v���n�u

    [SerializeField]
    private GameObject charaInfoObj;//�L�������̕\���Ɏg�p

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Text hpText;

    [SerializeField]
    private Text atkText;

    [SerializeField]
    private Text defText;

    [SerializeField]
    private Image charaStandImage;

    [SerializeField]
    private Text levelUpText;//���x���A�b�v�ɕK�v�Ȃ���

    [SerializeField]
    private Button levelUpButton;

    private int selectUniqCharaId;//���ݑI�𒆂̃L�����̃��j�[�NID

   private Dictionary<int, CharaResponseData> possessionCharaDic;//���ݏ��L���Ă���L�����̏��
    private Dictionary<int, CharaIconCell> iconCellDic;//�A�C�R���Z���̎Q��

    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        //�L�����̃X�e�[�^�X���ŏ��͔�\��
        charaInfoObj.SetActive(false);

        var api = new CharaAPI();
        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
        yield return StartCoroutine(api.PossessionChara((response) =>
        {
            ManagerAccessor.Instance.screenManager.LoadingAnimation(false);

            if (response.status.Contains("NG"))
            {
                //�G���[�̏ꍇ
                Debug.LogError(response.error);
                return;
            }

            //������
            possessionCharaDic = new Dictionary<int, CharaResponseData>();
            iconCellDic = new Dictionary<int, CharaIconCell>();

            //���L���Ă���L�����̐��������[�v
             foreach (var chara in response.chara_datas)
            {
                var cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, contentObj.transform).GetComponent<CharaIconCell>();
                cell.SetCharaIcon(chara.chara_id);
                cell.SetCharaLevel(chara.level);
                cell.SetOnTapEvent(() =>
                {
                    Debug.Log(chara.uniq_chara_id);
                    //�^�b�v���ꂽ�L�����̃X�e�[�^�X��\������
                    UpdateCharainfo(chara.uniq_chara_id);
                });

                possessionCharaDic.Add(chara.uniq_chara_id, chara);
                iconCellDic.Add(chara.uniq_chara_id, cell);

            }


        }));
    }


    /// <summary>
    /// �L��������ʂ��X�V����
    /// </summary>
    /// <param name="uniqCharaId">�\���������L�����̃��j�[�NID</param>
    public void UpdateCharainfo(int uniqCharaId)
    {
        //���ݑI�𒆂�ID��ۑ�
        selectUniqCharaId = uniqCharaId;

        var charaData = ManagerAccessor.Instance.dataManager.GetCharaData(possessionCharaDic[uniqCharaId].chara_id);
        var charaStatusData = ManagerAccessor.Instance.dataManager.
            GetCharaStatusData(possessionCharaDic[uniqCharaId].chara_id, possessionCharaDic[uniqCharaId].level);

        //UI�ɔ��f
        nameText.text = charaData.name;
        charaStandImage.sprite = charaData.stand_sprite;
        levelText.text = possessionCharaDic[uniqCharaId].level.ToString();
        hpText.text = charaStatusData.hp.ToString();
        atkText.text = charaStatusData.atk.ToString();
        defText.text = charaStatusData.def.ToString();
        levelUpText.text = charaStatusData.level_up_gold.ToString();

        //���x���A�b�v�o���鎞�����{�^����������悤�ɂ���
        levelUpButton.interactable = (ManagerAccessor.Instance.dataManager.Money >= charaStatusData.level_up_gold);

        //UI��\��
        charaInfoObj.SetActive(true);
    }

    public void OnTapReturnButton()
    {
        //�߂�{�^���������ꂽ��z�[���ɑJ��
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.HomeScene);
    }

    public void OnTapLevelUpButton()
    {
        var charaStatusData = ManagerAccessor.Instance.dataManager.
            GetCharaStatusData(possessionCharaDic[selectUniqCharaId].chara_id, possessionCharaDic[selectUniqCharaId].level);

        var api = new CharaAPI();

        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
        ManagerAccessor.Instance.screenManager.TapGuard(true);

        StartCoroutine(api.LevelUp(selectUniqCharaId, charaStatusData.level_up_gold, (response) =>
          {

              ManagerAccessor.Instance.screenManager.LoadingAnimation(false);
              ManagerAccessor.Instance.screenManager.TapGuard(false);

              if (response.status.Contains("NG"))
              {
                  //�G���[�̏ꍇ
                  Debug.LogError(response.error);
                  return;
              }

              //�ʐM������
              possessionCharaDic[selectUniqCharaId].level = response.level;
              iconCellDic[selectUniqCharaId].SetCharaLevel(response.level);
              UpdateCharainfo(selectUniqCharaId);

              //�����Ɋ���������X�V
              ManagerAccessor.Instance.dataManager.Money = response.money;
              ManagerAccessor.Instance.headerManager.SetMoneyNum(response.money);

          }));

        Debug.Log("LevelUpButton");
    }

}
