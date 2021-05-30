using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WorkbenchWPF.Models
{
    public class OperationModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public bool IsWorst { get; set; }
        public DateTime Date { get; set; }
        public string Active { get; set; }
        public int OpWin { get; set; }
        public int OpLoss { get; set; }
        public int Contract { get; set; }
        public double WinRate { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public double Profit { get; set; }
    }
}
