using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkbenchWPF.Models
{
    public class TrydCSVModel
    {
        public string Security { get; set; }
        public string Open { get; set; }
        public string Close { get; set; }
        public string Time { get; set; }
        public string Qtty { get; set; }
        public string Balance { get; set; }
        public string BS { get; set; }
        public string BuyAverPrice { get; set; }
        public string SellAverPrice { get; set; }
        public string OpenResult { get; set; }
        public string ClosedResult { get; set; }
        public string TotalResult { get; set; }
        public DateTime Date { get; set; }
        public string Costs { get; set; }
        public string NetTotRes { get; set; }
        public string ResultBalance { get; set; }
        public string TBT { get; set; }
        public string MPE { get; set; }
        public string MNE { get; set; }

        public override string ToString()
        {
            string str = $"{Security} - {Open} - {Close} - {Time}\n";
            return str;
        }
    }
}
