﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using Autofac.Builder;
using trackvisualizer.Ioc;
using trackvisualizer.Service;
using trackvisualizer.View;

namespace trackvisualizer
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer _container;

        private App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(@"Unhandled exception: " + e.Exception);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MaybeSetTheme();
            BuildContainer();

            _container.Resolve<LocalizationManager>().InitLocalization();   
            
            var window = _container.Resolve<MainWindow>();
            
            MainWindow = window;
            
            window.Show();
        }


        private void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<MainModule>();

            _container = builder.Build();
        }

        
        private void MaybeSetTheme()
        {
            try
            {
                var uri = new Uri(
                    "PresentationFramework.Aero;V3.0.0.0;31bf3856ad364e35;component\\themes/aero.normalcolor.xaml",
                    UriKind.Relative);
                Resources.MergedDictionaries.Add(LoadComponent(uri) as ResourceDictionary);
            }
            catch (Exception)
            {
            }
        }
    }
}