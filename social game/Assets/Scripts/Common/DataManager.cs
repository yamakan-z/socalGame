using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset charaMasterDataText;//�L�����N�^�[����`���ꂽ�}�X�^�[�f�[�^

    [SerializeField]
    private TextAsset[] charaStatusDataText;//�e�L�����̃X�e�[�^�X�̃}�X�^�[�f�[�^

    [SerializeField]
    private TextAsset stageMasterDataText; //�X�e�[�W�Ɋւ���}�X�^�[�f�[�^

    private CharaMasterData[] charaMasterData = null;
    private Dictionary<int, CharaStatusData[]> charaStatusData = null;
    private StageMasterData[] stageMasterData = null;

    private int money;

    public int Money
    {
        set { money = value; }
        get { return money; }
    }

    private int gem;
    public int Gem
    {
        set { gem = value; }
        get { return gem; }
    }

    private void Awake()
    {
        ManagerAccessor.Instance.dataManager = this;

        //�}�X�^�[�f�[�^�ǂݍ���
        charaMasterData = CSVSerializer.Deserialize<CharaMasterData>(charaMasterDataText.text);

        charaStatusData = new Dictionary<int, CharaStatusData[]>();

        stageMasterData = CSVSerializer.Deserialize<StageMasterData>(stageMasterDataText.text);

        for(int i = 0; i < charaStatusDataText.Length; i++)
        {
            var data = CSVSerializer.Deserialize<CharaStatusData>(charaStatusDataText[i].text);
            charaStatusData.Add(i + 1, data);
        }
    }

    /// <summary>
    /// �L�����̏����擾
    /// </summary>
    /// <param name="charaId"><�擾�������L������ID/param>
    /// <returns></returns>
    public CharaMasterData GetCharaData(int charaId)
    {
        foreach (var chara in charaMasterData)
        {
            if(chara.chara_id==charaId)
            {
                return chara;
            }
        }

        return null;
    }

    /// <summary>
    /// �L�����̃X�e�[�^�X���擾
    /// </summary>
    /// <param name="charaId"><�擾�������L������ID/param>
    /// <param name="level">�擾�������L�����̃��x��0</param>
    /// <returns>�L�����̃X�e�[�^�X</returns>
    public CharaStatusData GetCharaStatusData(int charaId,int level)
    {
        foreach (var chara in charaStatusData[charaId])
        {
            if (chara.level == level)
            {
                return chara;
            }
        }

        return null;
    }

    //�S�X�e�[�W����Ԃ��ϐ�
    public StageMasterData[] GetAllStageData()
    {
        return stageMasterData;
    }

    public string GetUID()
    {
        return PlayerPrefs.GetString("uid", "");
    }

    public void SaveUID(string uid)
    {
        PlayerPrefs.SetString("uid",uid);
    }

    public void DeleteUID()
    {
        PlayerPrefs.DeleteKey("uid");
    }

    public EditData GetEditData()
    {
        var jsonData = PlayerPrefs.GetString("edit", "");
        var editData = JsonUtility.FromJson<EditData>(jsonData);

        if(editData==null)
        {
            //�Ґ��f�[�^���܂����݂��Ȃ��ꍇ
            editData = new EditData();
            editData.editCharaList = new List<int>();
        }

        return editData;

    }
    public void SaveEditData(EditData editData)
    {
        var jsonData = JsonUtility.ToJson(editData);
        PlayerPrefs.SetString("edit", jsonData);
    }

    public void DeleteEditData()
    {
        PlayerPrefs.DeleteKey("edit");
    }

    public StageClearData GetStageClearData()
    {
        var jsonData = PlayerPrefs.GetString("stage", "");
        var stageClearData = JsonUtility.FromJson<StageClearData>(jsonData);

        if (stageClearData == null)
        {
            //�X�e�[�W�f�[�^���܂����݂��Ȃ��ꍇ
            stageClearData = new StageClearData();
            stageClearData.stageClearList = new List<int>();
        }

        return stageClearData;

    }
    public void SaveStageClearData(StageClearData stageClearData)
    {
        var jsonData = JsonUtility.ToJson(stageClearData);
        PlayerPrefs.SetString("stage", jsonData);
    }

    public void DeleteStageClearData()
    {
        PlayerPrefs.DeleteKey("stage");
    }
}


[System.Serializable]
public class CharaMasterData
{
    public int chara_id;//�L������ID
    public string name;//�L�����̖��O
    public string rank;//�L�����̃����N
    public Sprite icon_sprite;//�A�C�R���摜
    public Sprite stand_sprite;//�����G�摜
}

[System.Serializable]
public class CharaStatusData
{
    public int level;          //���x��
    public int hp;             //���̃��x���̎���HP
    public int atk;            //���̃��x���̎��̍U����
    public int def;            //���̃��x���̎��̖h���
    public int level_up_gold;  //���̃��x���ɏオ��̂ɕK�v�Ȃ���
}


[System.Serializable] 
public class StageMasterData
{
    public int stage_id;        //�X�e�[�W��ID
    public string name;         //�X�e�[�W�̖��O
    public int unlock_stage_id; //��������
}