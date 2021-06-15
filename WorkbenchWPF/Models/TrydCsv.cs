using CsvHelper.Configuration.Attributes;

namespace WorkbenchWPF.Models
{
    public class TrydCsv
    {
        [Name("Papel")]
        public string Papel { get; set; }
        [Name("Abertura")]
        public string Abertura { get; set; }
        [Name("Fechamento")]
        public string Fechamento { get; set; }
        [Name("Tempo")]
        public string Tempo { get; set; }
        //[Name("Qtd")]
        //public string Qtd { get; set; }
        //[Name("Saldo")]
        //public string Saldo { get; set; }
        //[Name("C/V")]
        //public string CV { get; set; }
        //[Name("Prc Medio Cpa")]
        //public string PrcMedioCpa { get; set; }
        //[Name("Prc Medio Vda")]
        //public string PrcMedioVda { get; set; }
        //[Name("Result Aber")]
        //public string ResultAber { get; set; }
        //[Name("Result Fech")]
        //public string ResultFech { get; set; }
        //[Name("Result Total")]
        //public string ResultTotal { get; set; }
        //[Name("Data")]
        //public string Data { get; set; }
        //[Name("Custos")]
        //public string Custos { get; set; }
        //[Name("Res. Tot. Liq.")]
        //public string ResTotLiq { get; set; }
        //[Name("Saldo Result.")]
        //public string SaldoResult { get; set; }
        //[Name("TET")]
        //public string TET { get; set; }
        //[Name("MEP")]
        //public string MEP { get; set; }
        //[Name("MEN")]
        //public string MEN { get; set; }
    }
}
