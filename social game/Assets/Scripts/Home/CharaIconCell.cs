using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharaIconCell : MonoBehaviour
{
    [SerializeField]
    private Image charaImage;//�L�����̃A�C�R��

    [SerializeField]
    private GameObject levelArea;//���x����\������G���A

    [SerializeField]
    private Text leveltext;//���x���e�L�X�g

    [SerializeField]
    private Image selectImage;//�I�𒆂�\���摜

    private System.Action tapEvent;//�^�b�v���ꂽ�ۂɎ��s����C�x���g

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
