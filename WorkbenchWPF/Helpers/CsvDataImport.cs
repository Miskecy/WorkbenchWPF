using CsvHelper;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WorkbenchWPF.Helpers
{
    public class CsvDataImport
    {        
        public List<T> GetCsvData<T>(string filePath)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    var records = csvReader.GetRecords<T>().ToList();
                    return records;
                }
            }
        }
    }
}
