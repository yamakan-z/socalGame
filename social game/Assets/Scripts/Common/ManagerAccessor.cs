using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e�}�l�[�W���[�N���X�ւ̃A�N�Z�X���ȈՉ�����N���X
/// </summary>
public class ManagerAccessor
{
    private static ManagerAccessor instance = null;
    public static ManagerAccessor Instance
    {
        get
        {
            if(instance==null)
            {
                instance = new ManagerAccessor();
            }
            return instance;
        }
    }

    //�}�l�[�W���[�̎Q��
    public SceneManager sceneManager;
    public ScreenManager screenManager;
    public DataManager dataManager;
    public HeaderManager headerManager;
}
