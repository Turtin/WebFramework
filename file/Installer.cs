using System.Reflection;
using Web_Framework.logger;

namespace Web_Framework.file;

public static class Installer
{
    private static Logger logger = Logger.GetLogger();
    public static bool VerifyInstall()
    {
        string oldName = Thread.CurrentThread.Name ?? "Unknown";
        Thread.CurrentThread.Name = "Install";
        
        logger.Log(Logger.LogLevel.Info, "Validating Installation...");
        
        if (File.Exists(@".\server.cfg") &&
            Directory.Exists(@".\plugins") &&
            Directory.Exists(@".\page-handlers"))
        {
            logger.Log(Logger.LogLevel.Info, "Installation successfully validated and is valid.");
            Thread.CurrentThread.Name = oldName;
            return true;
        }

        logger.Log(Logger.LogLevel.Warning, "Detecting and older or invalid file setup");
        Thread.CurrentThread.Name = oldName;
        return false;
    }

    private static void CopyInternalFile(string source, string destination)
    {
        using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(source) ?? throw new FileNotFoundException(source);
        using FileStream destinationStream = File.Create(destination);
        stream.CopyTo(destinationStream);
    }

    public static void Install()
    {
        if (VerifyInstall())
        {
            return;
        }
        
        string oldName = Thread.CurrentThread.Name ?? "Unknown";
        Thread.CurrentThread.Name = "Install";
        
        logger.Log(Logger.LogLevel.Info, "Updating files...");
        
        if (!File.Exists(@".\server.cfg")) {
            logger.Log(Logger.LogLevel.Info, "Creating server.cfg...");
            CopyInternalFile("Web_Framework.file.resources.server.cfg", @".\server.cfg");
        }

        if (!Directory.Exists(@".\plugins"))
        {
            logger.Log(Logger.LogLevel.Info, "Creating plugin directory...");
            Directory.CreateDirectory(@".\plugins");
        }

        if (!Directory.Exists(@".\page-handlers"))
        {
            logger.Log(Logger.LogLevel.Info, "Creating handler directory...");
            Directory.CreateDirectory(@".\page-handlers");
        }

        logger.Log(Logger.LogLevel.Info, "done!");
        Thread.CurrentThread.Name = oldName;
        
        Install();
    }
}