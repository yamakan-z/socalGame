using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderManager : MonoBehaviour
{

    [SerializeField]
    private GameObject headerObj;

    [SerializeField]
    private Text moneyText;

    [SerializeField]
    private Text gemText;

    private void Awake()
    {
        ManagerAccessor.Instance.headerManager = this;
        headerObj.SetActive(false);//初期状態は非表示
    }

    /// <summary>
    /// 所持金を設定するための関数
    /// </summary>
    /// <param name="num">設定する個数</param>
    public void SetMoneyNum(int num)
    {
        moneyText.text = num.ToString();
    }

    /// <summary>
    /// 所持している宝石の個数を設定するための個数
    /// </summary>
    /// <param name="num"></param>
    public void SetGemNum(int num)
    {
        gemText.text = num.ToString();
    }

    /// <summary>
    /// ヘッダーの表示状態を設定する
    /// </summary>
    /// <param name="isShow">表示するかどうか</param>
    public void IsShowHeader(bool isShow)
    {
        headerObj.SetActive(isShow);
    }
}
