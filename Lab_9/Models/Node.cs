using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab_9.Models
{
    public class Node : INotifyPropertyChanged
    {
        public ObservableCollection<Node> Content { get; set; }//Содержит папки и картинки

        public ObservableCollection<Node> Images { get; set; }
        public Node Parent { get; }
        public string Fullpath { get; }
        public string IconPath { get; }
        public string Name { get; }
        public int ImageCounter { get; set; }
        public Node(string _path, Node? parent)
        {

            Parent = parent;
            Fullpath = _path;

            if (Fullpath.EndsWith(".png") || Fullpath.EndsWith(".jpg"))
            {
                IconPath = "../../../Assets/ImageIcon.png";
            }
            else IconPath = "../../../Assets/FolderIcon.png";

            Name = Path.GetFileName(_path);
            ImageCounter = 0;
            if (!File.Exists(_path))
            {

                try
                {
                    Content = new ObservableCollection<Node>();
                    string[] images = Directory.GetFiles(_path, "*.jpg");
                    foreach (string image in images)
                    {
                        Content.Add(new Node(image, this));
                        ImageCounter++;
                    }
                    images = Directory.GetFiles(_path, "*.png");
                    foreach (string image in images)
                    {
                        Content.Add(new Node(image, this));
                        ImageCounter++;
                    }
                }
                catch (UnauthorizedAccessException)
                {

                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyCnahged([CallerMemberName] String propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
