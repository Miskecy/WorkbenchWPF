namespace WorkbenchWPF.Models
{
    public class ImportFileModel
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public int FileProgress { get; set; }
        public string FilePath { get; set; }
        public bool IsImported { get; set; }
        public string Icon { get; set; }
    }
}
