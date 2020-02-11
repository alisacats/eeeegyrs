using System;
using System.IO;
using NPOI.SS.UserModel;

namespace eeegyr.Xls
{
    public sealed class XlsTable : TableBase
    {
        private readonly IWorkbook _workbook;
        private readonly ISheet _sheet;
        public override int RowsCount { get; }
        public override int ColumnsCount { get; }

        private XlsTable(IWorkbook workbook)
        {
            _workbook = workbook;
            _sheet = _workbook.GetSheetAt(0);
            RowsCount = _sheet.LastRowNum + 1;
            ColumnsCount = RowsCount == 0 ? 0 : _sheet.GetRow(0).LastCellNum;
        }

        public static XlsTable Load(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            var workbook = WorkbookFactory.Create(stream);
            return new XlsTable(workbook);
        }

        public static XlsTable LoadFromFile(string filePath)
        {
            using (var fs = File.OpenRead(filePath)) return Load(fs);
        }

        public static void Export(XlsTable table, Stream stream)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            table._workbook.Write(stream);
        }

        private NPOI.SS.UserModel.IRow UnsafeGetOrCreateRow(int row) => _sheet.GetRow(row) ?? _sheet.CreateRow(row);
        private static ICell UnsafeGetOrCreateCell(NPOI.SS.UserModel.IRow row, int column) => row.GetCell(column) ?? row.CreateCell(column);
        private ICell UnsafeGetOrCreateCell(int row, int column) => UnsafeGetOrCreateCell(UnsafeGetOrCreateRow(row), column);

        protected override string UnsafeGet(int row, int column) => _sheet.GetRow(row)?.GetCell(column)?.ToString();
        protected override void UnsafeSet(int row, int column, string value) => UnsafeGetOrCreateCell(row, column).SetCellValue(value);
    }
}