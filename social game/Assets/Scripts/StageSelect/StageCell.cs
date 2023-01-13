using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageCell : MonoBehaviour
{
    [SerializeField]
    private GameObject lockObj;

    [SerializeField]
    private Text nameText;

    private System.Action tapEvent;

    public void SetLockStage(bool isLock)
    {
        lockObj.SetActive(isLock);
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetOnTapEvent(System.Action tapEvent)
    {
        this.tapEvent = tapEvent;
    }

    public void OnTapCell()
    {
        if(tapEvent != null)
        {
            tapEvent();
        }
    }

}
