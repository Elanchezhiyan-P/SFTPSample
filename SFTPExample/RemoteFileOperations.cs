using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTPExample
{
    public class RemoteFileOperations
    {
        private string ftpPathSrcFolder = "/C:/Users/xxxx/xxx/CurrentFiles/";//path should end with /
        private string ftpPathDestFolder = "/C:/Users/xxxx/xxx/Processed/";//path should end with /
        private string ftpServerIP = "00.000.00.00";
        private int ftpPortNumber = 22;//change to appropriate port number
        private string ftpUserID = "xxxx";//
        private string ftpPassword = "xxxxx";
        /// <summary>
        /// Move first file from one source folder to another. 
        /// Note: Modify code and add a for loop to move all files of the folder
        /// </summary>
        public void MoveFolderToArchive()
        {
            using (SftpClient sftp = new SftpClient(ftpServerIP, ftpPortNumber, ftpUserID, ftpPassword))
            {
                sftp.Connect();
                var eachRemoteFile = sftp.ListDirectory(ftpPathSrcFolder).First();//Get first file in the Directory            
                if (eachRemoteFile.IsRegularFile)//Move only file
                {
                    bool eachFileExistsInArchive = CheckIfRemoteFileExists(sftp, ftpPathDestFolder, eachRemoteFile.Name);

                    //MoveTo will result in error if filename alredy exists in the target folder. Prevent that error by cheking if File name exists
                    string eachFileNameInArchive = eachRemoteFile.Name;

                    if (eachFileExistsInArchive)
                    {
                        eachFileNameInArchive = eachFileNameInArchive + "_" + DateTime.Now.ToString("MMddyyyy_HHmmss");//Change file name if the file already exists
                    }
                    eachRemoteFile.MoveTo(ftpPathDestFolder + eachFileNameInArchive);
                }
            }
        }

        /// <summary>
        /// Checks if Remote folder contains the given file name
        /// </summary>
        public bool CheckIfRemoteFileExists(SftpClient sftpClient, string remoteFolderName, string remotefileName)
        {
            bool isFileExists = sftpClient
                                .ListDirectory(remoteFolderName)
                                .Any(
                                        f => f.IsRegularFile &&
                                        f.Name.ToLower() == remotefileName.ToLower()
                                    );
            return isFileExists;
        }

    }
}
