namespace SFTPExample
{
    internal class Program
    {
        static void Main(string[] args)
        {

            RemoteFileOperations remote = new();
            remote.MoveFolderToArchive();

            Console.WriteLine("Process has been completed!");
            Console.ReadKey();
        }
    }
}
