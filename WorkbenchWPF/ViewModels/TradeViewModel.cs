using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using WorkbenchWPF.Helpers;
using WorkbenchWPF.Models;

namespace WorkbenchWPF.ViewModels
{
    public class TradeViewModel : Conductor<FileDetailViewModel>.Collection.AllActive
    {
        MongoCRUD db = new("Workbench");

        public string ManualComboBox { get; set; }
        public bool ManualIsWorst { get; set; }
        public string ManualContracts { get; set; }
        public string ManualOpWin { get; set; }
        public string ManualOpLoss { get; set; }
        public double ManualWinrate { get; set; }
        public string ManualWin { get; set; }
        public string ManualLoss { get; set; }
        public double ManualProfit { get; set; }

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

        private FileDetailViewModel _uploadfile;

        public FileDetailViewModel UploadFile
        {
            get { 
                return _uploadfile; 
            }
            set { 
                _uploadfile = value;
                NotifyOfPropertyChange(() => UploadFile);
            }
        }


        public TradeViewModel()
        {
            GetOperationsData();
        }        

        public void CreateOperationManual()
        {
            ManualIsWorst = ManualProfit <= 0;
            ManualWinrate = (double.Parse(ManualOpWin) / ((double.Parse(ManualOpLoss) + double.Parse(ManualOpWin)))) * 100;
            ManualProfit = double.Parse(ManualWin) - double.Parse(ManualLoss);

            OperationModel record = new()
            {
                IsWorst = ManualIsWorst,
                Date = new DateTime(2077, 07, 07, 0, 0, 0, DateTimeKind.Utc),
                Active = ManualComboBox,
                OpWin = int.Parse(ManualOpWin),
                OpLoss = int.Parse(ManualOpLoss),
                Contract = int.Parse(ManualContracts),
                WinRate = ManualWinrate,
                Win = int.Parse(ManualWin),
                Loss = int.Parse(ManualLoss),
                Profit = ManualProfit
            };

            db.CreateOne("trades", record);
            GetOperationsData();
        }

        public void GetDropedFile()
        {
            OpenFileDialog f = new() { Multiselect = true };
            bool? response = f.ShowDialog();
            if (response == true)
            {
                string[] files = f.FileNames;

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = Path.GetFileName(files[i]);
                    FileInfo fileinfo = new FileInfo(files[i]);

                    //ActivateItemAsync(new FileDetailViewModel()
                    //{
                    //    FileName = filename,
                    //    FileSize = string.Format("{0} {1}", (fileinfo.Length / 1.049e+6).ToString("0.0"), "Mb"),
                    //    UploadProgress = 100,
                    //    UploadSpeed = 600
                    //});

                    UploadFile = new FileDetailViewModel()
                    {
                        FileName = filename,
                        FileSize = string.Format("{0} {1}", (fileinfo.Length / 1.049e+6).ToString("0.0"), "Mb"),
                        UploadProgress = 100,
                        UploadSpeed = 600
                    };

                    Items.Add(UploadFile);
                }
            }
        }

        private void GetOperationsData()
        {
            Operation = new BindableCollection<OperationModel>(db.LoadData<OperationModel>("trades"));
        }

    }
}
