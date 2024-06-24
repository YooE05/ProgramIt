using UnityEngine;

public class DataController : MonoBehaviour
{
    #region Singleton

    private static DataController insctance;
    public static DataController Instance => insctance;

    private Data _data;
    private string jsoneString;

    [HideInInspector]
    public float Volume;

    private int[] _marks;


    private void Awake()
    {
        if (insctance != null)
        {
            Destroy(gameObject);
        }
        else
        { insctance = this; }

        _data = new Data();
        DontDestroyOnLoad(this.gameObject);
        LoadData();

        _marks = _data.marks;
        Volume = _data.volume;
    }
    #endregion

    public float getLevelMark(int index)
    { return _marks[index]; }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("settings"))
        {
            jsoneString = PlayerPrefs.GetString("settings");
            _data = JsonUtility.FromJson<Data>(jsoneString);
        }
        else
        {
            _data.marks = new int[] { 0, 0, 0, 0, 0, 0 };
            _data.volume = 1f;
        }
    }

    public void SetMark(int mark, int levelIndex)
    {
        _marks[levelIndex] = mark;

        /*if (mark > data.marks[levelIndex] || data.marks[levelIndex] == 0)
        {
            _marks[levelIndex] = mark;
        }*/
    }

    public void SaveData()
    {
        _data.marks = _marks;
        _data.volume = Volume;

        jsoneString = JsonUtility.ToJson(_data);

        PlayerPrefs.SetString("settings", jsoneString);
        PlayerPrefs.Save();
    }
}

public class Data
{
    public int[] marks = new int[6];
    public float volume;
}