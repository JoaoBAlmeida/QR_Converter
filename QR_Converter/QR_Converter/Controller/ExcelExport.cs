using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using QR_Converter.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QR_Converter.Controller
{
    public class ExcelExport
    {
        public ExcelExport() { }

        public async Task<string> ExportCSV(string converted)
        {
            //Separate rows created by converter
            string[] rows = converted.Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);

            //Variable to receive file path
            string path;

            //Check write file permission to continue
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            await Task.Delay(500);
            if (status != PermissionStatus.Granted)
                throw new Exception("Applicativo precisa de permissão para escrever arquivos");

            //Access File Location
            string title = "QRExport_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString()
            + DateTime.Now.Year.ToString() + ".csv";
            path = DependencyService.Get<IFileSystem>().GetExternalStorage(title);

            //Write CSV File
            if (File.Exists(path)) File.Delete(path);
            using (var streamWriter = new StreamWriter(path, true))
            {
                //Print the data in the file
                for (int x = 0; x < rows.Length; x++)
                {
                    streamWriter.WriteLine(rows[x]);
                }
            }

            return "Arquivo salvo em:\n " + path;
        }
    }
}
