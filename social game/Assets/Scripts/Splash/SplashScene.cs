using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScene : SceneBase
{

    [SerializeField]
    private Animator animator;

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.TitleScene);
        }
    }

}
