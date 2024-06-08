using System;
using UnityEngine;
using TMPro;

public sealed class UIContoller : MonoBehaviour
{
    [SerializeField]
    private GameObject _endPanel;
    [SerializeField]
    private TextMeshProUGUI _markText;

    private void Start()
    {
        HideEndPanel();
    }

    public void HideEndPanel()
    {
        _endPanel.SetActive(false);
    }

    public void ShowEndPanel()
    {
        _endPanel.SetActive(true);
    }

    internal void SetupEndPanel(string mark)
    {
        _markText.text = mark;
    }
}
