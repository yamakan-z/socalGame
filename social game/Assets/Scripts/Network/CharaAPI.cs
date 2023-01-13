using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAPI : BaseAPI
{

    public CharaAPI()
    {
        category = "chara";
    }

    public IEnumerator PossessionChara(System.Action<CharaPossessionCharaResponse>callback)
    {
        method = "possession-chara";
        AddRequest("uid", ManagerAccessor.Instance.dataManager.GetUID());
        yield return SendWebRequest(callback);
    }

    public IEnumerator LevelUp(int uniqCharaId,int useMoney,System.Action<CharaLevelUpResponse>callback)
    {
        method = "level-up";
        AddRequest("uid", ManagerAccessor.Instance.dataManager.GetUID());
        AddRequest("uniq_chara_id", uniqCharaId);
        AddRequest("use_money", useMoney);
        yield return SendWebRequest(callback);
    }

}


[System.Serializable]
public class CharaPossessionCharaResponse : ResponseDataBase
{
    public CharaResponseData[] chara_datas;//���L���Ă���L�����̈ꗗ
}

[System.Serializable]
public class CharaResponseData
{
    public int chara_id;//�L�����̎푰ID
    public int uniq_chara_id;//�L�����̃��j�[�NID
    public string rank;//�L�����̃����N
    public int level;//�L�����̃��x��
}

[System.Serializable]
public class CharaLevelUpResponse:ResponseDataBase
{
    public int level;//���x���A�b�v��̃��x��
    public int money;//���x���A�b�v��̏�����
}