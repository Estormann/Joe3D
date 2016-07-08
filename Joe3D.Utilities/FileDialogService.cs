using System;
using Microsoft.Win32;

namespace Joe3D.Utilities
{
    public class FileDialogService : IFileDialogService
    {
        public string OpenFileDialog(string initialDirectory, string defaultPath, string filter, string defaultExtension)
        {
            var d = new OpenFileDialog();
            d.InitialDirectory = initialDirectory;
            d.FileName = defaultPath;
            d.Filter = filter;
            d.DefaultExt = defaultExtension;
            if (!d.ShowDialog().Value)
            {
                return null;
            }

            return d.FileName;
        }

        public string SaveFileDialog(string initialDirectory, string defaultPath, string filter, string defaultExtension)
        {
            var d = new SaveFileDialog();
            d.InitialDirectory = initialDirectory;
            d.FileName = defaultPath;
            d.Filter = filter;
            d.DefaultExt = defaultExtension;
            if (!d.ShowDialog().Value)
            {
                return null;
            }

            return d.FileName;
        }
    }
    public interface IFileDialogService
    {
        string OpenFileDialog(string initialDirectory, string defaultPath, string filter, string defaultExtension);
        string SaveFileDialog(string initialDirectory, string defaultPath, string filter, string defaultExtension);
    }
}
