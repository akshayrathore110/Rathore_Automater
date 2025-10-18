# WPF Setup Guide for Visual Studio 2022

## Step-by-Step Instructions

### 1. Create New WPF Project

1. Open **Visual Studio 2022**
2. Click **File** → **New** → **Project**
3. Search for **"WPF Application"**
4. Select **"WPF Application"** (not WPF App (.NET Framework))
5. Click **Next**
6. Project name: `RathoreSearchAutomation`
7. Framework: **.NET 6.0** or **.NET 7.0**
8. Click **Create**

### 2. Add Project Files

After the project is created, add all the files from the `/wpf/` folder to your project:

#### Create Folder Structure:
```
RathoreSearchAutomation/
├── Pages/              (Create this folder)
├── Helpers/            (Create this folder)
└── Resources/          (Create this folder)
```

Right-click on the project in Solution Explorer:
- **Add** → **New Folder** → Name it `Pages`
- **Add** → **New Folder** → Name it `Helpers`
- **Add** → **New Folder** → Name it `Resources`

#### Copy Files:

1. **Replace `App.xaml` and `App.xaml.cs`** with the provided files
2. **Replace `MainWindow.xaml` and `MainWindow.xaml.cs`** with the provided files
3. **Add to `Resources/` folder:**
   - Copy `Styles.xaml`
   - Add your background images:
     - `dark-bg.png` (your dark mode background)
     - `light-bg.jpg` (download from Unsplash or use any colorful gradient image)

4. **Add to `Pages/` folder:**
   - `LoginPage.xaml` + `LoginPage.xaml.cs`
   - `ConfigPage.xaml` + `ConfigPage.xaml.cs`
   - `RunningPage.xaml` + `RunningPage.xaml.cs`
   - `CompletionPage.xaml` + `CompletionPage.xaml.cs`

5. **Add to `Helpers/` folder:**
   - `EdgeProfileDetector.cs`
   - `SearchAutomation.cs`

6. **Add to project root:**
   - `search_runner.py` (your Python automation script)

### 3. Configure Project File

Right-click on the project → **Edit Project File** and ensure it looks like this:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net6.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWPF>true</UseWPF>
  </PropertyGroup>

  <ItemGroup>
    <None Update="search_runner.py">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
    <None Update="Resources\**">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
</Project>
```

### 4. Add Background Images

For the background images, you have two options:

**Option A: Use Your Existing Images**
- Copy your `dark-bg.png` to `Resources/` folder
- Download a colorful gradient image for light mode and save as `Resources/light-bg.jpg`

**Option B: Use Solid Colors (Fallback)**
The app will automatically use gradient backgrounds if images are not found.

### 5. Set Image Properties

For each image in the `Resources/` folder:
1. Right-click the image in Solution Explorer
2. Select **Properties**
3. Set **Build Action** to `Resource`
4. Set **Copy to Output Directory** to `Copy if newer`

### 6. Build and Run

1. Press **F5** or click **Start Debugging**
2. The application should launch with the login page

### 7. Publishing as EXE

To create a standalone EXE file:

1. Right-click the project → **Publish**
2. Choose **Folder** as target
3. Click **Finish**
4. In the publish profile settings:
   - **Target Framework**: net6.0-windows
   - **Deployment mode**: Self-contained
   - **Target runtime**: win-x64
   - Check **Produce single file**
5. Click **Publish**

The EXE will be in: `bin\Release\net6.0-windows\win-x64\publish\`

### 8. Installing Python Dependencies

For the automation to work, users need:
- Python 3.x installed
- `pyautogui` package: `pip install pyautogui`

Alternatively, you can bundle Python with PyInstaller (see Advanced Setup below).

## Advanced: Bundle Python Script

To avoid requiring Python installation on target machines:

### Option 1: PyInstaller
Convert `search_runner.py` to an EXE:
```bash
pip install pyinstaller
pyinstaller --onefile search_runner.py
```

Then update `SearchAutomation.cs` to call `search_runner.exe` instead of `python search_runner.py`.

### Option 2: IronPython
Use IronPython to run Python code directly from C# (more complex but no external dependencies).

## Troubleshooting

### Images Not Loading
If background images don't show:
1. Check the image paths in `MainWindow.xaml`
2. Ensure images are set as `Resource` in properties
3. The app will use fallback colors if images fail

### Python Script Not Running
1. Verify Python is installed: `python --version`
2. Check `search_runner.py` is in the output directory
3. Install required packages: `pip install pyautogui`

### Edge Profiles Not Detected
The app will show "Default" profile if:
- Edge is not installed
- User Data directory doesn't exist
- Permissions issue

This is normal and the script will still try to run Edge.

## Project Structure Reference

```
RathoreSearchAutomation/
│
├── App.xaml                    # Application entry point
├── App.xaml.cs                 # Application code-behind
├── MainWindow.xaml             # Main window UI
├── MainWindow.xaml.cs          # Main window logic
├── search_runner.py            # Python automation script
│
├── Pages/
│   ├── LoginPage.xaml
│   ├── LoginPage.xaml.cs
│   ├── ConfigPage.xaml
│   ├── ConfigPage.xaml.cs
│   ├── RunningPage.xaml
│   ├── RunningPage.xaml.cs
│   ├── CompletionPage.xaml
│   └── CompletionPage.xaml.cs
│
├── Helpers/
│   ├── EdgeProfileDetector.cs  # Detects Edge browser profiles
│   └── SearchAutomation.cs     # Manages Python script execution
│
└── Resources/
    ├── Styles.xaml             # UI styles (glassmorphism)
    ├── dark-bg.png             # Dark mode background
    └── light-bg.jpg            # Light mode background
```

## Features Implemented

✅ Glassmorphism UI with dark/light mode toggle
✅ Login page with User ID and Key fields
✅ Configuration page with search count and Edge profile selection
✅ Running page (overlay) with pause/resume/stop controls
✅ Completion page with restart/exit options
✅ Automatic Edge profile detection
✅ Python script integration
✅ Browser close option on stop/exit
✅ "RATHORE" branding with custom font
✅ Telegram button linking to @Akshayr677

## Next Steps

After setup:
1. Test the login flow
2. Configure your search parameters
3. Verify Python script integration
4. Build and test the EXE
5. Deploy to target machines

Good luck with your WPF application! 🚀
