using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharaIconCell : MonoBehaviour
{
    [SerializeField]
    private Image charaImage;//キャラのアイコン

    [SerializeField]
    private GameObject levelArea;//レベルを表示するエリア

    [SerializeField]
    private Text leveltext;//レベルテキスト

    [SerializeField]
    private Image selectImage;//選択中を表す画像

    private System.Action tapEvent;//タップされた際に実行するイベント

    private void Awake()
    {
        levelArea.gameObject.SetActive(false);
        selectImage.gameObject.SetActive(false);
    }

    public void SetCharaIcon(int charaId)
    {
        charaImage.sprite = ManagerAccessor.Instance.dataManager.GetCharaData(charaId).icon_sprite;
    }

    public void SetCharaLevel(int level)
    {
        levelArea.gameObject.SetActive(true);
        selectImage.gameObject.SetActive(true);
        leveltext.text = level.ToString();
    }

    public void SetOnTapEvent(System.Action tapEvent)
    {
        this.tapEvent = tapEvent;
    }

    public void OntapIcon()
    {
        if(tapEvent != null)
        {
            tapEvent();
        }
    }

    public void SetSelectImage(bool isSelect)
    {
        selectImage.gameObject.SetActive(isSelect);
    }
}
