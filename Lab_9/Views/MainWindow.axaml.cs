using Avalonia.Controls;
using Lab_9.ViewModels;

namespace Lab_9.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.FindControl<MenuItem>("OpenFolderButton").Click += async delegate
            {
                var taskPath = new OpenFolderDialog()
                {
                    Title = "Choose folder..."
                };
                string? path = await taskPath.ShowAsync((Window)this.VisualRoot);

                var context = this.DataContext as MainWindowViewModel;
                if (path == null) return;
                else context.RootFolder = string.Join("\\", path);
                context.OpenDialogWindow();

            };

            this.FindControl<MenuItem>("ExitButton").Click += async delegate
            {
                this.Close();
            };
        }
    }
}
