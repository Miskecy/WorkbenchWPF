using Caliburn.Micro;
using System.Collections.Generic;
using WorkbenchWPF.Helpers;
using WorkbenchWPF.Models;

namespace WorkbenchWPF.ViewModels
{
    public class TradeViewModel : Screen
    {
        MongoCRUD db = new("Workbench");

        public BindableCollection<OperationModel> Operation { get; set; }

        public TradeViewModel()
        {
            //var trades = db.LoadData<OperationModel>("trades");
            //bool status;
            //int profit;

            //foreach (var trade in trades)
            //{
            //    profit = trade.Win - trade.Loss;

            //    if (profit >= 0)
            //    {
            //        status = false;
            //    } else
            //    {
            //        status = true;
            //    }
            //}

            Operation = new BindableCollection<OperationModel>(db.LoadData<OperationModel>("trades"));            
        }

    }
}
