using Graduation_project.AppData;
using Microsoft.Web.WebView2.Core;
using System;
using System.Windows;

namespace Graduation_project.View.Windows
{
    public partial class SandboxWindow : Window
    {
        public SandboxWindow()
        {
            InitializeComponent();

            NicknameTbl.Text = UserSession.UserName;

            CodeEditor.Text = @"<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8'>
<style>
html, body{
    background:white;
    color:black;
    margin:20px;
}
</style>
</head>
<body>
<h1>Header</h1>
<p>Content goes here...</p>
</body>
</html>";

            Loaded += SandboxWindow_Loaded;
            CodeEditor.TextChanged += CodeEditor_TextChanged;
        }

        private async void SandboxWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var options = new CoreWebView2EnvironmentOptions(
                "--disable-features=msWebOOUI,msPdfOOUI");

            var env = await CoreWebView2Environment.CreateAsync(
                null,
                null,
                options);

            await Preview.EnsureCoreWebView2Async(env);

            Preview.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

            Preview.CoreWebView2.NavigateToString(CodeEditor.Text);
        }

        private void CodeEditor_TextChanged(object? sender, EventArgs e)
        {
            if (Preview.CoreWebView2 != null)
            {
                Preview.CoreWebView2.NavigateToString(CodeEditor.Text);
            }
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void TheoryBtn_Click(object sender, RoutedEventArgs e)
        {
            new TheoryWindow().Show();
            Close();
        }

        private void CourceBtn_Click(object sender, RoutedEventArgs e)
        {
            new CourseWindow().Show();
            Close();
        }

        private void ProgressBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProgressWindow().Show();
            Close();
        }
    }
}