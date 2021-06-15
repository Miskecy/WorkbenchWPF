using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkbenchWPF.Helpers
{
    public class TrydTradesCSV
    {
        public string Papel { get; set; }
        public string Abertura { get; set; }
        public string Fechamento { get; set; }
        public string Tempo { get; set; }

        private string[] SplitLines(string[] csvLines)
        {
            string[] rowParse = csvLines;
            for (int i = 0 ; i < csvLines.Length ; i++)
            {
                rowParse = csvLines[i].Split(',');                
            }
            return rowParse;
        }

        public List<TrydTradesCSV> MakeObj(string filePath)
        {
            string[] csvLines = System.IO.File.ReadAllLines(filePath);
            string[] rowData = SplitLines(csvLines);
            var trades = new List<TrydTradesCSV>();

            for (int i = 0; i < rowData.Length; i++)
            {
                TrydTradesCSV row = new()
                {
                    Abertura = rowData[0],
                    Fechamento = rowData[1],
                    Papel = rowData[2],
                    Tempo = rowData[3]
                };

                trades.Add(row);
            }

            return trades;
        }

        public override string ToString()
        {
            string str = $"Papel:{Papel}, Abertura:{Abertura}, Fechamento:{Fechamento}, Tempo:{Tempo}";
            return str;
        }
    }
}
