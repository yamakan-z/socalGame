using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditScene : SceneBase
{
    [SerializeField]
    private GameObject contentObj;//生成する場所

    [SerializeField]
    private GameObject cellPrefab;//キャラのアイコン

    private EditData editData;//編成情報

    //フェードインする前に呼ばれる関数
    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        //保存されている編成データを取得
        editData = ManagerAccessor.Instance.dataManager.GetEditData();

        var api = new CharaAPI();
        ManagerAccessor.Instance.screenManager.LoadingAnimation(true);
        yield return StartCoroutine(api.PossessionChara((response) =>
        {
            ManagerAccessor.Instance.screenManager.LoadingAnimation(false);

            //通信が成功した場合
            if (response.status.Contains("NG"))
            {
                //エラー
                Debug.LogError(response.error);
                return;
            }

            //正常な場合

            foreach (var chara in response.chara_datas)
            {
                //所有しているキャラ分ループ
                GameObject cellObj = Instantiate(cellPrefab,
                    Vector3.zero, Quaternion.identity, contentObj.transform);

                CharaIconCell cell = cellObj.GetComponent<CharaIconCell>();
                cell.SetCharaIcon(chara.chara_id);
                cell.SetCharaLevel(chara.level);

                //編成情報リストの中にIDがあるかチェック
                if(editData.editCharaList.Contains(chara.uniq_chara_id))
                {
                    //現在編成中
                    cell.SetSelectImage(true);
                }
                else
                {
                    cell.SetSelectImage(false);
                }

                cell.SetOnTapEvent(() =>
                {
                    //アイコンがタップされたら呼ばれる
                    if(editData.editCharaList.Contains(chara.uniq_chara_id))
                    {
                        //選択中の場合は未選択に
                        cell.SetSelectImage(false);
                        editData.editCharaList.Remove(chara.uniq_chara_id);
                    }
                    else
                    {
                        //未選択の場合は選択状態に
                        cell.SetSelectImage(true);
                        editData.editCharaList.Add(chara.uniq_chara_id);
                    }
                });

            }
        }));
    }


    public void OnTapSaveButton()
    {
        if(editData.editCharaList.Count !=3)
        {
            Debug.Log("編成メンバーは3人です");
        }
        ManagerAccessor.Instance.dataManager.SaveEditData(editData);
    }

    public void OnTapReturnButton()
    {
        //戻るボタンが押されたらホームに遷移
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.HomeScene);
    }

}
//編成情報を維持するクラス
[System.Serializable]
public class EditData
{
    public List<int> editCharaList;//現在編成してるキャラのユニークID
}
