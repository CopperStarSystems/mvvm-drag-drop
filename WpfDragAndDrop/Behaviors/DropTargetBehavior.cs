//  --------------------------------------------------------------------------------------
// WpfDragAndDrop.DropTargetBehavior.cs
// 2017/02/20
//  --------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using WpfDragAndDrop.Model;

namespace WpfDragAndDrop.Behaviors
{
    public class DropTargetBehavior
    {
        public static readonly DependencyProperty DropTargetProperty =
            DependencyProperty.RegisterAttached("DropTarget",
                                                typeof(Canvas),
                                                typeof(DropTargetBehavior),
                                                new PropertyMetadata(default(Canvas),
                                                                     OnDropTargetChanged));

        public static Canvas GetDropTarget(DependencyObject obj)
        {
            return (Canvas) obj.GetValue(DropTargetProperty);
        }

        public static void SetDropTarget(DependencyObject obj, Canvas value)
        {
            obj.SetValue(DropTargetProperty, value);
        }

        static void AddChildToCanvas(Canvas canvas, Rectangle child)
        {
            canvas.Children.Add(child);
        }

        static Rectangle CreateRectangleFromData(RectangleData data)
        {
            var child = new Rectangle
                        {
                            Fill = data.Fill,
                            Height = data.Height,
                            Width = data.Width
                        };
            return child;
        }

        static RectangleData GetDroppedData(DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Serializable) as RectangleData;
            return data;
        }

        static void HandleDrop(object sender, DragEventArgs e)
        {
            var canvas = (Canvas) sender;
            if (e.Data.GetDataPresent(DataFormats.Serializable))
            {
                var data = GetDroppedData(e);
                var child = CreateRectangleFromData(data);
                SetChildPosition(e, canvas, child);
                AddChildToCanvas(canvas, child);
            }
        }

        static void OnDropTargetChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var target = (Canvas) e.NewValue;
            target.Drop += HandleDrop;
        }

        static void SetChildPosition(DragEventArgs e, Canvas canvas, Rectangle child)
        {
            var position = e.GetPosition(canvas);
            Canvas.SetTop(child, position.Y);
            Canvas.SetLeft(child, position.X);
        }
    }
}