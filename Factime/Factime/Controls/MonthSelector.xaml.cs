using System;
using System.Windows;
using System.Windows.Controls;

namespace Factime.Controls
{
    /// <summary>
    /// Interaction logic for MonthSelector.xaml
    /// </summary>
    public partial class MonthSelector : UserControl
    {
        public MonthSelector()
        {
            InitializeComponent();

            SelectedMonth = DateTime.Now.Month;
        }

        //Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec

        #region Jan

        public static readonly DependencyProperty JanProperty =
            DependencyProperty.Register("Jan",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnJanChanged)
                                        );

        public bool Jan
        {
            get
            {
                return (bool)GetValue(JanProperty);
            }
            set
            {
                SetValue(JanProperty, value);
            }
        }

        private static void OnJanChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Feb

        public static readonly DependencyProperty FebProperty =
            DependencyProperty.Register("Feb",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnFebChanged)
                                        );

        public bool Feb
        {
            get
            {
                return (bool)GetValue(FebProperty);
            }
            set
            {
                SetValue(FebProperty, value);
            }
        }

        private static void OnFebChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Mar

        public static readonly DependencyProperty MarProperty =
            DependencyProperty.Register("Mar",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnMarChanged)
                                        );

        public bool Mar
        {
            get
            {
                return (bool)GetValue(MarProperty);
            }
            set
            {
                SetValue(MarProperty, value);
            }
        }

        private static void OnMarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Apr

        public static readonly DependencyProperty AprProperty =
            DependencyProperty.Register("Apr",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnAprChanged)
                                        );

        public bool Apr
        {
            get
            {
                return (bool)GetValue(AprProperty);
            }
            set
            {
                SetValue(AprProperty, value);
            }
        }

        private static void OnAprChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region May

        public static readonly DependencyProperty MayProperty =
            DependencyProperty.Register("May",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnMayChanged)
                                        );

        public bool May
        {
            get
            {
                return (bool)GetValue(MayProperty);
            }
            set
            {
                SetValue(MayProperty, value);
            }
        }

        private static void OnMayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Jun

        public static readonly DependencyProperty JunProperty =
            DependencyProperty.Register("Jun",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnJunChanged)
                                        );

        public bool Jun
        {
            get
            {
                return (bool)GetValue(JunProperty);
            }
            set
            {
                SetValue(JunProperty, value);
            }
        }

        private static void OnJunChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Jul

        public static readonly DependencyProperty JulProperty =
            DependencyProperty.Register("Jul",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnJulChanged)
                                        );

        public bool Jul
        {
            get
            {
                return (bool)GetValue(JulProperty);
            }
            set
            {
                SetValue(JulProperty, value);
            }
        }

        private static void OnJulChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Aug

        public static readonly DependencyProperty AugProperty =
            DependencyProperty.Register("Aug",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnAugChanged)
                                        );

        public bool Aug
        {
            get
            {
                return (bool)GetValue(AugProperty);
            }
            set
            {
                SetValue(AugProperty, value);
            }
        }

        private static void OnAugChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Sep

        public static readonly DependencyProperty SepProperty =
            DependencyProperty.Register("Sep",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnSepChanged)
                                        );

        public bool Sep
        {
            get
            {
                return (bool)GetValue(SepProperty);
            }
            set
            {
                SetValue(SepProperty, value);
            }
        }

        private static void OnSepChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Oct

        public static readonly DependencyProperty OctProperty =
            DependencyProperty.Register("Oct",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnOctChanged)
                                        );

        public bool Oct
        {
            get
            {
                return (bool)GetValue(OctProperty);
            }
            set
            {
                SetValue(OctProperty, value);
            }
        }

        private static void OnOctChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Nov

        public static readonly DependencyProperty NovProperty =
            DependencyProperty.Register("Nov",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnNovChanged)
                                        );

        public bool Nov
        {
            get
            {
                return (bool)GetValue(NovProperty);
            }
            set
            {
                SetValue(NovProperty, value);
            }
        }

        private static void OnNovChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region Dec

        public static readonly DependencyProperty DecProperty =
            DependencyProperty.Register("Dec",
                                        typeof(bool),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(false, OnDecChanged)
                                        );

        public bool Dec
        {
            get
            {
                return (bool)GetValue(DecProperty);
            }
            set
            {
                SetValue(DecProperty, value);
            }
        }

        private static void OnDecChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion


        #region SelectedMonth

        public static readonly DependencyProperty SelectedMonthProperty =
            DependencyProperty.Register("SelectedMonth",
                                        typeof(int),
                                        typeof(MonthSelector),
                                        new UIPropertyMetadata(1, OnSelectedMonthChanged)
                                        );

        public int SelectedMonth
        {
            get
            {
                return (int)GetValue(SelectedMonthProperty);
            }
            set
            {
                SetValue(SelectedMonthProperty, value);
            }
        }

        private static void OnSelectedMonthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion
    }
}
