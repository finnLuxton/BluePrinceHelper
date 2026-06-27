using System;
using BluePrinceHelper.ViewModels;
using BluePrinceHelper.Views;

namespace BluePrinceHelper;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>
    /// Application Entry for BluePrinceHelper
    /// </summary>
    public App()
    {
        var view = new MainView
        {
            DataContext = Activator.CreateInstance<MainViewModel>()
        };

        view.Show();
    }
}