using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginAPI : BaseAPI
{
   
    public LoginAPI()
    {
        category = "login";
    }

    public IEnumerator GetUID(System.Action<LoginGetUIDResponse>callback)
    {
        method = "get-uid";
        yield return SendWebRequest(callback);
    }

    public IEnumerator Login(System.Action<LoginLoginResponse> callback)
    {
        method = "login";
        AddRequest("uid", ManagerAccessor.Instance.dataManager.GetUID());
        yield return SendWebRequest(callback);
    }


}


[System.Serializable]
public class LoginGetUIDResponse : ResponseDataBase
{
    public string uid;//ê∂ê¨ÇµÇΩUIDÇ™ì¸ÇÈ
}

[System.Serializable] 
public class LoginLoginResponse : ResponseDataBase
{

}
