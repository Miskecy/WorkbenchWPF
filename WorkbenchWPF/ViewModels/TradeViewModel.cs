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
            Operation = new BindableCollection<OperationModel>(db.LoadData<OperationModel>("trades"));            
        }

    }
}
