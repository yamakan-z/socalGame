using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaResultScene : SceneBase
{
    [SerializeField]
    private GameObject cellsObj;

    [SerializeField]
    private GameObject cellPrefabs;

    public override IEnumerator ViewWillFadeIn()
    {
        yield return base.ViewWillFadeIn();

        CharaResponseData[] charaDataArray = (CharaResponseData[])ManagerAccessor.Instance.sceneManager.DeliveryData;

        foreach(var chara in charaDataArray)
        {
            //ƒZƒ‹‚Ì¶¬
            GameObject obj = Instantiate(cellPrefabs, Vector3.zero, Quaternion.identity, cellsObj.transform);
            CharaIconCell cell = obj.GetComponent<CharaIconCell>();

            //ƒLƒƒƒ‰‚Ìî•ñ‚ğİ’è
            cell.SetCharaIcon(chara.chara_id);

        }
    }

    public void OnTapReturnButton()
    {
        ManagerAccessor.Instance.sceneManager.SceneChange(SceneType.Type.GachaMenuScene);
    }
}
