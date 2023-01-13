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
        headerObj.SetActive(false);//������Ԃ͔�\��
    }

    /// <summary>
    /// ��������ݒ肷�邽�߂̊֐�
    /// </summary>
    /// <param name="num">�ݒ肷���</param>
    public void SetMoneyNum(int num)
    {
        moneyText.text = num.ToString();
    }

    /// <summary>
    /// �������Ă����΂̌���ݒ肷�邽�߂̌�
    /// </summary>
    /// <param name="num"></param>
    public void SetGemNum(int num)
    {
        gemText.text = num.ToString();
    }

    /// <summary>
    /// �w�b�_�[�̕\����Ԃ�ݒ肷��
    /// </summary>
    /// <param name="isShow">�\�����邩�ǂ���</param>
    public void IsShowHeader(bool isShow)
    {
        headerObj.SetActive(isShow);
    }
}
