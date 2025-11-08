using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using RathoreSearchAutomation.Windows;

namespace RathoreSearchAutomation.Helpers.Overlay
{
    public class WpfOverlayController : IDisposable
    {
        private readonly Action _onPauseToggle;
        private readonly Action _onStop;
        private readonly Action _onExit;
        private readonly HotkeyManager _hotkeys = new HotkeyManager();
        private EdgeOverlayWindow? _overlay;
        private CancellationTokenSource? _cts;
        private IntPtr _edgeHwnd = IntPtr.Zero;
        private bool _paused;

        public WpfOverlayController(Action onPauseToggle, Action onStop, Action onExit)
        {
            _onPauseToggle = onPauseToggle;
            _onStop = onStop;
            _onExit = onExit;
        }

        public void Start()
        {
            _hotkeys.RegisterPauseToggle(() =>
            {
                _paused = !_paused;
                _overlay?.UpdatePaused(_paused);
                _onPauseToggle?.Invoke();
            });
            _hotkeys.RegisterStop(() => _onStop?.Invoke());
            _hotkeys.RegisterExit(() => _onExit?.Invoke());

            _overlay = new EdgeOverlayWindow();
            _overlay.Show();

            _cts = new CancellationTokenSource();
            Task.Run(() => Loop(_cts.Token));
        }

        public void UpdateProgress(int current, int total)
        {
            _overlay?.SetProgress(current, total);
        }

        private void Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                EnsureEdgeHandle();
                if (_overlay != null && _edgeHwnd != IntPtr.Zero)
                {
                    if (GetWindowRect(_edgeHwnd, out RECT rect))
                    {
                        int w = rect.Right - rect.Left;
                        int h = rect.Bottom - rect.Top;
                        _overlay.MoveTo(rect.Left, rect.Top, w, h);
                    }
                    else
                    {
                        Application.Current?.Dispatcher.Invoke(() => _overlay.Visibility = Visibility.Collapsed);
                    }
                }
                else if (_overlay != null)
                {
                    Application.Current?.Dispatcher.Invoke(() => _overlay.Visibility = Visibility.Collapsed);
                }
                Thread.Sleep(50);
            }
        }

        private void EnsureEdgeHandle()
        {
            if (_edgeHwnd != IntPtr.Zero)
            {
                if (!IsWindow(_edgeHwnd)) _edgeHwnd = IntPtr.Zero;
                else return;
            }
            var edge = Process.GetProcessesByName("msedge").FirstOrDefault();
            if (edge != null && edge.MainWindowHandle != IntPtr.Zero)
                _edgeHwnd = edge.MainWindowHandle;
        }

        public void Dispose()
        {
            try
            {
                _cts?.Cancel();
                _cts?.Dispose();
                _hotkeys.Dispose();
                Application.Current?.Dispatcher.Invoke(() => _overlay?.Close());
            }
            catch { }
        }

        [DllImport("user32.dll")] static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")] static extern bool IsWindow(IntPtr hWnd);

        private struct RECT { public int Left, Top, Right, Bottom; }
    }
}
