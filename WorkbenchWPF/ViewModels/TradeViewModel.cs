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
using WorkbenchWPF.Structs;

namespace WorkbenchWPF.ViewModels
{
    public class TradeViewModel : Screen
    {
        #region instances

        MongoCRUD db = new("Workbench");
        Utils util = new();

        #endregion

        #region TextBox Properties
        private string _comboBoxActive;
        private string _textBoxContracts;
        private string _textBoxOpWin;
        private string _textBoxOpLoss;
        private string _textBoxWin;
        private string _textBoxLoss;

        public string ComboBoxActive
        {
            get { return _comboBoxActive; }
            set { _comboBoxActive = value; }
        }

        public string TextBoxContracts
        {
            get 
            { 
                return _textBoxContracts; 
            }
            set
            {
                _textBoxContracts = value;
                NotifyOfPropertyChange(() => TextBoxContracts);
            }
        }

        public string TextBoxOpWin
        {
            get
            {
                return _textBoxOpWin;
            }
            set
            {
                _textBoxOpWin = value;
                NotifyOfPropertyChange(() => TextBoxOpWin);
            }
        }

        public string TextBoxOpLoss
        {
            get 
            { 
                return _textBoxOpLoss; 
            }
            set 
            { 
                _textBoxOpLoss = value; 
                NotifyOfPropertyChange(() => TextBoxOpLoss);
            }
        }

        public string TextBoxWin
        {
            get 
            { 
                return _textBoxWin; 
            }
            set 
            { 
                _textBoxWin = value; 
                NotifyOfPropertyChange(() => TextBoxWin); 
            }
        }

        public string TextBoxLoss
        {
            get { return _textBoxLoss; }
            set { _textBoxLoss = value; NotifyOfPropertyChange(() => TextBoxLoss); }
        }

        #endregion

        #region Bindables Collection

        public BindableCollection<OperationModel> _operation;
        private BindableCollection<ImportFileModel> _importFile;

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

        #endregion

        public TradeViewModel()
        {
            GetOperationsData();
        }

        #region Methods to insert data into database

        #region Manual
        public void CreateOperationManual()
        {
            decimal Profit = util.StringToDecimal(TextBoxWin) - util.StringToDecimal(TextBoxLoss);
            OperationStoreData store = new()
            {
                IsWorst = Profit <= 0,
                Active = ComboBoxActive,
                OpWin = util.StringToInteger(TextBoxOpWin),
                OpLoss = util.StringToInteger(TextBoxOpLoss),
                Winrate = util.CalcWinrate(util.StringToDecimal(TextBoxOpWin), util.StringToDecimal(TextBoxOpLoss)),
                Win = util.StringToDecimal(TextBoxWin),
                Loss = util.StringToDecimal(TextBoxLoss),
                Contracts = util.StringToInteger(TextBoxContracts),
                Profit = Profit
            };

            OperationModel record = new()
            {
                IsWorst = store.IsWorst,
                Date = DateTime.UtcNow,
                Active = store.Active,
                OpWin = store.OpWin,
                OpLoss = store.OpLoss,
                Contract = store.Contracts,
                WinRate = store.Winrate,
                Win = store.Win,
                Loss = store.Loss,
                Profit = store.Profit
            };

            db.CreateOne("trades", record);
            GetOperationsData();

            TextBoxWin = "";
            TextBoxLoss = "";
            TextBoxOpWin = "";
            TextBoxOpLoss = "";
            TextBoxContracts = "";
        }
        #endregion

        #region Automatic
        public void CreateOperationAutomatic(string filePath)
        {
            CSVHelper csvHelper = new();
            List<TrydCSVModel> csvData = csvHelper.LoadDataFile<TrydCSVModel>(filePath);
            OperationStoreData store = new() {
                IsWorst = false,
                Active = "FUTURE",
                OpWin = 0,
                OpLoss = 0,
                Winrate = 0m,
                Win = 0m,
                Loss = 0m,
                Contracts = 0,
                Profit = 0m
            };            

            foreach (var item in csvData)
            {
                decimal result = util.StringToDecimal(item.NetTotRes);
                if ( result < 0)
                {
                    store.Loss -= result;
                    store.OpLoss++;
                    
                } 
                else
                {
                    store.Win += result;                    
                    store.OpWin++;
                }
                store.Active = item.Security;
                store.Winrate = util.CalcWinrate(store.OpWin, store.OpWin);
                store.Contracts += util.StringToInteger(item.Qtty);
                store.Profit = store.Win - store.Loss;
                store.IsWorst = store.Profit <= 0;
            }

            OperationModel record = new()
            {
                IsWorst = store.IsWorst,
                Date = DateTime.UtcNow,
                Active = store.Active,
                OpWin = store.OpWin,
                OpLoss = store.OpLoss,
                Contract = store.Contracts,
                WinRate = store.Winrate,
                Win = store.Win,
                Loss = store.Loss,
                Profit = store.Profit
            };

            db.CreateOne("trades", record);
            GetOperationsData();
        }
        #endregion

        #endregion

        #region Methods to process files
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

        #endregion

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
