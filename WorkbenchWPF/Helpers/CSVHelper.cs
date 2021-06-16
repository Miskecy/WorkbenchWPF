using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WorkbenchWPF.Helpers
{
    public class CSVHelper
    {
        public List<T> LoadDataFile<T>(string filePath) where T : class, new()
        {
            var lines = System.IO.File.ReadAllLines(filePath).ToList();
            List<T> output = new();
            T entry = new();
            var cols = entry.GetType().GetProperties();

            if (lines.Count < 2)
            {
                throw new IndexOutOfRangeException("The file was either empty or missing.");
            }

            var headers = lines[0].Split(',');

            lines.RemoveAt(0);

            foreach (var line in lines)
            {
                entry = new T();
                var vals = line.Split(',');

                if(!string.IsNullOrEmpty(line))
                {
                    for (int i = 0; i < headers.Length; i++)
                    {
                        foreach (var col in cols)
                        {
                            try
                            {
                                string headerName = Regex.Replace(headers[i], @"[^\w\d]", string.Empty);

                                if (col.Name == headerName)
                                {
                                    col.SetValue(entry, Convert.ChangeType(vals[i], col.PropertyType));
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    output.Add(entry);
                }               
                
            }
            return output;
        }

        public void SaveDataFile<T>(List<T> data, string filePath) where T : class
        {
            List<string> lines = new();
            StringBuilder line = new();

            if(data == null || data.Count == 0)
            {
                throw new ArgumentNullException("Data", "You must populate the data parameter with at least one item.");
            }
            var cols = data[0].GetType().GetProperties();

            foreach(var col in cols)
            {
                line.Append(col.Name);
                line.Append(',');
            }

            lines.Add(line.ToString().Substring(0, line.Length - 1));

            foreach(var row in data)
            {
                line = new StringBuilder();

                foreach(var col in cols)
                {
                    line.Append(col.GetValue(row));
                    line.Append(',');
                }

                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }
            System.IO.File.WriteAllLines(filePath, lines);
        }
    }
}
