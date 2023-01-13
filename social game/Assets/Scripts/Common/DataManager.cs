using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset charaMasterDataText;//キャラクターが定義されたマスターデータ

    [SerializeField]
    private TextAsset[] charaStatusDataText;//各キャラのステータスのマスターデータ

    [SerializeField]
    private TextAsset stageMasterDataText; //ステージに関するマスターデータ

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

        //マスターデータ読み込み
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
    /// キャラの情報を取得
    /// </summary>
    /// <param name="charaId"><取得したいキャラのID/param>
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
    /// キャラのステータスを取得
    /// </summary>
    /// <param name="charaId"><取得したいキャラのID/param>
    /// <param name="level">取得したいキャラのレベル0</param>
    /// <returns>キャラのステータス</returns>
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

    //全ステージ情報を返す変数
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
            //編成データがまだ実在しない場合
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
            //ステージデータがまだ実在しない場合
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
    public int chara_id;//キャラのID
    public string name;//キャラの名前
    public string rank;//キャラのランク
    public Sprite icon_sprite;//アイコン画像
    public Sprite stand_sprite;//立ち絵画像
}

[System.Serializable]
public class CharaStatusData
{
    public int level;          //レベル
    public int hp;             //そのレベルの時のHP
    public int atk;            //そのレベルの時の攻撃力
    public int def;            //そのレベルの時の防御力
    public int level_up_gold;  //次のレベルに上がるのに必要なお金
}


[System.Serializable] 
public class StageMasterData
{
    public int stage_id;        //ステージのID
    public string name;         //ステージの名前
    public int unlock_stage_id; //解除条件
}