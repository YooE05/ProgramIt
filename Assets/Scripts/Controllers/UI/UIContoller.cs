using System;
using UnityEngine;
using TMPro;

public sealed class UIContoller : MonoBehaviour
{
    [SerializeField]
    private GameObject _endPanel;
    [SerializeField]
    private GameObject _nextButtonOnEndPanel;
    [SerializeField]
    private TextMeshProUGUI _markText;
    [SerializeField]
    private TextMeshProUGUI _markConclusionText;

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

    internal void SetupEndPanel(string mark, string markConclusion)
    {
        _markText.text = mark;
        _markConclusionText.text = markConclusion;
    }

    internal void HideNextButtonOnEndPanel()
    {
        _nextButtonOnEndPanel.SetActive(false);
    }

    internal void ShowNextButtonOnEndPanel()
    {
        _nextButtonOnEndPanel.SetActive(true);
    }

}
