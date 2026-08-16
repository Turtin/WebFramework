using System.Reflection;
using Web_Framework.logger;

namespace Web_Framework.file;

public static class Installer
{
    private static Logger logger = Logger.GetLogger();

    // Stores the list of directories that are empty but still should be created prior to startup
    private static readonly string[] Directories =
    [
        @".\logs",
        @".\page-handlers",
        @".\plugins"
    ];
    
    /// <summary>
    /// Verifies the installation of the server but ensuring all the required files and folders exist as of the time
    /// of execution
    /// </summary>
    /// <returns>Returns a boolean value where true is returned when the installation is valid and false when the
    /// installation is not valid and should be updated/fixed, it will also put a warning in the console
    /// </returns>
    public static bool VerifyInstall()
    {
        string oldName = Thread.CurrentThread.Name ?? "Unknown";
        Thread.CurrentThread.Name = "Install";

        string[] files = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        bool success = true;
        
        logger.Log(Logger.LogLevel.Info, "Validating Installation...");
        // Validate all the files
        foreach (string filePackage in files)
        {
            string filePath = GetFilePath(filePackage);
            
            if (!new FileInfo(filePath).Directory.Exists || !new FileInfo(filePath).Exists)
            {
                success = false;
                break;
            }
        }
        // Validate all the folders
        foreach (string directory in Directories)
        {
            if (!new DirectoryInfo(directory).Exists)
            {
                success = false;
                break;
            }
        }

        if (success)
        {
            logger.Log(Logger.LogLevel.Info, "Installation successfully validated and is valid.");
        }
        else
        {
            logger.Log(Logger.LogLevel.Warning, "Detecting and older or invalid file setup");
        }
        
        Thread.CurrentThread.Name = oldName;
        return success;
    }

    /// <summary>
    /// Gets a file from the internal resources folder and puts it into a given location outside the executable.
    /// </summary>
    /// <param name="source">The package path that leads to the source file that will be copied</param>
    /// <param name="destination">The file path on the outside of the executable that file will be copied into</param>
    /// <exception cref="FileNotFoundException">If the source does not exist</exception>
    private static void CopyInternalFile(string source, string destination)
    {
        using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(source) ?? throw new FileNotFoundException(source);
        using FileStream destinationStream = File.Create(destination);
        stream.CopyTo(destinationStream);
    }

    /// <summary>
    /// Gets the path of the file from the package, the webserver will copy the file structure given in the
    /// reasources folder and this method will retrieve the path that leads to it so that it can be used to
    /// deriect where the file should go and is.
    /// </summary>
    /// <param name="filePackage">The internal package path of the file</param>
    /// <returns>The file path as a string</returns>
    private static string GetFilePath(string filePackage)
    {
        string[] fileDirectories = filePackage.Replace("Web_Framework.file.resources", "").Split(".");
        string filePath = ".";
            
        foreach (string fileDirectory in fileDirectories[..^1])
        {
            filePath += @"\" +  fileDirectory;
        }

        filePath += "." + fileDirectories[^1];
        
        return filePath;
    }

    /// <summary>
    /// This method simply installs the required folders and files onto the server. It first runs a verification
    /// before attempting to update all the files that it needs to, and then finally runs another verification to ensure
    /// success.
    /// </summary>
    public static void Install()
    {
        if (VerifyInstall())
        {
            return;
        }
        
        string oldName = Thread.CurrentThread.Name ?? "Unknown";
        Thread.CurrentThread.Name = "Install";
        
        logger.Log(Logger.LogLevel.Info, "Updating files...");
        string[] files = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        // Attempt to update the required files
        foreach (string filePackage in files)
        {
            string filePath = GetFilePath(filePackage);
            Console.WriteLine(filePath);
            new FileInfo(filePath).Directory?.Create();
            CopyInternalFile(filePackage, filePath);
        }
        // Attempt to create the required folders
        foreach (string directory in Directories)
        {
            if (!new DirectoryInfo(directory).Exists)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                directoryInfo.Create();
            }
        }

        logger.Log(Logger.LogLevel.Info, "done!");
        Thread.CurrentThread.Name = oldName;
        
        Install();
    }
}