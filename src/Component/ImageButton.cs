using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CalcColor.Component {
    class ImageButton : Button {
        static ImageButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        public static readonly DependencyProperty DefaultImageProperty =
                                                        DependencyProperty.Register("DefaultImage",
                                                                                    typeof(BitmapImage),
                                                                                    typeof(ImageButton),
                                                                                    new FrameworkPropertyMetadata());
        public static readonly DependencyProperty HoverImageProperty =
                                                        DependencyProperty.Register("HoverImage",
                                                                                    typeof(BitmapImage),
                                                                                    typeof(ImageButton),
                                                                                    new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty PressedImageProperty =
                                                        DependencyProperty.Register("PressedImage",
                                                                                    typeof(BitmapImage),
                                                                                    typeof(ImageButton),
                                                                                    new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty DisabledImageProperty =
                                                        DependencyProperty.Register("DisabledImage",
                                                                                    typeof(BitmapImage),
                                                                                    typeof(ImageButton),
                                                                                    new FrameworkPropertyMetadata(null));
        public BitmapImage DefaultImage {
            get { return (BitmapImage)GetValue(DefaultImageProperty); }
            set { SetValue(DefaultImageProperty, value); }
        }

        public BitmapImage HoverImage {
            get { return (BitmapImage)FindResource(GetValue(HoverImageProperty)); }
            set { SetValue(HoverImageProperty, value); }
        }

        public BitmapImage PressedImage {
            get { return (BitmapImage)GetValue(PressedImageProperty); }
            set { SetValue(PressedImageProperty, value); }
        }

        public BitmapImage DisabledImage {
            get { return (BitmapImage)GetValue(DisabledImageProperty); }
            set { SetValue(DisabledImageProperty, value); }
        }
    }
}
