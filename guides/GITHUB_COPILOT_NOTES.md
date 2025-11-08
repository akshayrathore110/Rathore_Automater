# GitHub Copilot Change Log

This file records changes made by GitHub Copilot to the RathoreSearchAutomation project. All future edits I make will be documented here.

Conventions:
- Date: YYYY-MM-DD (local)
- Summary: Short description of the change
- Files: List of files touched
- Build/Typecheck: PASS/FAIL results at time of change
- Notes: Extra context or follow-ups

---

## 2025-10-20

Summary:

Files:

Build/Typecheck:

Notes:


## 2025-10-20

Summary:
- Added minimal DirectX overlay that attaches to Edge (msedge) window. Provides hotkeys: P (pause/resume), S (stop), Esc (exit app). Starts with RunningPage and disposes on stop/complete.

Files:
- `Helpers/Overlay/DirectXOverlayController.cs` — New: creates `DirectXOverlayWindow` over Edge main window; draws top bar with hotkey hints; manages visibility based on foreground window.
- `Helpers/Overlay/HotkeyManager.cs` — New: simple polling-based hotkey manager using `GetAsyncKeyState`.
- `Pages/RunningPage.xaml.cs` — Wire overlay lifecycle to page and automation events; hotkeys call existing Pause/Stop/Exit handlers.
- `RathoreSearchAutomation.csproj` — Added package refs (Overlay.NET, System.Drawing.Common).

Build/Typecheck:
- Build: PASS
- Lint/Typecheck: PASS

Notes:
- No dependency on Process.NET to reduce friction. We attach via `Process.GetProcessesByName("msedge")` and use `MainWindowHandle`.
- Overlay is resilient: errors are swallowed to avoid crashing the app.

## Future Updates

All subsequent changes I perform will be appended below with the same format.
