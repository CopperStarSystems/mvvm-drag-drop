//  --------------------------------------------------------------------------------------
// WpfDragAndDrop.DragMoveBehavior.cs
// 2017/02/20
//  --------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace WpfDragAndDrop.Behaviors
{
    public class DragMoveBehavior : DependencyObject
    {
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.RegisterAttached("Target",
                                                typeof(Rectangle),
                                                typeof(DragMoveBehavior),
                                                new PropertyMetadata(default(Rectangle),
                                                                     OnTargetChanged));

        static readonly DependencyProperty ClickPositionProperty =
            DependencyProperty.RegisterAttached("ClickPosition",
                                                typeof(Point),
                                                typeof(DragMoveBehavior),
                                                new PropertyMetadata(default(Point)));

        public static Rectangle GetTarget(DependencyObject obj)
        {
            return (Rectangle) obj.GetValue(TargetProperty);
        }

        public static void SetTarget(DependencyObject obj, Rectangle value)
        {
            obj.SetValue(TargetProperty, value);
        }

        static void CaptureMouseForDrag(object sender, MouseButtonEventArgs e)
        {
            var rectangle = (Rectangle) sender;
            var position = e.GetPosition(rectangle);
            SetClickPosition(rectangle, position);
            rectangle.CaptureMouse();
            rectangle.MouseLeftButtonUp += UncaptureMouse;
            rectangle.MouseMove += MoveRectangle;
        }

        static Point GetClickPosition(DependencyObject obj)
        {
            return (Point) obj.GetValue(ClickPositionProperty);
        }

        static void MoveRectangle(object sender, MouseEventArgs e)
        {
            var rectangle = (Rectangle) sender;
            var parent = (IInputElement) rectangle.Parent;
            var position = e.GetPosition(parent);
            var clickPosition = GetClickPosition(rectangle);
            Canvas.SetTop(rectangle, position.Y - clickPosition.Y);
            Canvas.SetLeft(rectangle, position.X - clickPosition.X);
        }

        static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (Rectangle) e.NewValue;
            target.MouseLeftButtonDown += CaptureMouseForDrag;
        }

        static void SetClickPosition(DependencyObject obj, Point value)
        {
            obj.SetValue(ClickPositionProperty, value);
        }

        static void UncaptureMouse(object sender, MouseButtonEventArgs e)
        {
            var rectangle = (Rectangle) sender;
            rectangle.MouseLeftButtonUp -= UncaptureMouse;
            rectangle.MouseMove -= MoveRectangle;
            rectangle.ReleaseMouseCapture();
        }
    }
}