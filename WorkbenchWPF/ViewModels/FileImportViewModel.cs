using Caliburn.Micro;

namespace WorkbenchWPF.ViewModels
{
    public class FileImportViewModel : Screen
    {
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private string _fileSize;

        public string FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; }
        }

        private int _fileProgress;

        public int FileProgress
        {
            get { return _fileProgress; }
            set { _fileProgress = value; }
        }

    }
}
