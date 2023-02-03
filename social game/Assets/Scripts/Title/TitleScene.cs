using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TitleScene : SceneBase
{

    [SerializeField]
    private Text uidText;//UID�\���p�̃e�L�X�g

    [SerializeField] private GameObject B_manager;//BGM�}�l�[�W���[�Ăяo��

    BGMManager bgmmanager;//BGM�X�N���v�g


    override public void Init()
    {
        base.Init();

        B_manager = GameObject.Find("BGMManager");
        bgmmanager = B_manager.GetComponent<BGMManager>();//�X�N���v�g�l��

        string uid = ManagerAccessor.Instance.dataManager.GetUID();
        if(string.IsNullOrEmpty(uid))
        {
            //UID���Ȃ��ꍇ
            uidText.text = "0";
        }
        else
        {
            //UID������ꍇ
            uidText.text = uid;
        }
    }

    public void OnTapDeleteUIDButton()
    {

        Debug.Log("�폜");
        //UID���폜
        ManagerAccessor.Instance.dataManager.DeleteUID();

        //�\�����߂�
        uidText.text = "0";

        //���ɂ�������������̂�����
        ManagerAccessor.Instance.dataManager.DeleteEditData();
        ManagerAccessor.Instance.dataManager.DeleteStageClearData();

    }



    public void OnTapButton()
    {
        //UID�����邩�`�F�b�N
        string uid = ManagerAccessor.Instance.dataManager.GetUID();
        if(string.IsNullOrEmpty(uid))
        {
            //UID���Ȃ��ꍇ
            var api = new LoginAPI();
            ManagerAccessor.Instance.screenManager.TapGuard(true);
            ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
            StartCoroutine(api.GetUID((response)=> 
            {

                ManagerAccessor.Instance.screenManager.TapGuard(false);
                ManagerAccessor.Instance.screenManager.LoadingAnimation(false);

                //�ʐM�����������ꍇ
                if (response.status.Contains("NG"))
                {
                    //�G���[
                    Debug.LogError(response.error);
                    return;
                }

                //����ȏꍇ

                //UID��ۑ�
                ManagerAccessor.Instance.dataManager.SaveUID(response.uid);

                //���O�C����ʂ�
                LoginProcess();

            }));
        }
        else
        {
            //UID������ꍇ
            LoginProcess();
        }
    }

    /// <summary>
    /// ���O�C���̏���
    /// ���O�C�����s���z�[���V�[���ɑJ��
    /// </summary>
    private void LoginProcess()
    {
        var api = new LoginAPI();
        ManagerAccessor.Instance.screenManager.TapGuard(true);
        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
        StartCoroutine(api.Login((response)=>
        {

            ManagerAccessor.Instance.screenManager.TapGuard(false);
            ManagerAccessor.Instance.screenManager.LoadingAnimation(false);
            bgmmanager.BGM_Start();//������BGM���V�[��BGM��

            //�ʐM�����������ꍇ
            if (response.status.Contains("NG"))
            {
                //�G���[
                Debug.LogError(response.error);
                return;
            }

            ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.HomeScene);



        }));
    }

}

