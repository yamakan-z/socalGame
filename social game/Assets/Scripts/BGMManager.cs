using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource audio;//�I�[�f�B�I�\�[�X

    public bool start_bgm;//BGM�J�n

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
            Debug.Log("�J�n");
            audio.Play();//�ȊJ�n
            one = true;
        }
    }
}
