using PCLStorage;
using PrismHelperApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PrismHelperApp.UWP.Services
{
    public class UWPDirectoryService : IDirectoryService
    {
        public string LogFile { get; }
        public string DatabaseDirectory { get; }
        public string CacheDirectory { get; }
        public string ImprintsDirectory { get; }
        public string TemplateDirectory { get; }
        public string UpdateDirectory { get; }
        public string PrintQueueDirectory { get; }
        public string ImprintsOnEmarkDirectory { get; }
        public string ExportDirectory { get; set; }
        public string CustomFontsDirectory { get; }
        public string FontsThumbnailDirectory { get; }

        public async Task CopyFolderContentRecursiveAsync(string sourcePath, string destinationPath)
        {
            var sourceFolder = await FileSystem.Current.GetFolderFromPathAsync(sourcePath);
            var files = await sourceFolder.GetFilesAsync();
            var destinationFolder = await CreateFoldersAsync(destinationPath);
            foreach (var file in files)
            {
                await file.CopyFileTo(destinationFolder);
            }

            var folders = await sourceFolder.GetFoldersAsync();
            foreach (var folder in folders)
            {
                await CopyFolderContentRecursiveAsync(folder.Path, Path.Combine(destinationPath, folder.Name));
            }
        }
        public async Task GetAllFoldersFromPathAsync(string path)
        {
            var folders = await StorageFolder.GetFolderFromPathAsync(path);

        }
        public async Task<IFolder> CreateFoldersAsync(string directoryPath)
        {
            var folderInstance = await FileSystem.Current.GetFolderFromPathAsync(directoryPath);
            if (folderInstance == null)
            {
                directoryPath = directoryPath.TrimStart(Path.DirectorySeparatorChar);
                var folderNames = directoryPath.Split(Path.DirectorySeparatorChar);

                if (folderNames.Length > 0)
                {
                    var currentFolder = await FileSystem.Current.GetFolderFromPathAsync(folderNames[0]);

                    for (int i = 1; i < folderNames.Length; i++)
                    {
                        IFolder tempFolder = null;
                        try
                        {
                            tempFolder = await currentFolder.GetFolderAsync(folderNames[i]);
                        }
                        catch (DirectoryNotFoundException)
                        {
                            tempFolder = await currentFolder.CreateFolderAsync(folderNames[i], PCLStorage.CreationCollisionOption.OpenIfExists);
                        }

                        if (folderNames[i] == "eMark")
                        {
                            await tempFolder.CreateFileAsync(".nomedia", PCLStorage.CreationCollisionOption.OpenIfExists);
                        }

                        currentFolder = tempFolder;
                    }
                    folderInstance = currentFolder;
                }
            }
            return folderInstance;
        }

        public async Task<string> GetFileText(string path)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(path);
            string text = await Windows.Storage.FileIO.ReadTextAsync(file);
            return text;
        }

        public async Task WriteAllAsync(string pathToReplace, string fileText)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(pathToReplace);
            await Windows.Storage.FileIO.WriteTextAsync(file, fileText);
        }
    }
}