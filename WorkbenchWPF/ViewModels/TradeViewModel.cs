using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using WorkbenchWPF.Helpers;
using WorkbenchWPF.Models;

namespace WorkbenchWPF.ViewModels
{
    public class TradeViewModel : Screen
    {
        MongoCRUD db = new("Workbench");
        Utils util = new();

        public string ManualComboBox { get; set; }
        public bool IsWorst { get; set; }
        public string Contracts { get; set; }
        public string OpWin { get; set; }
        public string OpLoss { get; set; }
        public decimal Winrate { get; set; }
        public string Win { get; set; }
        public string Loss { get; set; }
        public decimal Profit { get; set; }

        public BindableCollection<OperationModel> _operation;
        public BindableCollection<OperationModel> Operation
        {
            get { return _operation; }
            set
            {
                _operation = value;
                NotifyOfPropertyChange(() => Operation);
            }
        }

        public List<ImportFileModel> ListImports { get; set; }

        private BindableCollection<ImportFileModel> _importFile;

        public BindableCollection<ImportFileModel> ImportFile
        {
            get 
            { 
                return _importFile;
            }
            set 
            { 
                _importFile = value;
                NotifyOfPropertyChange(() => ImportFile);
            }
        }

        public TradeViewModel()
        {
            GetOperationsData();
        }        

        public void CreateOperationManual()
        {
            IsWorst = Profit <= 0;
            Winrate = (util.StringToDecimal(OpWin) / ((util.StringToDecimal(OpLoss) + util.StringToDecimal(OpWin)))) * 100;
            Profit = util.StringToDecimal(OpWin) - util.StringToDecimal(OpLoss);

            OperationModel record = new()
            {
                IsWorst = IsWorst,
                Date = new DateTime(2077, 07, 07, 0, 0, 0, DateTimeKind.Utc),
                Active = ManualComboBox,
                OpWin = int.Parse(OpWin),
                OpLoss = int.Parse(OpLoss),
                Contract = int.Parse(Contracts),
                WinRate = Winrate,
                Win = int.Parse(Win),
                Loss = int.Parse(Loss),
                Profit = Profit
            };

            db.CreateOne("trades", record);
            GetOperationsData();
        }

        public void CreateOperationAutomatic(string filePath)
        {
            CSVHelper csvHelper = new();
            List<TrydCSVModel> csvData = csvHelper.LoadDataFile<TrydCSVModel>(filePath);

            int opWin = 0;
            int opLoss = 0;
            decimal win = 0;
            decimal loss = 0;
            int contracts = 0;
            string active = "";
            bool isWorst = false;
            decimal winrate = 0;
            decimal profit = 0;

            foreach (var item in csvData)
            {
                decimal result = util.StringToDecimal(item.NetTotRes);
                Debug.WriteLine(result);
                if ( result < 0)
                {
                    loss -= result;
                    opLoss++;
                    
                } 
                else
                {
                    win += result;                    
                    opWin++;
                }
                active = item.Security;
                winrate = util.CalcWinrate(opWin, opLoss);
                contracts += util.StringToInteger(item.Qtty);
                profit = win - loss;
                isWorst = profit <= 0;
            }

            OperationModel record = new()
            {
                IsWorst = isWorst,
                Date = DateTime.UtcNow,
                Active = active,
                OpWin = opWin,
                OpLoss = opLoss,
                Contract = contracts,
                WinRate = winrate,
                Win = win,
                Loss = loss,
                Profit = profit
            };

            Debug.WriteLine(record);

            db.CreateOne("trades", record);
            GetOperationsData();
        }

        public void GetDropedFileByClick()
        {
            ListImports = new List<ImportFileModel>();
            OpenFileDialog f = new() { Multiselect = true };
            bool? response = f.ShowDialog();
            if (response == true)
            {
                string[] files = f.FileNames;

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = Path.GetFileName(files[i]);
                    FileInfo fileinfo = new FileInfo(files[i]);

                    ImportFileModel fileSelected = new()
                    {
                        FileName = filename,
                        FileSize = string.Format("{0} {1}", (fileinfo.Length / 1.049e+6).ToString("0.0"), "Mb")
                    };

                    ListImports.Add(fileSelected);
                }
            }

            ImportFile = new BindableCollection<ImportFileModel>(ListImports);
        }

        public void GetDropedFileByDrop()
        {
            MessageBox.Show(string.Format("Hello!"));
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //    ListImports = new List<ImportFileModel>();
            //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            //    for (int i = 0; i < files.Length; i++)
            //    {
            //        string filename = Path.GetFileName(files[i]);
            //        FileInfo fileinfo = new FileInfo(files[i]);

            //        ImportFileModel fileSelected = new()
            //        {
            //            FileName = filename,
            //            FileSize = string.Format("{0} {1}", (fileinfo.Length / 1.049e+6).ToString("0.0"), "Mb")
            //        };

            //        ListImports.Add(fileSelected);
            //    }

            //    ImportFile = new BindableCollection<ImportFileModel>(ListImports);
            //}
        }

        public void GetCsvData()
        {            
            CSVHelper csvHelper = new();

            OpenFileDialog f = new() { Multiselect = false };
            bool? response = f.ShowDialog();
            if (response == true)
            {
                string[] files = f.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(files[i]);

                    //List<TrydCSVModel> results = csvHelper.LoadDataFile<TrydCSVModel>(fileInfo.ToString());

                    //foreach(var item in results)
                    //{
                    //    Debug.Write(item);
                    //}

                    CreateOperationAutomatic(fileInfo.ToString());
                }
            }          
        }

        private void GetOperationsData()
        {
            Operation = new BindableCollection<OperationModel>(db.LoadData<OperationModel>("trades"));
        }

    }
}
