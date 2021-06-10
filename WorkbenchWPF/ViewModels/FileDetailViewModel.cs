using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkbenchWPF.ViewModels
{
    public class FileDetailViewModel : Screen
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

    }
}
