using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaAnimationScene : SceneBase
{
    [SerializeField]
    private GameObject cellsObj;//アイコンを生成する場所

    [SerializeField]
    private GameObject cellPrefab;//生成するアイコンプレハブ

    //フェードインが完了した際に呼ばれる
    public override void ViewDidFadeIn()
    {
        base.ViewDidFadeIn();

        //演出開始
        StartCoroutine(GachaAnimation());
    }

    IEnumerator GachaAnimation()
    {
        //ガチャの結果を受け取る
        CharaResponseData[] charaDataArray = (CharaResponseData[])ManagerAccessor.Instance.sceneManager.DeliveryData;

        //配列の要素の数だけループ
        foreach(var chara in charaDataArray)
        {
            GameObject obj = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, cellsObj.transform);
            CharaIconCell cell = obj.GetComponent<CharaIconCell>();

            //アイコンにキャラの情報を設定
            cell.SetCharaIcon(chara.chara_id);

            //レア度によって処理が変わる場合
            if(chara.rank == "1")
            {
                Debug.Log("SR");
            }

            if (chara.rank == "2")
            {
                Debug.Log("SSR");
            }

            if (chara.rank == "3")
            {
                Debug.Log("LR");
            }

            //0.5秒経過
            yield return new WaitForSeconds(0.5f);
        }
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.GachaResultScene);

    }

}
