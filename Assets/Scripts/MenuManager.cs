using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] _marksTexts;
    [SerializeField]
    private Slider _volumeSlider;

    private DataController dataController;

    private void Awake()
    {
        dataController = FindObjectOfType<DataController>();
    }

    private void Start()
    {
        SetUpLevelMarks();
        SetUpMenuSlider();
    }

    void SetUpLevelMarks()
    {
        //setup marks
        for (int i = 0; i < _marksTexts.Length; i++)
        {
            _marksTexts[i].text = dataController.getLevelMark(i).ToString();

            if (_marksTexts[i].text == "0")
            {
                _marksTexts[i].text = "-";
            }
        }
    }

    private void SetUpMenuSlider()
    {
        //setup volume
        _volumeSlider.value = dataController.Volume;
        AudioManager.Instance.ChangeVolume("Master", dataController.Volume);
    }

    public void SetVolumeData()
    {
        dataController.Volume = _volumeSlider.value;
        AudioManager.Instance.ChangeVolume("Master", dataController.Volume);
        dataController.SaveData();
    }

    public void OffMusic()
    {
        _volumeSlider.value = 0f;

        /* dataController.Volume = 0f;
         AudioManager.Instance.ChangeVolume("Master", dataController.Volume);
         dataController.SaveData();*/
    }

    public void ResetLevelMarks()
    {
        for (int i = 0; i < 6; i++)
        {
            dataController.SetMark(0, i);
        }

        SetUpLevelMarks();
    }
}
