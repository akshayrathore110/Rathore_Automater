using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace RathoreSearchAutomation.Helpers.Overlay
{
    internal class HotkeyManager : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private CancellationTokenSource? _cts;
        private Action? _onPause;
        private Action? _onStop;
        private Action? _onExit;

        public void RegisterPauseToggle(Action action)
        {
            _onPause = action;
            EnsureLoop();
        }

        public void RegisterStop(Action action)
        {
            _onStop = action;
            EnsureLoop();
        }

        public void RegisterExit(Action action)
        {
            _onExit = action;
            EnsureLoop();
        }

        private void EnsureLoop()
        {
            if (_cts != null) return;
            _cts = new CancellationTokenSource();
            Task.Run(() => Loop(_cts.Token));
        }

        private void Loop(CancellationToken token)
        {
            bool pDown = false, sDown = false, escDown = false;
            while (!token.IsCancellationRequested)
            {
                // P key
                bool pNow = (GetAsyncKeyState(0x50) & 0x8000) != 0; // 'P'
                if (pNow && !pDown) _onPause?.Invoke();
                pDown = pNow;

                // S key
                bool sNow = (GetAsyncKeyState(0x53) & 0x8000) != 0; // 'S'
                if (sNow && !sDown) _onStop?.Invoke();
                sDown = sNow;

                // Esc key
                bool escNow = (GetAsyncKeyState(0x1B) & 0x8000) != 0; // VK_ESCAPE
                if (escNow && !escDown) _onExit?.Invoke();
                escDown = escNow;

                Thread.Sleep(50);
            }
        }

        public void Dispose()
        {
            try { _cts?.Cancel(); _cts?.Dispose(); } catch { }
        }
    }
}
