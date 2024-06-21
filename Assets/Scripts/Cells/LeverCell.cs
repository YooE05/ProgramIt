
public class LeverCell : CellBase
{
    private int _pullCount;

    private void Start()
    {
        ClearPullCount();
        IsAbleToMove = false;
    }

    public void AddPullCount()
    {
        _pullCount++;
    }

    public int GetPullCount()
    {
        return _pullCount;
    }

    public void ClearPullCount()
    {
        _pullCount = 0;
    }
}
