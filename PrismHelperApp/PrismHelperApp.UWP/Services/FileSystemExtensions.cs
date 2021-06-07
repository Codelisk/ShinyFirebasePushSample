using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismHelperApp.UWP.Services
{
    public static class FileSystemExtensions
    {
        /// <summary>
        /// Adds an extension to an IFile object to also provide a Copy function
        /// </summary>
        /// <param name="file">Source File to copy</param>
        /// <param name="destinationFolder">Destination Folder to copy the file to</param>
        /// <param name="filename">Optional new filename after copying</param>
        /// <returns></returns>
        public static async Task<IFile> CopyFileTo(this IFile file, IFolder destinationFolder, string filename = null)
        {
            var destinationFile = filename == null ? await destinationFolder.CreateFileAsync(file.Name, CreationCollisionOption.ReplaceExisting)
                : await destinationFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            using (var outFileStream = await destinationFile.OpenAsync(FileAccess.ReadAndWrite))
            using (var sourceStream = await file.OpenAsync(FileAccess.Read))
            {
                await sourceStream.CopyToAsync(outFileStream);
            }

            return destinationFile;
        }

        /// <summary>
        /// Adds an extension to a System.IO.Stream object for copying from a Stream to a file within IDirectory.
        /// </summary>
        /// <param name="sourceStream">Source stream to copy the file from</param>
        /// <param name="destinationFolder">Destination Folder to copy the file to</param>
        /// <param name="filename">Filename of the destination file</param>
        /// <returns>Returns created file</returns>
        public static async Task<IFile> CopyStreamToFile(this System.IO.Stream sourceStream, IFolder destinationFolder, string filename)
        {
            // create destination file 
            IFile destinationFile = await destinationFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            using (var destinationStream = await destinationFile.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                // write stream into destination stream
                await sourceStream.CopyToAsync(destinationStream);
            }
            return destinationFile;
        }

        public static async Task<IFile> SaveTextToFile(this string text, IFolder destinationFolder, string filename)
        {
            // create destination file 
            IFile destinationFile = await destinationFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            using (var destinationStream = await destinationFile.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                // write stream into destination stream
                byte[] textAsBytes = Encoding.ASCII.GetBytes(text);
                await destinationStream.WriteAsync(textAsBytes, 0, textAsBytes.Length);
            }
            return destinationFile;
        }
    }
}
