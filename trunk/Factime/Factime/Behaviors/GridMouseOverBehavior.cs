using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Factime.Behaviors
{
    public class GridMouseOverBehavior : Behavior<Grid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseEnter += (sender, e) =>
            {
                foreach (Control radioButton in FindInVisualTreeDown(AssociatedObject, typeof(RadioButton)))
                    radioButton.Visibility = Visibility.Visible;                              
            };

            AssociatedObject.MouseLeave += (sender, e) =>
            {
                foreach (Control radioButton in FindInVisualTreeDown(AssociatedObject, typeof(RadioButton)))
                    radioButton.Visibility = Visibility.Hidden;
            };
        }

        private static IEnumerable<DependencyObject> FindInVisualTreeDown(DependencyObject dependencyObject, Type type)
        {
            if (dependencyObject != null)
            {
                if (dependencyObject.GetType() == type) yield return dependencyObject;
                

                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                    foreach (var child in FindInVisualTreeDown(VisualTreeHelper.GetChild(dependencyObject, i), type))
                        if (child != null) yield return child;
            }

            yield break;
        } 
    }
}
