using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaAPI : BaseAPI
{
  public GachaAPI()
    {
        category = "gacha";
    }

    public IEnumerator PlayCharaGacha(int num,System.Action<GachaPlayCharaGachaResponse>callback)
    {
        method = "play-chara-gacha";
        AddRequest("uid", ManagerAccessor.Instance.dataManager.GetUID());
        AddRequest("num", num);
        yield return SendWebRequest(callback);
    }
}

[System.Serializable]
public class GachaPlayCharaGachaResponse:ResponseDataBase
{
    public CharaResponseData[] gacha_result;//ƒKƒ`ƒƒ‚ÌŒ‹‰Ê
}
