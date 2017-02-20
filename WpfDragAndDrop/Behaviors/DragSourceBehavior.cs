//  --------------------------------------------------------------------------------------
// WpfDragAndDrop.DragSourceBehavior.cs
// 2017/02/20
//  --------------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using WpfDragAndDrop.Model;

namespace WpfDragAndDrop.Behaviors
{
    public class DragSourceBehavior
    {
        public static readonly DependencyProperty DragSourceProperty =
            DependencyProperty.RegisterAttached("DragSource",
                                                typeof(Rectangle),
                                                typeof(DragSourceBehavior),
                                                new PropertyMetadata(default(Rectangle),
                                                                     OnDragSourceChanged));

        public static Rectangle GetDragSource(DependencyObject obj)
        {
            return (Rectangle) obj.GetValue(DragSourceProperty);
        }

        public static void SetDragSource(DependencyObject obj, Rectangle value)
        {
            obj.SetValue(DragSourceProperty, value);
        }

        static void InitiateDrag(object sender, MouseEventArgs e)
        {
            var target = (Rectangle) sender;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var data = new RectangleData(target);
                var dataObject = new DataObject(DataFormats.Serializable, data);
                DragDrop.DoDragDrop(target, dataObject, DragDropEffects.Copy);
            }
                
        }

        static void OnDragSourceChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            // Hook MouseMove event
            var target = (Rectangle) e.NewValue;
            target.MouseMove += InitiateDrag;
        }
    }
}