﻿using System.Windows;

namespace StroyCompany
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
