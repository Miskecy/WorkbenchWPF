using Caliburn.Micro;
using System.Collections.Generic;
using WorkbenchWPF.Helpers;
using WorkbenchWPF.Models;

namespace WorkbenchWPF.ViewModels
{  
    public class DashboardViewModel : Screen
    {
        MongoCRUD db = new("Workbench");

        public BindableCollection<Coods> HeatMap { get; set; }

        public DashboardViewModel()
        {
            HeatMap = new BindableCollection<Coods>();
        }       

        public Coods matriz()
        {
            Coods c = new();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    c.var1 = new string[3]{ $"[{i},{j}]", $"[{i},{j}]", $"[{i},{j}]" };         
                }
            }

            return c;
        }
    }

    public class Coods
    {
        public string[] var1 { get; set; }
    }
}
