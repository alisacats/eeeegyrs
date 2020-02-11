namespace eeegyr.Xls
{
    public interface IRow
    {
        ITable Table { get; }
        int Row { get; }
        int ColumnsCount { get; }

        string this[int column] { get; set; }
    }
}