using System;
using System.Collections.Generic;
using System.Text;

namespace CsvDotNet
{
    public interface ICsvRowParser
    {
        void Initialize(ICsvDataProvider dataProvider);

        string[] GetNextRow();
    }

    public enum LineBreakType
    {
        Unknown,
        LineFeed,
        CarriageReturn,
        CarriageReturnLineFeed
    }

    public class CsvRowParser : ICsvRowParser
    {
        private const int EndOfLine = -1;
        private const int CarriageReturn = '\r';
        private const int NewLine = '\n';
        private const int Escape = '\\';
        private const int ColumnDelimiter = ',';

        private ICsvDataProvider _dataProvider;
        private LineBreakType _lineBreakType = LineBreakType.Unknown;

        public void Initialize(ICsvDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public string[] GetNextRow()
        {
            if (_dataProvider == null)
            {
                throw new InvalidOperationException($"Must call '{nameof(Initialize)}' method before calling '{nameof(GetNextRow)}' method.");
            }

            var row = new List<string>();
            var column = new StringBuilder();
            var escaped = false;
            var loop = true;

            while (loop)
            {
                var c = _dataProvider.Next();

                if (c == EndOfLine)
                {
                    if (escaped)
                    {
                        throw new CsvException($"Incomplete escape sequence detected at end of file.");
                    }

                    AddColumnToRow(row, column, true);

                    loop = false;
                }
                else if (c == CarriageReturn || c == NewLine)
                {
                    if (escaped)
                    {
                        throw new CsvException($"Incomplete escape sequence detected at end of line.");
                    }

                    AddColumnToRow(row, column, false);

                    _lineBreakType = HandleLineBreak(c, _dataProvider, _lineBreakType);

                    loop = false;
                }
                else if (c == Escape)
                {
                    if (escaped)
                    {
                        column.Append((char)c);
                        escaped = false;
                        continue;
                    }

                    escaped = true;
                }
                else if (c == ColumnDelimiter)
                {
                    if (escaped)
                    {
                        column.Append((char)c);
                        escaped = false;
                        continue;
                    }

                    AddColumnToRow(row, column, false);
                }
                else
                {
                    if (escaped)
                    {
                        throw new CsvException($"Unknown escape sequence '\\{(char)c}'");
                    }

                    column.Append((char)c);
                }
            }

            return row.ToArray();
        }

        private static void AddColumnToRow(List<string> row, StringBuilder column, bool isEof)
        {
            // Prevents an empty, zero length row, if the file has a trailing new line.
            if (row.Count != 0 || column.Length != 0 || !isEof)
            {
                row.Add(column.ToString());
                column.Clear();
            }
        }

        private static LineBreakType HandleLineBreak(int trigger, ICsvDataProvider dataProvider, LineBreakType lineBreakType)
        {
            if (lineBreakType == LineBreakType.Unknown)
            {
                if (trigger == CarriageReturn)
                {
                    if (dataProvider.Peek() == NewLine)
                    {
                        dataProvider.Next();

                        return LineBreakType.CarriageReturnLineFeed;
                    }

                    return LineBreakType.CarriageReturn;
                }

                return LineBreakType.LineFeed;
            }
            else if (lineBreakType == LineBreakType.CarriageReturnLineFeed)
            {
                dataProvider.Next();
            }

            return lineBreakType;
        }
    }
}
