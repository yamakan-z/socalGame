using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaMenuScene : SceneBase
{
    public void OnTapGachaButton1()
    {
        var api = new GachaAPI();
        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
        ManagerAccessor.Instance.screenManager.TapGuard(true);

        StartCoroutine(api.PlayCharaGacha(1, (response) =>
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

             //�K�`���̌��ʂ��ꎞ�ۑ�
             ManagerAccessor.Instance.sceneManager.DeliveryData = (object)response.gacha_result;
             ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.GachaAnimationScene);
            

         }));
    }

    public void OnTapGachaButton10()
    {
        var api = new GachaAPI();
        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
        ManagerAccessor.Instance.screenManager.TapGuard(true);

        StartCoroutine(api.PlayCharaGacha(10, (response) =>
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

            //�K�`���̌��ʂ��ꎞ�ۑ�
            ManagerAccessor.Instance.sceneManager.DeliveryData = (object)response.gacha_result;
            ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.GachaAnimationScene);


        }));
    }

    public void OnTapReturnButton()
    {
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.HomeScene);
    }
}
