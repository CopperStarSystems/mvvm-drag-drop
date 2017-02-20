//  --------------------------------------------------------------------------------------
// WpfDragAndDrop.RectangleData.cs
// 2017/02/20
//  --------------------------------------------------------------------------------------

using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfDragAndDrop.Model
{
    [Serializable]
    public class RectangleData
    {
        public RectangleData(Rectangle input)
        {
            Fill = input.Fill;
            Height = input.ActualHeight;
            Width = input.ActualWidth;
        }

        RectangleData()
        {
        }

        public Brush Fill { get; private set; }

        public double Height { get; private set; }

        public double Width { get; private set; }
    }
}