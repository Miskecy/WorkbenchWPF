using Caliburn.Micro;

namespace WorkbenchWPF.ViewModels
{
    public class FileUploadViewModel : Screen
    {
        private string _filename;
        
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        private string _filesize;

        public string FileSize
        {
            get { return _filesize; }
            set { _filesize = value; }
        }


        private int _uploadProgress;

        public int UploadProgress
        {
            get { return _uploadProgress; }
            set { _uploadProgress = value; }
        }

        private int _uploadspeed;

        public int UploadSpeed
        {
            get { return _uploadspeed; }
            set { _uploadspeed = value; }
        }

        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }
    }
}
