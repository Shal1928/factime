using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Factime.Behaviors
{
    public class RadioButtonPreviewEventBehavior : Behavior<RadioButton>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseLeftButtonDown += (sender, e) =>
            {
                AssociatedObject.IsChecked = !AssociatedObject.IsChecked;
            };
        }
    }
}
