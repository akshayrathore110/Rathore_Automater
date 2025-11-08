using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace RathoreSearchAutomation.Windows
{
    public partial class EdgeOverlayWindow : Window
    {
        public EdgeOverlayWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            int exStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            exStyle |= WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW; // click-through and hide from Alt+Tab
            SetWindowLong(hwnd, GWL_EXSTYLE, exStyle);
        }

        public void UpdatePaused(bool paused)
        {
            Dispatcher.Invoke(() =>
            {
                StatusText.Text = paused ? "Rathore Overlay — Paused" : "Rathore Overlay — Running";
            });
        }

        public void MoveTo(int x, int y, int width, int height)
        {
            Dispatcher.Invoke(() =>
            {
                // Convert from device pixels to WPF DIPs using the current HwndSource transform
                var src = PresentationSource.FromVisual(this) as HwndSource;
                Matrix transform = src?.CompositionTarget?.TransformFromDevice ?? Matrix.Identity;

                // Edge window rect in device pixels
                var topLeft = transform.Transform(new Point(x, y));
                var bottomRight = transform.Transform(new Point(x + width, y + height));

                double w = Math.Max(0, bottomRight.X - topLeft.X);
                double h = Math.Max(0, bottomRight.Y - topLeft.Y);

                if (w < 5 || h < 5)
                {
                    // Likely minimized or not visible
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    Left = topLeft.X;
                    Top = topLeft.Y;
                    Width = w;
                    Height = h;
                }
            });
        }

        public void SetProgress(int current, int total)
        {
            Dispatcher.Invoke(() =>
            {
                ProgressText.Text = $"Progress: {current} / {total}";
            });
        }

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_TOOLWINDOW = 0x80;

        [DllImport("user32.dll")] static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")] static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    }
}
