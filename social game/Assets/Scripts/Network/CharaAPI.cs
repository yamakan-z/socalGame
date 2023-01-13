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
    public CharaResponseData[] chara_datas;//所有しているキャラの一覧
}

[System.Serializable]
public class CharaResponseData
{
    public int chara_id;//キャラの種族ID
    public int uniq_chara_id;//キャラのユニークID
    public string rank;//キャラのランク
    public int level;//キャラのレベル
}

[System.Serializable]
public class CharaLevelUpResponse:ResponseDataBase
{
    public int level;//レベルアップ後のレベル
    public int money;//レベルアップ後の所持金
}