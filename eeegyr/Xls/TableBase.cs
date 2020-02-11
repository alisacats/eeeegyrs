using System;
using System.Collections.Generic;
using System.Linq;

namespace eeegyr.Xls
{
    public abstract class TableBase : ITable
    {
        public abstract int RowsCount { get; }
        public abstract int ColumnsCount { get; }

        private TableBase CheckRow(int row)
        {
            if (!row.IsInRightOpenSegment(0, RowsCount)) throw new ArgumentOutOfRangeException(nameof(row));
            return this;
        }

        private void CheckColumn(int column)
        {
            if (!column.IsInRightOpenSegment(0, ColumnsCount)) throw new ArgumentOutOfRangeException(nameof(column));
        }

        private TableBase CheckCell(int row, int column)
        {
            CheckRow(row);
            CheckColumn(column);
            return this;
        }

        private TableRow UnsageGet(int row) => new TableRow(this, row);

        protected abstract string UnsafeGet(int row, int column);
        protected abstract void UnsafeSet(int row, int column, string value);

        IRow ITable.this[int row] => CheckRow(row).UnsageGet(row);
        public IEnumerable<IRow> GetRows() => Enumerable.Range(0, RowsCount).Select(UnsageGet);

        string ITable.this[int row, int column]
        {
            get => CheckCell(row, column).UnsafeGet(row, column);
            set => CheckCell(row, column).UnsafeSet(row, column, value);
        }
    }
}