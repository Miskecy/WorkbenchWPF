using System;
using WorkbenchWPF.Helpers;
using WorkbenchWPF.Models;

namespace WorkbenchWPF.Trades
{
    public class Trade
    {
        public static OperationModel Create(string Active, DateTime Date, string OpWin, string OpLoss, string Win, string Loss, string Contracts)
        {
            decimal Profit = Utils.StringToDecimal(Win) - Utils.StringToDecimal(Loss);
            bool IsWorst = Profit < 0;

            OperationModel output = new()
            {
                IsWorst = IsWorst,
                Active = Active,
                Date = Date,
                OpWin = Utils.StringToInteger(OpWin),
                OpLoss = Utils.StringToInteger(OpLoss),
                WinRate = Utils.CalcWinrate(Utils.StringToDecimal(OpWin), Utils.StringToDecimal(OpLoss)),
                Win = Utils.StringToDecimal(Win),
                Loss = Utils.StringToDecimal(Loss),
                Contracts = Utils.StringToInteger(Contracts),
                Profit = Profit
            };

            return output;
        }
    }
}
