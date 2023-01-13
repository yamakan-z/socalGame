using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BaseAPI
{
    protected string baseUrl = "https://kc-network-lesson.com/api/game/";
    protected string category = "";
    protected string method = "";
    protected Dictionary<string, int> intRequestDic;//サーバーに送信するためのリクエスト
    protected Dictionary<string, string> stringRequestDic;//サーバーに送信するためのリクエスト

    protected BaseAPI()
    {
        intRequestDic = new Dictionary<string, int>();
        stringRequestDic = new Dictionary<string, string>();
    }

    protected void AddRequest(string name,int data)
    {
        intRequestDic.Add(name, data);
    }

    protected void AddRequest(string name, string data)
    {
        stringRequestDic.Add(name, data);
    }


    protected IEnumerator SendWebRequest<ResponeseDatatType>(System.Action<ResponeseDatatType> callback)
    {
        //POSTデータ用のリクエストデータ
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, int> kvp in intRequestDic)
        {
            form.AddField(kvp.Key, kvp.Value);
        }
        foreach (KeyValuePair<string, string> kvp in stringRequestDic)
        {
            form.AddField(kvp.Key, kvp.Value);
        }

        string url = baseUrl + category + "/" + method;
        UnityWebRequest www = UnityWebRequest.Post(url, form);

        //実際の通信の処理
        Debug.Log("通信開始");
        yield return www.SendWebRequest();
        Debug.Log("通信終了");

        //エラーチェック
        if(www.result!=UnityWebRequest.Result.Success)
        {
            //エラー
            Debug.Log(www.error);
            yield break;
        }

        //成功している場合
        Debug.Log(www.downloadHandler.text);

        //レスポンスデータをパース
        var response = JsonUtility.FromJson<ResponeseDatatType>(www.downloadHandler.text);
        callback(response);

    }

}


[System.Serializable]
public class ResponseDataBase
{
    public string status;
    public string error;
}