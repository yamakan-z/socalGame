using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource audio;//オーディオソース

    public bool start_bgm;//BGM開始

    bool one;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start_bgm && !one)
        {
            Debug.Log("開始");
            audio.Play();//曲開始
            one = true;
        }
    }
}
