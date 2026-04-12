using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Graduation_project.View.Windows
{
    public partial class SandboxWindow : Window
    {
        public SandboxWindow()
        {
            InitializeComponent();
            InitializeAsync();
            #region Начальный шаблон страницы
            CodeEditor.Text = @"<!DOCTYPE html>
<html>
<body>
        <h1>Header</h1>
        <p>Content goes here...</p>
        Footer
</body>
</html>";
            #endregion
            CodeEditor.TextChanged += CodeEditor_TextChanged;
        }

        private async void InitializeAsync()
        {
            await Preview.EnsureCoreWebView2Async(null);
            Preview.NavigateToString(CodeEditor.Text);
        }

        private void CodeEditor_TextChanged(object? sender, EventArgs e)
        {
            if (Preview.CoreWebView2 != null)
            {
                Preview.NavigateToString(CodeEditor.Text);
            }
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void TheoryBtn_Click(object sender, RoutedEventArgs e)
        {
            TheoryWindow theoryWindow = new TheoryWindow();
            theoryWindow.Show();
            this.Close();
        }
        private void CourceBtn_Click(object sender, RoutedEventArgs e)
        {
            CourseWindow courseWindow = new CourseWindow();
            courseWindow.Show();
            this.Close();
        }
        private void ProgressBtn_Click(object sender, RoutedEventArgs e)
        {
            ProgressWindow progressWindow = new ProgressWindow();
            progressWindow.Show();
            this.Close();
        }
        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }

    }

}