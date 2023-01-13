using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private Image tapGuardImage;//��ʂ̃^�b�v�𐧌����邽�߂̃C���[�W

    [SerializeField]
    private Animator fadeAnimator;//�t�F�[�h�𐧌䂷��A�j���[�^�[

    [SerializeField]
    private GameObject loadingObj;//

    // private System.Action callback;//�R�[���o�b�N�ۑ��p�̕ϐ�

    //private bool isAnimation;//���݃A�j���[�V���������̃t���O

    private void Awake()
    {
        ManagerAccessor.Instance.screenManager = this;
    }


    //private void Update()
    //{
    //    //�A�j���[�V�������̓A�j���[�V�������I���������`�F�b�N����
    //    if (isAnimation && fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idel"))
    //    {
    //        //�A�j���[�V�������I�����Ă���ꍇ
    //        callback();
    //        callback = null;
    //        isAnimation = false;
    //    }
    //}

    //�^�b�v�K�[�h�̐���
    public void TapGuard(bool isOn)
    {
        tapGuardImage.gameObject.SetActive(isOn);
    }

    public void LoadingAnimation(bool isOn)
    {
        loadingObj.SetActive(isOn);
    }

    //��ʂ��Â�����
    //public void StartFadeOut(System.Action callback)
    //{
    //    this.callback = callback;
    //    isAnimation = true;
    //    fadeAnimator.SetTrigger("startFadeOut");
    //}

    public IEnumerator StartFadeOut()
    {
        fadeAnimator.SetTrigger("startFadeOut");
        yield return null;//1�t���[���҂�

        while (!fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //���o���͑ҋ@
            yield return null;
        }

    }

    //��ʂ𖾂邭����
    //public void StartFadeIn(System.Action callback)
    //{
    //    this.callback = callback;
    //    isAnimation = true;
    //    fadeAnimator.SetTrigger("startFadeIn");
    //}

    public IEnumerator StartFadeIn()
    {
        fadeAnimator.SetTrigger("startFadeIn");
        yield return null;//1�t���[���҂�

        while (!fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //���o���͑ҋ@
            yield return null;
        }
    }
}
