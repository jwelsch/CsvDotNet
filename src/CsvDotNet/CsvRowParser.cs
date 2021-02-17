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

    public enum EolType
    {
        Unknown,
        LineFeed,
        CarriageReturn,
        CarriageReturnLineFeed
    }

    public class CsvRowParser : ICsvRowParser
    {
        private ICsvDataProvider _dataProvider;
        private EolType _eolType = EolType.Unknown;

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
            var loop = true;

            while (loop)
            {
                var c = _dataProvider.Next();

                if (c == -1)
                {
                    AddColumnToRow(row, column, true);
                    loop = false;
                }
                else if (c == '\r' || c == '\n')
                {
                    AddColumnToRow(row, column, false);

                    _eolType = HandleEol(c, _dataProvider, _eolType);

                    loop = false;
                }
                else if (c == ',')
                {
                    AddColumnToRow(row, column, false);
                }
                else
                {
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

        private static EolType HandleEol(int trigger, ICsvDataProvider dataProvider, EolType eolType)
        {
            if (eolType == EolType.Unknown)
            {
                if (trigger == '\r')
                {
                    if (dataProvider.Peek() == '\n')
                    {
                        dataProvider.Next();

                        return EolType.CarriageReturnLineFeed;
                    }

                    return EolType.CarriageReturn;
                }

                return EolType.LineFeed;
            }
            else if (eolType == EolType.CarriageReturnLineFeed)
            {
                dataProvider.Next();
            }

            return eolType;
        }
    }
}
