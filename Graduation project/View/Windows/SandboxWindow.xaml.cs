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
<head>
    <title>Demo Page</title>
    <style>
        body { font-family: Arial; }
        header, footer { background: #f0f0f0; padding: 10px; }
    </style>
</head>
<body>
    <header>
        <h1>Header</h1>
    </header>
    <main>
        <p>Content goes here...</p>
    </main>
    <footer>
        Footer
    </footer>
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

        private void HomeBtn_Click(object sender, RoutedEventArgs e) { }
        private void TheoryBtn_Click(object sender, RoutedEventArgs e) { }
        private void CourceBtn_Click(object sender, RoutedEventArgs e) { }
        private void ProgressBtn_Click(object sender, RoutedEventArgs e) { }
        private void SettingsBtn_Click(object sender, RoutedEventArgs e) { }

    }

}