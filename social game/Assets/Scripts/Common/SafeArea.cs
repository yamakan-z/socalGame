using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SafeArea : MonoBehaviour
{
    public RectTransform panel;
    Rect lastSafeArea = new Rect(0, 0, 0, 0);

    void ApplySafeArea(Rect area)
    {
        panel.anchoredPosition = Vector2.zero;
        panel.sizeDelta = Vector2.zero;

        var anchorMin = area.position;
        var anchorMax = area.position + area.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
        lastSafeArea = area;
    }

    private void Update()
    {
        if(panel==null)
        {
            return;
        }

        Rect safeArea = Screen.safeArea;

#if UNITY_EDITOR
        //sizeA
        if(Screen.width==1334&&Screen.height==750)
        {

        }
        //sizeB
        if (Screen.width == 1792 && Screen.height == 828)
        {
            safeArea.x = 88;
            safeArea.y = 42;
            safeArea.height = 786;
            safeArea.width = 1616;
        }
#endif
        if(safeArea!=lastSafeArea)
        {
            ApplySafeArea(safeArea);
        }



    }

}
