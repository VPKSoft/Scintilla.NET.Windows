using System;
using System.Windows;

namespace TestAppWpf;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private bool shown;

    private void MainWindow_OnContentRendered(object? sender, EventArgs e)
    {
        if (IsVisible && !shown)
        {
            shown = true;
        }
    }
}
