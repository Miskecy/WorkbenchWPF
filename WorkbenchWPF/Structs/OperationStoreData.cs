namespace WorkbenchWPF.Structs
{
    public struct OperationStoreData
    {
        public bool IsWorst { get; set; } 
        public string Active { get; set; }
        public int OpWin { get; set; }
        public int OpLoss { get; set; }
        public decimal Winrate { get; set; }
        public int Contracts { get; set; }
        public decimal Win { get; set; }
        public decimal Loss { get; set; }
        public decimal Profit { get; set; }        
    }
}
