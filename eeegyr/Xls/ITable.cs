using System.Collections.Generic;

namespace eeegyr.Xls
{
    public interface ITable
    {
        int RowsCount { get; }
        int ColumnsCount { get; }

        IEnumerable<IRow> GetRows();

        string this[int row, int column] { get; set; }
        IRow this[int row] { get; }
    }
}