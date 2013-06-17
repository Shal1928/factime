using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Factime.Models;

namespace Factime.Controls
{
    public class DataGridTemplateColumnExt : DataGridBoundColumn
    {
        //#region DependencyProperty DataContext

        //public static readonly DependencyProperty DataContextProperty =
        //    DependencyProperty.Register("DataContext",
        //                                typeof(object),
        //                                typeof(DataGridTemplateColumnExt),
        //                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, OnDataContextChanged)
        //                                );

        //public object DataContext
        //{
        //    get
        //    {
        //        return (object)GetValue(DataContextProperty);
        //    }
        //    set
        //    {
        //        SetValue(DataContextProperty, value);
        //    }
        //}

        //private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    //
        //}

        //#endregion

        public DataTemplate CellTemplate
        {
            get; 
            set;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            if (Binding == null) return null;
            var binding = new Binding(((Binding) Binding).Path.Path)
                              {
                                  Source = dataItem
                              };

            var content = new ContentControl
                              {
                                  ContentTemplate = CellTemplate
                              };
            content.SetBinding(ContentControl.ContentProperty, binding);

            return content;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }
    }
}
