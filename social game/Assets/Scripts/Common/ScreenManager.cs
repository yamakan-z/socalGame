using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private Image tapGuardImage;//画面のタップを制限するためのイメージ

    [SerializeField]
    private Animator fadeAnimator;//フェードを制御するアニメーター

    [SerializeField]
    private GameObject loadingObj;//

    // private System.Action callback;//コールバック保存用の変数

    //private bool isAnimation;//現在アニメーション中かのフラグ

    private void Awake()
    {
        ManagerAccessor.Instance.screenManager = this;
    }


    //private void Update()
    //{
    //    //アニメーション中はアニメーションが終了したかチェックする
    //    if (isAnimation && fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idel"))
    //    {
    //        //アニメーションが終了している場合
    //        callback();
    //        callback = null;
    //        isAnimation = false;
    //    }
    //}

    //タップガードの制御
    public void TapGuard(bool isOn)
    {
        tapGuardImage.gameObject.SetActive(isOn);
    }

    public void LoadingAnimation(bool isOn)
    {
        loadingObj.SetActive(isOn);
    }

    //画面を暗くする
    //public void StartFadeOut(System.Action callback)
    //{
    //    this.callback = callback;
    //    isAnimation = true;
    //    fadeAnimator.SetTrigger("startFadeOut");
    //}

    public IEnumerator StartFadeOut()
    {
        fadeAnimator.SetTrigger("startFadeOut");
        yield return null;//1フレーム待つ

        while (!fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //演出中は待機
            yield return null;
        }

    }

    //画面を明るくする
    //public void StartFadeIn(System.Action callback)
    //{
    //    this.callback = callback;
    //    isAnimation = true;
    //    fadeAnimator.SetTrigger("startFadeIn");
    //}

    public IEnumerator StartFadeIn()
    {
        fadeAnimator.SetTrigger("startFadeIn");
        yield return null;//1フレーム待つ

        while (!fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //演出中は待機
            yield return null;
        }
    }
}
