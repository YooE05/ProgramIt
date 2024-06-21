using System.Collections.Generic;
using UnityEngine;

public class LeversController : MonoBehaviour
{
    public bool IsActiveCondition;

    [SerializeField]
    private List<LeverCell> _levers = new();
    [SerializeField]
    private List<int> _leversNeedPullValues = new();

    public bool CheckAllLeverPulls()
    {
        if (!IsActiveCondition)
        {
            return true;
        }

        if (_levers.Count == _leversNeedPullValues.Count)
        {
            for (int i = 0; i < _levers.Count; i++)
            {
                if (_levers[i].GetPullCount() != _leversNeedPullValues[i])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void ClearPullsCount()
    {
        for (int i = 0; i < _levers.Count; i++)
        {
            _levers[i].ClearPullCount();
        }
    }
}