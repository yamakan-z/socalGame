using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : SceneBase
{
    //�t�F�[�h�C������O�ɌĂ΂��֐�
    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        var api = new UserAPI();
        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);

        yield return StartCoroutine(api.GetUserData((response) =>
        {
            ManagerAccessor.Instance.screenManager.LoadingAnimation(false);

            if(response.status.Contains("NG"))
            {
                //�G���[�̏ꍇ
                Debug.LogError(response.error);
                return;
            }

            //�ʐM�ɐ���
            ManagerAccessor.Instance.headerManager.IsShowHeader(true);
            ManagerAccessor.Instance.headerManager.SetMoneyNum(response.money);
            ManagerAccessor.Instance.headerManager.SetGemNum(response.gem);

            ManagerAccessor.Instance.dataManager.Money = response.money;
            ManagerAccessor.Instance.dataManager.Gem = response.gem;
        }));

    }

    public void OnTapBattleButton()
    {
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.StageSelectScene);
    }

    public void OntapEditButton()
    {
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.EditScene);
    }

    public void OnTapChareButton()
    {
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.CharaListScene);
    }

    public void OnTapGachaButton()
    {
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.GachaMenuScene);
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(100,100,80,80),"����+1000"))
        {
            //�{�^���������ꂽ�ꍇ
            int amount = 1000;
            var api = new UserAPI();
            StartCoroutine(api.AddMoney(amount, (response) =>
            {
                ManagerAccessor.Instance.headerManager.SetMoneyNum(response.money);
                ManagerAccessor.Instance.dataManager.Money = response.money;
            }));

        }

        if (GUI.Button(new Rect(100, 200, 80, 80), "�W�F��+1000"))
        {
            //�{�^���������ꂽ�ꍇ
            int amount = 1000;
            var api = new UserAPI();
            StartCoroutine(api.AddGem(amount, (response) =>
            {
                ManagerAccessor.Instance.headerManager.SetGemNum(response.gem);
                ManagerAccessor.Instance.dataManager.Gem = response.gem;
            }));

        }
    }

}
