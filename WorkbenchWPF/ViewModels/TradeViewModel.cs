using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using WorkbenchWPF.Helpers;
using WorkbenchWPF.Models;
using WorkbenchWPF.Trades;

namespace WorkbenchWPF.ViewModels
{
    public class TradeViewModel : Screen
    {
        MongoCRUD db = new("Workbench");


        #region TextBox Properties

        private string _comboBoxActive;
        private string _textBoxContracts;
        private string _textBoxOpWin;
        private string _textBoxOpLoss;
        private string _textBoxWin;
        private string _textBoxLoss;

        public string ComboBoxActive
        {
            get 
            { 
                return _comboBoxActive; 
            }
            set 
            { 
                _comboBoxActive = value; 
            }
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

        private BindableCollection<OperationModel> _operation;
        private BindableCollection<ImportFileModel> _importFile;
        private List<ImportFileModel> _listImports;

        public BindableCollection<OperationModel> Operation
        {
            get { return _operation; }
            set
            {
                _operation = value;
                NotifyOfPropertyChange(() => Operation);
            }
        }        

        public List<ImportFileModel> ListImports
        {
            get 
            { 
                return _listImports; 
            }
            set 
            { 
                _listImports = value;
                NotifyOfPropertyChange(() => ListImports);
            }
        }

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
            ListImports = new();
        }

        #region Methods to insert data into database

        #region Manual

        private bool CanCreateOperationManual(string textBoxWin) => !string.IsNullOrWhiteSpace(textBoxWin);

        public void CreateOperationManual(string textBoxWin)
        {
            //temp check is whiteornull
            if (string.IsNullOrWhiteSpace(ComboBoxActive) 
                || string.IsNullOrWhiteSpace(TextBoxWin) 
                || string.IsNullOrWhiteSpace(TextBoxLoss)
                || string.IsNullOrWhiteSpace(TextBoxOpWin)
                || string.IsNullOrWhiteSpace(TextBoxOpLoss)
                || string.IsNullOrWhiteSpace(TextBoxContracts)) return;

            db.CreateOne("trades", Trade.Create(ComboBoxActive, DateTime.UtcNow, TextBoxOpWin, TextBoxOpLoss, TextBoxWin, TextBoxLoss, TextBoxContracts));
            GetOperationsData();

            TextBoxWin = "";
            TextBoxLoss = "";
            TextBoxOpWin = "";
            TextBoxOpLoss = "";
            TextBoxContracts = "";
        }
        #endregion

        #region Automatic
        public bool CreateOperationAutomatic(string filePath)
        {
            CSVHelper csvHelper = new();
            List<TrydCSVModel> csvData = csvHelper.LoadDataFile<TrydCSVModel>(filePath);

            foreach (var item in csvData)
            {
                if (string.IsNullOrWhiteSpace(item.Security) || string.IsNullOrWhiteSpace(item.Open)) return false;
            }

            decimal Loss, OpLoss, Win, OpWin;
            Loss = OpLoss = Win = OpWin = 0m;
            int Contracts = 0;
            string Active = string.Empty;

            foreach (var item in csvData)
            {
                decimal result = Utils.StringToDecimal(item.NetTotRes);
                Debug.WriteLine(result);
                if (result < 0)
                {
                    Loss -= result;
                    OpLoss++;
                }
                else
                {
                    Win += result;
                    OpWin++;
                }
                Active = item.Security;
                Contracts += Utils.StringToInteger(item.Qtty);
            }

            return db.CreateOne("trades", 
                Trade.Create(
                    Active, 
                    DateTime.UtcNow, 
                    OpWin.ToString(), 
                    OpLoss.ToString(), 
                    Win.ToString(), 
                    Loss.ToString(), 
                    Contracts.ToString()
                    ));         
        }
        #endregion

        #endregion

        #region Methods to process files
        public void GetFileByClick()
        {
            ListImports.RemoveAll(x => x.IsImported);

            OpenFileDialog f = new() { Multiselect = true, Filter = "CSV | *.csv" };
            bool? response = f.ShowDialog();
            if (!response == true) return;

            string[] files = f.FileNames;

            foreach (var file in files)
            {
                string filename = Path.GetFileName(file);
                FileInfo fileinfo = new FileInfo(file);

                ImportFileModel fileSelected = new()
                {
                    FileName = filename,
                    FileSize = string.Format("{0} {1}", (fileinfo.Length / 1.049e+6).ToString("0.0"), "Mb"),
                    FileProgress = 0,
                    FilePath = fileinfo.ToString(),
                    IsImported = false,
                    Icon = "../Assets/Icons/delete_50px_Red.png"
                };

                ListImports.Add(fileSelected);
            }
            ImportFile = new BindableCollection<ImportFileModel>(ListImports);            
        }

        public void GetFileByDrop(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            ListImports.RemoveAll(x => x.IsImported);

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach(var file in files)
            {
                var ext = Path.GetExtension(file);
                if (ext.Equals(".csv", StringComparison.CurrentCultureIgnoreCase))
                {
                    string filename = Path.GetFileName(file);
                    FileInfo fileinfo = new FileInfo(file);

                    ImportFileModel fileSelected = new()
                    {
                        FileName = filename,
                        FileSize = string.Format("{0} {1}", (fileinfo.Length / 1.049e+6).ToString("0.0"), "Mb"),
                        FileProgress = 0,
                        FilePath = fileinfo.ToString(),
                        IsImported = false
                    };

                    ListImports.Add(fileSelected);
                }
            }

            ImportFile = new BindableCollection<ImportFileModel>(ListImports);
        }

        public void GetDragOverFile(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file);
                if (ext.Equals(".csv", StringComparison.CurrentCultureIgnoreCase))
                {
                    Debug.WriteLine("CSV");
                }
            }
        }

        public void GetCsvData()
        {
            ListImports.RemoveAll(x => x.IsImported);

            if (ListImports.Count > 0)
            {
                foreach (var item in ListImports)
                {
                    if (CreateOperationAutomatic(item.FilePath))
                    {
                        item.FileProgress = 100;
                        item.IsImported = true;
                        GetImportFileList();
                        GetOperationsData();
                        item.Icon = "../Assets/Icons/checkmark_50px.png";
                    } 
                    else
                    {
                        item.FileProgress = 100;
                        item.IsImported = true;
                        GetImportFileList();
                        item.Icon = "../Assets/Icons/cancel_50px_Red.png";
                    }
                }
            }
        }

        #endregion

        private void GetImportFileList()
        {
            ImportFile = new BindableCollection<ImportFileModel>(ListImports);
        }

        private void GetOperationsData()
        {
            Operation = new BindableCollection<OperationModel>(db.LoadData<OperationModel>("trades"));
        }

    }
}
