using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Shapes;

namespace Factime.Behaviors
{
    public class YearSelectorBehavior : Behavior<Path>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseLeftButtonDown += (sender, e) =>
            {
                if (IsIncrease) SelectedYear++;
                else SelectedYear--;
            };
        }

        #region SelectedYear

        public static readonly DependencyProperty SelectedYearProperty =
            DependencyProperty.Register("SelectedYear",
                                        typeof(int),
                                        typeof(YearSelectorBehavior),
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

        #region IsIncrease

        public static readonly DependencyProperty IsIncreaseProperty =
            DependencyProperty.Register("IsIncrease",
                                        typeof(bool),
                                        typeof(YearSelectorBehavior),
                                        new UIPropertyMetadata(false, OnIsIncreaseChanged)
                                        );

        public bool IsIncrease
        {
            get
            {
                return (bool)GetValue(IsIncreaseProperty);
            }
            set
            {
                SetValue(IsIncreaseProperty, value);
            }
        }

        private static void OnIsIncreaseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        #endregion
    }
}
