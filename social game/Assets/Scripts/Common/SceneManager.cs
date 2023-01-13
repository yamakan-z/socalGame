using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [System.Serializable]
    public struct scenePrefab
    {
        public SceneType.Type type;
        public GameObject prefab;
    }

    [SerializeField]
    private List<scenePrefab> scenePrefabList;//�V�[�����X�g

    [SerializeField]
    private Camera mainCamera;

    public Camera MainCamera
    {
        get { return mainCamera; }
    }


    [SerializeField]
    private ScreenManager screenManager;

    [SerializeField]
    private GameObject sceneRoot;//�V�[����Root�ƂȂ�I�u�W�F�N�g�̎Q��

    [SerializeField]
    private SceneType.Type firstScene;//�ŏ��ɌĂ΂��V�[��

    private SceneBase currentScene;//���݂̃V�[��

    private SceneType.Type nextSceneType = SceneType.Type.None;//���ɑJ�ڂ���V�[��

    private bool isSceneChange;//���݃V�[���J�ڒ����̃t���O

    private object deliveryData; //�V�[���ԂŃf�[�^���󂯓n�������Ƃ��Ɉꎞ�I�ɕۑ����邽�߂̕ϐ�

    public object DeliveryData
    {
        set { deliveryData = value;}
        get { return deliveryData; }
    }


    private void Awake()
    {
        ManagerAccessor.Instance.sceneManager = this;
        CreateScene(firstScene);
    }

    /// <summary>
    /// �V�[���J�ڎ��ɌĂԊ֐�
    /// </summary>
    /// <param name="sceneType">�J�ڐ�̃V�[��</param>
    public void SceneChange(SceneType.Type sceneType)
    {
        if(isSceneChange)
        {
            //���łɃV�[���J�ڒ��̏ꍇ�͖���
            return;
        }
        isSceneChange = true;
        nextSceneType = sceneType;

        //��ʂ̃^�b�v�𐧌�
        screenManager.TapGuard(true);

        StartCoroutine("ChangeScene");
    }

    /// <summary>
    /// ���ۂ̃V�[���J�ڂ̏���
    /// </summary>
    /// <param name="sceneType"></param>
    /// 
    IEnumerator ChangeScene()
    {
        Debug.Log("StartFadeOut");
        yield return StartCoroutine(screenManager.StartFadeOut());

        //���݂̃V�[����j��
        Debug.Log("DeleteScene");
        DeleteScene();

        //�V�����V�[�����쐬
        Debug.Log("CreateScene");
        CreateScene(nextSceneType);

        //ViewWillFadeIn�̎��s
        Debug.Log("ViewWillFadeIn");
        yield return StartCoroutine(currentScene.ViewWillFadeIn());

        //viewDidFadeIn�̎��s
        Debug.Log("ViewDidFadeIn");
        currentScene.ViewDidFadeIn();

        nextSceneType = SceneType.Type.None;

        screenManager.TapGuard(false);

        isSceneChange = false;
    }

    //�V�[���𐶐�����֐�
    void CreateScene(SceneType.Type sceneType)
    {
        //��������V�[����prefab������
        GameObject prefab = scenePrefabList.Find(scenePrefab => scenePrefab.type == sceneType).prefab;

        //sceneRoot�ɃV�[���𐶐�����
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, sceneRoot.transform);

        currentScene = obj.GetComponent<SceneBase>();
        currentScene.SceneManager = this;
        currentScene.ScreenManager = screenManager;
        currentScene.Init();
    }


    //���݂̃V�[����j������֐�
    void DeleteScene()
    {
        currentScene.Deinit();
        Destroy(currentScene.gameObject);
        currentScene = null;
    }

}
