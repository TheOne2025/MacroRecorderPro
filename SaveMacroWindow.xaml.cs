using System;
using System.Windows;

namespace MacroRecorderReplica
{
    public partial class SaveMacroWindow : Window
    {
        public string FileName => FileNameBox.Text;

        public SaveMacroWindow()
        {
            InitializeComponent();
            FileNameBox.Text = $"Macro_{DateTime.Now:yyyyMMdd_HHmmss}";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
