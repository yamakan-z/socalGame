using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharaListScene : SceneBase
{
    [SerializeField]
    private GameObject contentObj;//スクロールビューの生成する場所

    [SerializeField]
    private GameObject cellPrefab;//アイコンセルのプレハブ

    [SerializeField]
    private GameObject charaInfoObj;//キャラ情報の表示に使用

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
    private Text levelUpText;//レベルアップに必要なお金

    [SerializeField]
    private Button levelUpButton;

    private int selectUniqCharaId;//現在選択中のキャラのユニークID

   private Dictionary<int, CharaResponseData> possessionCharaDic;//現在所有しているキャラの情報
    private Dictionary<int, CharaIconCell> iconCellDic;//アイコンセルの参照

    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        //キャラのステータス情報最初は非表示
        charaInfoObj.SetActive(false);

        var api = new CharaAPI();
        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
        yield return StartCoroutine(api.PossessionChara((response) =>
        {
            ManagerAccessor.Instance.screenManager.LoadingAnimation(false);

            if (response.status.Contains("NG"))
            {
                //エラーの場合
                Debug.LogError(response.error);
                return;
            }

            //初期化
            possessionCharaDic = new Dictionary<int, CharaResponseData>();
            iconCellDic = new Dictionary<int, CharaIconCell>();

            //所有しているキャラの数だけループ
             foreach (var chara in response.chara_datas)
            {
                var cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, contentObj.transform).GetComponent<CharaIconCell>();
                cell.SetCharaIcon(chara.chara_id);
                cell.SetCharaLevel(chara.level);
                cell.SetOnTapEvent(() =>
                {
                    Debug.Log(chara.uniq_chara_id);
                    //タップされたキャラのステータスを表示する
                    UpdateCharainfo(chara.uniq_chara_id);
                });

                possessionCharaDic.Add(chara.uniq_chara_id, chara);
                iconCellDic.Add(chara.uniq_chara_id, cell);

            }


        }));
    }


    /// <summary>
    /// キャラ情報画面を更新する
    /// </summary>
    /// <param name="uniqCharaId">表示したいキャラのユニークID</param>
    public void UpdateCharainfo(int uniqCharaId)
    {
        //現在選択中のIDを保存
        selectUniqCharaId = uniqCharaId;

        var charaData = ManagerAccessor.Instance.dataManager.GetCharaData(possessionCharaDic[uniqCharaId].chara_id);
        var charaStatusData = ManagerAccessor.Instance.dataManager.
            GetCharaStatusData(possessionCharaDic[uniqCharaId].chara_id, possessionCharaDic[uniqCharaId].level);

        //UIに反映
        nameText.text = charaData.name;
        charaStandImage.sprite = charaData.stand_sprite;
        levelText.text = possessionCharaDic[uniqCharaId].level.ToString();
        hpText.text = charaStatusData.hp.ToString();
        atkText.text = charaStatusData.atk.ToString();
        defText.text = charaStatusData.def.ToString();
        levelUpText.text = charaStatusData.level_up_gold.ToString();

        //レベルアップ出来る時だけボタンを押せるようにする
        levelUpButton.interactable = (ManagerAccessor.Instance.dataManager.Money >= charaStatusData.level_up_gold);

        //UIを表示
        charaInfoObj.SetActive(true);
    }

    public void OnTapReturnButton()
    {
        //戻るボタンが押されたらホームに遷移
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
                  //エラーの場合
                  Debug.LogError(response.error);
                  return;
              }

              //通信が成功
              possessionCharaDic[selectUniqCharaId].level = response.level;
              iconCellDic[selectUniqCharaId].SetCharaLevel(response.level);
              UpdateCharainfo(selectUniqCharaId);

              //お金に冠する情報を更新
              ManagerAccessor.Instance.dataManager.Money = response.money;
              ManagerAccessor.Instance.headerManager.SetMoneyNum(response.money);

          }));

        Debug.Log("LevelUpButton");
    }

}
