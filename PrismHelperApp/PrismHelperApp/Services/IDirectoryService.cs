using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismHelperApp.Services
{
    public interface IDirectoryService
    {
        string LogFile { get; }
        string DatabaseDirectory { get; }
        string CacheDirectory { get; }
        string ImprintsDirectory { get; }
        string TemplateDirectory { get; }
        string UpdateDirectory { get; }
        string PrintQueueDirectory { get; }
        string ImprintsOnEmarkDirectory { get; }
        string ExportDirectory { get; set; }
        string CustomFontsDirectory { get; }
        string FontsThumbnailDirectory { get; }

        /// <summary>
        /// Creates a folder hierachy asynchronously or returns existing instance.
        /// </summary>
        Task<IFolder> CreateFoldersAsync(string directoryPath);

        /// <summary>
        /// Copies and creates everything found from <paramref name="sourcePath"/> over to <paramref name="destinationPath"/>, including sub-folders, files within sub-folders, and files within all sub folders.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        Task CopyFolderContentRecursiveAsync(string sourcePath, string destinationPath);
        Task<string> GetFileText(string path);
        Task WriteAllAsync(string pathToReplace, string fileText);
        Task GetAllFoldersFromPathAsync(string path);
    }
}
