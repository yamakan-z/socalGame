using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource audio;//�I�[�f�B�I�\�[�X

    public bool start_bgm;//BGM�J�n

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
        Debug.Log("�J�n");
        audio.Play();//�ȊJ�n
    }

    public void BGM_Stop()
    {
        Debug.Log("��~");
        audio.Stop();//�ȃX�g�b�v
    }

}
