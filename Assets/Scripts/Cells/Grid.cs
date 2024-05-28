using System.Collections.Generic;
using UnityEngine;

public sealed class Grid : MonoBehaviour
{
    [SerializeField]
    private List<CellBase> _allCells = new();

    private List<List<CellBase>> _cellsList = new();

    [SerializeField]
    private CellBase _wrongCell;

    private void Awake()
    {
        //магическое число 9,нужно будет потом задавать где-ибдуь параметром
        List<CellBase> tempCells = new();
        for (int i = 0; i < _allCells.Count; i++)
        {
            tempCells.Add(_allCells[i]);

            if ((i+1) % 9 == 0)
            {
                _cellsList.Add(tempCells);
                tempCells = new();
            }
        }
    }

    public CellBase GetCellProperties(Vector2 cellCoordinates)
    {
        if (cellCoordinates.x < _cellsList.Count && cellCoordinates.y < _cellsList[0].Count)
        {
            return _cellsList[(int)cellCoordinates.y][(int)cellCoordinates.x];
        }
        else
        {
            return _wrongCell;
        }
    }
}
