namespace eeegyr.Xls
{
    public sealed class TableRow : IRow
    {
        public ITable Table { get; }
        public int Row { get; }
        public int ColumnsCount => Table.ColumnsCount;

        public TableRow(ITable table, int row)
        {
            Table = table;
            Row = row;
        }

        public string this[int column]
        {
            get => Table[Row, column];
            set => Table[Row, column] = value;
        }
    }
}