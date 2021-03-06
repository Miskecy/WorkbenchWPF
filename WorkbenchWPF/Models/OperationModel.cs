using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WorkbenchWPF.Models
{
    public class OperationModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public bool IsWorst { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Active { get; set; }
        public int OpWin { get; set; }
        public int OpLoss { get; set; }
        public int Contracts { get; set; }
        public decimal WinRate { get; set; }
        public decimal Win { get; set; }
        public decimal Loss { get; set; }
        public decimal Profit { get; set; }

        public override string ToString()
        {
            string str = $"{Id}\n{IsWorst}\n{Date}\n{Active}\n{OpWin}\n{OpLoss}\n{Contracts}\n{WinRate}\n{Win}\n{Loss}\n{Profit}";
            return str;
        }
    }
}
