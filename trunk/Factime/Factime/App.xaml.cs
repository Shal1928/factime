using System;
using System.Collections.Generic;
using System.Windows;
using Factime.Models;
using Factime.Stores;
using Factime.ViewModels;
using Factime.Views;
using UseAbilities.IoC.Core;
using UseAbilities.IoC.Helpers;
using UseAbilities.IoC.Stores;
using UseAbilities.MVVM.Managers;

namespace Factime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var relationsViewToViewModel = new Dictionary<Type, Type>
                                         {
                                            {typeof (MainWindowViewModel), typeof (MainWindowView)}
                                         };

            ViewManager.RegisterViewViewModelRelations(relationsViewToViewModel);
            ViewModelManager.ActiveViewModels.CollectionChanged += ViewManager.OnViewModelsCoolectionChanged;

            Loader(StaticHelper.IoCcontainer);

            var startupWindow = StaticHelper.IoCcontainer.Resolve<MainWindowViewModel>();
            startupWindow.Show();
        }

        private static void Loader(IoC ioc)
        {
            ioc.RegisterSingleton<IXmlStore<FactimeSettings>, FactimeSettingsStore>();
            //ioc.RegisterSingleton<DBStore<Requests>, RequestsDBStore>();
        }
    }
}
