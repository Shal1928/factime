using System;
using System.Windows;

namespace Factime.Controls
{
    /// <summary>
    /// Interaction logic for YearSelector.xaml
    /// </summary>
    public partial class YearSelector
    {
        public YearSelector()
        {
            InitializeComponent();
            SelectedYear = DateTime.Now.Year;        
        }


        #region SelectedYear

        public static readonly DependencyProperty SelectedYearProperty =
            DependencyProperty.Register("SelectedYear",
                                        typeof(int),
                                        typeof(YearSelector),
                                        new UIPropertyMetadata(0, OnSelectedYearPropertyChanged)
                                        );

        public int SelectedYear
        {
            get
            {
                return (int)GetValue(SelectedYearProperty);
            }
            set
            {
                SetValue(SelectedYearProperty, value);
            }
        }

        private static void OnSelectedYearPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion
    }
}
