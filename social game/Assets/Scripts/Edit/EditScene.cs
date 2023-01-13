using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditScene : SceneBase
{
    [SerializeField]
    private GameObject contentObj;//��������ꏊ

    [SerializeField]
    private GameObject cellPrefab;//�L�����̃A�C�R��

    private EditData editData;//�Ґ����

    //�t�F�[�h�C������O�ɌĂ΂��֐�
    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        //�ۑ�����Ă���Ґ��f�[�^���擾
        editData = ManagerAccessor.Instance.dataManager.GetEditData();

        var api = new CharaAPI();
        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
        yield return StartCoroutine(api.PossessionChara((response) =>
        {
            ManagerAccessor.Instance.screenManager.LoadingAnimation(false);

            //�ʐM�����������ꍇ
            if (response.status.Contains("NG"))
            {
                //�G���[
                Debug.LogError(response.error);
                return;
            }

            //����ȏꍇ

            foreach (var chara in response.chara_datas)
            {
                //���L���Ă���L���������[�v
                GameObject cellObj = Instantiate(cellPrefab,
                    Vector3.zero, Quaternion.identity, contentObj.transform);

                CharaIconCell cell = cellObj.GetComponent<CharaIconCell>();
                cell.SetCharaIcon(chara.chara_id);
                cell.SetCharaLevel(chara.level);

                //�Ґ���񃊃X�g�̒���ID�����邩�`�F�b�N
                if(editData.editCharaList.Contains(chara.uniq_chara_id))
                {
                    //���ݕҐ���
                    cell.SetSelectImage(true);
                }
                else
                {
                    cell.SetSelectImage(false);
                }

                cell.SetOnTapEvent(() =>
                {
                    //�A�C�R�����^�b�v���ꂽ��Ă΂��
                    if(editData.editCharaList.Contains(chara.uniq_chara_id))
                    {
                        //�I�𒆂̏ꍇ�͖��I����
                        cell.SetSelectImage(false);
                        editData.editCharaList.Remove(chara.uniq_chara_id);
                    }
                    else
                    {
                        //���I���̏ꍇ�͑I����Ԃ�
                        cell.SetSelectImage(true);
                        editData.editCharaList.Add(chara.uniq_chara_id);
                    }
                });

            }
        }));
    }


    public void OnTapSaveButton()
    {
        if(editData.editCharaList.Count !=3)
        {
            Debug.Log("�Ґ������o�[��3�l�ł�");
        }
        ManagerAccessor.Instance.dataManager.SaveEditData(editData);
    }

    public void OnTapReturnButton()
    {
        //�߂�{�^���������ꂽ��z�[���ɑJ��
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.HomeScene);
    }

}
//�Ґ������ێ�����N���X
[System.Serializable]
public class EditData
{
    public List<int> editCharaList;//���ݕҐ����Ă�L�����̃��j�[�NID
}
