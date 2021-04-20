using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Converter.Interfaces
{
    public interface IFileSystem
    {
        string GetExternalStorage(string filename);
    }
}
