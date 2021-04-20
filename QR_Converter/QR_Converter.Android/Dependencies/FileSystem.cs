using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QR_Converter.Droid.Dependencies;
using QR_Converter.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(FileSystem))]
namespace QR_Converter.Droid.Dependencies
{
    public class FileSystem : IFileSystem
    {
        public string GetExternalStorage(string filename)
        {
            //Get the desired external storage
            Java.IO.File root = global::Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            string path = Path.Combine(root.ToString(), filename);
            return path;
        }
    }
}