using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAPI : BaseAPI
{
  
    public UserAPI()
    {
        category = "user";
    }


    public IEnumerator GetUserData(System.Action<UserGetUserDataResponse>callback)
    {
        method = "get-userdata";
        AddRequest("uid", ManagerAccessor.Instance.dataManager.GetUID());
        yield return SendWebRequest(callback);
    }
   
    public IEnumerator AddMoney(int amount, System.Action<UserAddMoneyResponse> callback)
    {
        method = "add-money";
        AddRequest("uid", ManagerAccessor.Instance.dataManager.GetUID());
        AddRequest("amount", amount);
        yield return SendWebRequest(callback);
    }
    public IEnumerator UseMoney(int amount, System.Action<UserUseMoneyResponse> callback)
    {
        method = "use-money";
        AddRequest("uid", ManagerAccessor.Instance.dataManager.GetUID());
        AddRequest("amount", amount);
        yield return SendWebRequest(callback);
    }
    public IEnumerator AddGem(int amount, System.Action<UserAddGemResponse> callback)
    {
        method = "add-gem";
        AddRequest("uid", ManagerAccessor.Instance.dataManager.GetUID());
        AddRequest("amount", amount);
        yield return SendWebRequest(callback);
    }
    public IEnumerator UseGem(int amount, System.Action<UserUseGemResponse> callback)
    {
        method = "use-gem";
        AddRequest("uid", ManagerAccessor.Instance.dataManager.GetUID());
        AddRequest("amount", amount);
        yield return SendWebRequest(callback);
    }
}



[System.Serializable]
public class UserGetUserDataResponse : ResponseDataBase
{
    public int money;//èäéùã‡
    public int gem;//èäéùïÛêŒ
}

[System.Serializable]
public class UserAddMoneyResponse : ResponseDataBase
{
    public int money;
}

[System.Serializable]
public class UserUseMoneyResponse : ResponseDataBase
{
    public int money;
}


[System.Serializable]
public class UserAddGemResponse : ResponseDataBase
{
    public int gem;
}

[System.Serializable]
public class UserUseGemResponse : ResponseDataBase
{
    public int gem;
}