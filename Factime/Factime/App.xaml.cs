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
using UseAbilities.MVVM.Base;
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
            Loader(StaticHelper.IoCcontainer);
            var startupWindowSeed = (MainWindowViewModel)StaticHelper.IoCcontainer.Resolve(ObserveWrapper.Wrap(typeof(MainWindowViewModel)));

            var relationsViewToViewModel = new Dictionary<Type, Type>
                                         {
                                            {startupWindowSeed.GetType(), typeof (MainWindowView)}
                                         };

            ViewManager.RegisterViewViewModelRelations(relationsViewToViewModel);
            ViewModelManager.ActiveViewModels.CollectionChanged += ViewManager.OnViewModelsCoolectionChanged;

            startupWindowSeed.Show();
        }

        private static void Loader(IoC ioc)
        {
            ioc.RegisterSingleton<IXmlStore<FactimeSettings>, FactimeSettingsStore>();
            ioc.RegisterSingleton<IFileStore<List<CalendarDay>>, CalendarDayStore>();
        }
    }
}
