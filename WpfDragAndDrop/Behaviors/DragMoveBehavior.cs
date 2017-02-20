//  --------------------------------------------------------------------------------------
// WpfDragAndDrop.DragMoveBehavior.cs
// 2017/02/20
//  --------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Shapes;

namespace WpfDragAndDrop.Behaviors
{
    public class DragMoveBehavior
    {
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.RegisterAttached("Target",
                                                typeof(Rectangle),
                                                typeof(DragMoveBehavior),
                                                new PropertyMetadata(default(Rectangle),
                                                                     OnTargetChanged));

        public static Rectangle GetTarget(DependencyObject obj)
        {
            return (Rectangle) obj.GetValue(TargetProperty);
        }

        public static void SetTarget(DependencyObject obj, Rectangle value)
        {
            obj.SetValue(TargetProperty, value);
        }

        static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (Rectangle) e.NewValue;
        }
    }
}