using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaAnimationScene : SceneBase
{
    [SerializeField]
    private GameObject cellsObj;//�A�C�R���𐶐�����ꏊ

    [SerializeField]
    private GameObject cellPrefab;//��������A�C�R���v���n�u

    //�t�F�[�h�C�������������ۂɌĂ΂��
    public override void ViewDidFadeIn()
    {
        base.ViewDidFadeIn();

        //���o�J�n
        StartCoroutine(GachaAnimation());
    }

    IEnumerator GachaAnimation()
    {
        //�K�`���̌��ʂ��󂯎��
        CharaResponseData[] charaDataArray = (CharaResponseData[])ManagerAccessor.Instance.sceneManager.DeliveryData;

        //�z��̗v�f�̐��������[�v
        foreach(var chara in charaDataArray)
        {
            GameObject obj = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, cellsObj.transform);
            CharaIconCell cell = obj.GetComponent<CharaIconCell>();

            //�A�C�R���ɃL�����̏���ݒ�
            cell.SetCharaIcon(chara.chara_id);

            //���A�x�ɂ���ď������ς��ꍇ
            if(chara.rank == "1")
            {
                Debug.Log("SR");
            }

            if (chara.rank == "2")
            {
                Debug.Log("SSR");
            }

            if (chara.rank == "3")
            {
                Debug.Log("LR");
            }

            //0.5�b�o��
            yield return new WaitForSeconds(0.5f);
        }
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.GachaResultScene);

    }

}
