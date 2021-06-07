using Prism.Mvvm;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WpfPrismFileAdjust.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
            : base()
        {
            Path = @"C:\Users\Daniel\source\repos\sybos-app";
        }
        private DelegateCommand _changeFilesCommand;
        public DelegateCommand ChangeFilesCommand =>
            _changeFilesCommand ?? (_changeFilesCommand = new DelegateCommand(ExecuteChangeFilesCommand));
        private string path;
        public string Path
        {
            get { return path; }
            set { SetProperty(ref path, value); }
        }
        List<string> all = new List<string>();
        private List<string> LoadSubDirs(string dir)

        {

            Console.WriteLine(dir);

            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            foreach (string subdirectory in subdirectoryEntries)

            {
                all.Add(subdirectory);
                LoadSubDirs(subdirectory);

            }
            return all;
        }
        void ExecuteChangeFilesCommand()
        {
            try
            {
                string searchText = "[Reactive]";
                var allFiles = GetAllFiles();
                foreach (var file in allFiles)
                {
                    var text = File.ReadAllText(file.FullName);
                    bool contained = text.Contains(searchText);
                    while (text.Contains(searchText))
                    {
                        var startIndex = text.IndexOf(searchText);
                        var endIndex = text.IndexOf("}", startIndex + 1);
                        var firstSpaceIndex = text.IndexOf(" p", startIndex);
                        var secondSpaceIndex = text.IndexOf(" ", firstSpaceIndex + 1);
                        var thirdSpaceIndex = text.IndexOf(" ", secondSpaceIndex + 1);
                        var fourthSpaceIndex = text.IndexOf(" ", thirdSpaceIndex + 1);
                        var name = text.Substring(thirdSpaceIndex + 1, fourthSpaceIndex - thirdSpaceIndex - 1);
                        var varType = text.Substring(secondSpaceIndex + 1, thirdSpaceIndex - secondSpaceIndex - 1);
                        name = name.Replace("\n", "");
                        name = name.Replace("\r", "");
                        var name2 = name;
                        name= char.ToLower(name[0]) + name.Substring(1);
                        string reactiveProp = text.Substring(startIndex, endIndex - startIndex+1);
                        reactiveProp = reactiveProp.Replace("[Reactive]", "[Reactive1]");
                        string x = "        private " + varType + " _" + name + ";"+ Environment.NewLine +"        public " + varType + " " + name2 + " " + Environment.NewLine + "        {" + Environment.NewLine + "           get { return _" + name + " ; }" + Environment.NewLine + "           set { this.RaiseAndSetIfChanged(ref _" + name + " , value); " + Environment.NewLine + "        }}";
                        string newText = "/*ReactiveStartMark " + reactiveProp + "*/"+ Environment.NewLine + x + "//ReactiveEndMark";
                        text=text.Remove(startIndex, endIndex-startIndex+1).Insert(startIndex, newText);
                    }
                    if (contained)
                    {
                        File.WriteAllText(file.FullName, "");
                        File.WriteAllText(file.FullName, text);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        private List<FileInfo> GetAllFiles()
        {
            List<FileInfo> allFiles = new List<FileInfo>();
            var allDirs = LoadSubDirs(Path);
            allDirs = allDirs.Where(x => !x.Contains(".git") && !x.Contains(@"\obj\") && !x.Contains(@"\bin\")).ToList();
            foreach (var directory in allDirs)
            {
                var di = new DirectoryInfo(directory);
                allFiles.AddRange(di.GetFiles());
            }

            allFiles = allFiles.Where(x => x.FullName.Contains("ViewModel.cs") || x.FullName.Contains(".xaml.cs")).ToList();
            return allFiles;
        }
        private DelegateCommand _changeBackToReactiveCommand;
        public DelegateCommand ChangeBackToReactiveCommand =>
            _changeBackToReactiveCommand ?? (_changeBackToReactiveCommand = new DelegateCommand(ExecuteChangeBackToReactiveCommand));

        void ExecuteChangeBackToReactiveCommand()
        {
            string startMark = "/*ReactiveStartMark ";
            string endMark = "//ReactiveEndMark";

            var allFiles = GetAllFiles();
            foreach (var file in allFiles)
            {
                var text = File.ReadAllText(file.FullName);
                bool contained = text.Contains(startMark);
                while (text.Contains(startMark))
                {
                    var startIndex = text.IndexOf(startMark);
                    var endIndex = text.IndexOf(endMark, startIndex);
                    var firstEnd = text.IndexOf("}", startIndex);
                    var secondEnd = text.IndexOf("}", firstEnd);
                    var thirdEnd = text.IndexOf("}", secondEnd);
                    var fourthEnd = text.IndexOf("}", thirdEnd);
                    var fifthEnd = text.IndexOf("}", fourthEnd);
                    var replaceText = text.Substring(startIndex, fifthEnd - startIndex + 1);
                    replaceText = replaceText.Replace(startMark, "");
                    replaceText = replaceText.Replace("[Reactive1]", "[Reactive]");
                    var newTex = text.Substring(startIndex, endIndex - startIndex + endMark.Length);
                    text = text.Remove(startIndex, endIndex - startIndex + endMark.Length).Insert(startIndex, replaceText);
                }
                if (contained)
                {
                    File.WriteAllText(file.FullName, "");
                    File.WriteAllText(file.FullName, text);
                }
            }
        }
    }
}
