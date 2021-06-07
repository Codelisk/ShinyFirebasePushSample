using Prism.Commands;
using Prism.Magician;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PrismHelperApp.ViewModels
{
    public partial class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Path = @"C:\Users\Daniel\source\repos\sybos-app";
        }
        private DelegateCommand _changeFilesCommand;
        public DelegateCommand ChangeFilesCommand =>
            _changeFilesCommand ?? (_changeFilesCommand = new DelegateCommand(ExecuteChangeFilesCommand));
        [Reactive]
        public string Path { get; set; }
        void ExecuteChangeFilesCommand()
        {
            try
            {
                var searchText = "[Reactive]";
                var directories=Directory.GetDirectories(Path);
                List<FileInfo> allFiles = new List<FileInfo>();
                foreach (var directory in directories)
                {
                    var di = new DirectoryInfo(directory);
                    allFiles.AddRange(di.GetFiles());
                }
                foreach (var file in allFiles)
                {
                    var text=File.ReadAllText(file.FullName);
                    while (text.Contains(searchText))
                    {
                        var startIndex=text.IndexOf(searchText);
                        var endIndex=text.IndexOf("}", startIndex + 1);
                        var firstSpaceIndex = text.IndexOf(" ", startIndex);
                        var secondSpaceIndex = text.IndexOf(" ", firstSpaceIndex + 1);
                        var thirdSpaceIndex= text.IndexOf(" ", secondSpaceIndex + 1);
                        var name = text.Substring(secondSpaceIndex + 1, thirdSpaceIndex - secondSpaceIndex - 1);
                        text = "";
                    }
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}
