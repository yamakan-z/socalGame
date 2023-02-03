using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource audio;//オーディオソース

    public bool start_bgm;//BGM開始

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void BGM_Start()
    {
        Debug.Log("開始");
        audio.Play();//曲開始
    }

    public void BGM_Stop()
    {
        Debug.Log("停止");
        audio.Stop();//曲ストップ
    }

}
