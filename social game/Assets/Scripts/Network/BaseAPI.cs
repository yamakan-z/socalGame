using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BaseAPI
{
    protected string baseUrl = "https://kc-network-lesson.com/api/game/";
    protected string category = "";
    protected string method = "";
    protected Dictionary<string, int> intRequestDic;//�T�[�o�[�ɑ��M���邽�߂̃��N�G�X�g
    protected Dictionary<string, string> stringRequestDic;//�T�[�o�[�ɑ��M���邽�߂̃��N�G�X�g

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
        //POST�f�[�^�p�̃��N�G�X�g�f�[�^
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

        //���ۂ̒ʐM�̏���
        Debug.Log("�ʐM�J�n");
        yield return www.SendWebRequest();
        Debug.Log("�ʐM�I��");

        //�G���[�`�F�b�N
        if(www.result!=UnityWebRequest.Result.Success)
        {
            //�G���[
            Debug.Log(www.error);
            yield break;
        }

        //�������Ă���ꍇ
        Debug.Log(www.downloadHandler.text);

        //���X�|���X�f�[�^���p�[�X
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