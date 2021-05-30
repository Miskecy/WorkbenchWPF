using Caliburn.Micro;
using System.Collections.Generic;

namespace WorkbenchWPF.ViewModels
{  
    public class DashboardViewModel : Screen
    {
        public BindableCollection<MatrizModel> HeatMap { get; set; }

        public List<MatrizModel> coodsmatriz;

        public DashboardViewModel()
        {
            coodsmatriz = new();

            HeatMap = new BindableCollection<MatrizModel>(matriz());
        }

        public List<MatrizModel> matriz()
        {
            //List<MatrizModel> coodsmatriz = new();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    MatrizModel m = new()
                    {
                        col0 = $"[{i},{j}]",
                        col1 = $"[{i},{j}]",
                        col2 = $"[{i},{j}]",
                        col3 = $"[{i},{j}]",
                        col4 = $"[{i},{j}]"
                    };
                    coodsmatriz.Add(m);
                }
            }

            return coodsmatriz;
        }
    }

    public class MatrizModel
    {
        /*
         * [0,0] [0,1] [0,2] [0,3] [0,4]
         * [1,0] [1,1] [1,2] [1,3] [1,4]
         * [2,0] [2,1] [2,2] [2,3] [2,4]
         * [3,0] [3,1] [3,2] [3,3] [3,4]
         * [4,0] [4,1] [4,2] [4,3] [4,4]
         */
        public string col0 { get; set; }
        public string col1 { get; set; }
        public string col2 { get; set; }
        public string col3 { get; set; }
        public string col4 { get; set; }
    }
}
