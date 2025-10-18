# 🚀 START HERE - Quick Setup Guide

## ✅ What's Ready

All code files are updated with:
- ✨ Real glassmorphism (transparent glass effect)
- 🎨 SF Pro Rounded & Roboto fonts
- 🌙 Light/Dark theme toggle
- 📱 Telegram logo support
- 🪟 Window controls (minimize, maximize, close)

## 📁 What You Need to Add

### 1. **Create Fonts Folder**
```
Resources/
└── Fonts/           ← CREATE THIS
    ├── SF-Pro-Rounded-Bold.ttf
    ├── SF-Pro-Rounded-Semibold.ttf
    ├── SF-Pro-Rounded-Medium.ttf
    ├── SF-Pro-Rounded-Regular.ttf
    ├── Roboto-Bold.ttf
    ├── Roboto-Medium.ttf
    ├── Roboto-Regular.ttf
    └── Roboto-Light.ttf
```

**Download:**
- **SF Pro Rounded:** Apple Developer or font repositories
- **Roboto:** https://fonts.google.com/specimen/Roboto

### 2. **Add Your Images**
```
Resources/
├── light-bg.jpg     ← You already have this ✅
├── dark-bg.jpg      ← ADD THIS (dark version)
└── icon/
    └── telegram.png ← ADD THIS (your telegram logo)
```

## 🔧 Setup Steps (5 Minutes)

### Step 1: Add Fonts
1. Download the 8 font files listed above
2. In Visual Studio: Right-click `Resources` → Add → New Folder → Name it `Fonts`
3. Right-click `Fonts` → Add → Existing Item → Select all 8 .ttf files
4. For EACH .ttf file:
   - Right-click → Properties
   - **Build Action** → **Resource**
   - **Copy to Output Directory** → **Do not copy**

### Step 2: Add Images
1. Create a dark version of your light-bg.jpg (or use a dark image)
2. Name it `dark-bg.jpg`
3. Put in `Resources/` folder
4. Right-click → Properties → **Build Action: Resource**

5. Add your telegram logo:
   - Put `telegram.png` in `Resources/icon/`
   - Right-click → Properties → **Build Action: Resource**

### Step 3: Copy All Files
Copy from `/wpf` folder to your Visual Studio project, replacing:
- ✅ MainWindow.xaml & .cs
- ✅ All Page files (Login, Config, Running, Completion)
- ✅ Styles.xaml
- ✅ App.xaml & .cs
- ✅ Helper files

### Step 4: Build & Run
```
Build → Clean Solution
Build → Rebuild Solution
Press F5
```

## 📋 Quick Checklist

Before running, verify:
- [ ] 8 font files in `Resources/Fonts/` with **Build Action: Resource**
- [ ] `light-bg.jpg` exists with **Build Action: Resource**
- [ ] `dark-bg.jpg` added with **Build Action: Resource**
- [ ] `telegram.png` in `Resources/icon/` with **Build Action: Resource**
- [ ] All code files copied from `/wpf` folder
- [ ] Project rebuilt successfully

## 🎯 What You'll Get

After setup:
- 🔮 **Beautiful glassmorphism** - Transparent cards showing background
- 🎨 **Professional fonts** - SF Pro Rounded & Roboto
- 🌙 **Theme toggle** - Switch between light/dark backgrounds
- 📱 **Telegram button** - With your logo
- 🪟 **Window controls** - Minimize, maximize, close buttons
- ✨ **All 4 pages** - Login, Config, Running, Completion

## 📝 File Structure Summary

```
Your Project/
├── MainWindow.xaml          ← Updated (uses light-bg.jpg/dark-bg.jpg)
├── MainWindow.xaml.cs       ← Updated (theme switching)
├── App.xaml                 ← Updated
├── App.xaml.cs              ← Updated
├── Resources/
│   ├── Fonts/               ← CREATE & ADD 8 FONTS
│   ├── icon/
│   │   └── telegram.png     ← ADD THIS
│   ├── light-bg.jpg         ← YOU HAVE THIS ✅
│   ├── dark-bg.jpg          ← ADD THIS
│   └── Styles.xaml          ← Updated
├── Pages/
│   ├── LoginPage.*          ← Updated (SF Pro + Roboto fonts)
│   ├── ConfigPage.*         ← Updated
│   ├── RunningPage.*        ← Updated
│   └── CompletionPage.*     ← Updated
└── Helpers/
    ├── EdgeProfileDetector.cs
    └── SearchAutomation.cs
```

## 🆘 Need Help?

**Read these guides in order:**

1. **FONT_SETUP_GUIDE.md** - Detailed font installation
2. **IMAGE_SETUP_GUIDE.md** - Image setup and troubleshooting
3. **GLASSMORPHISM_FIX.md** - How the glass effect works
4. **FINAL_SETUP_SUMMARY.md** - Complete reference

## ❓ Common Questions

**Q: What if I don't have the fonts yet?**
A: Download from:
- SF Pro Rounded: Apple or font sites
- Roboto: https://fonts.google.com/specimen/Roboto

**Q: Can I use different background image names?**
A: The code expects `light-bg.jpg` and `dark-bg.jpg`. These are YOUR existing file names, so it should work!

**Q: What if telegram.png doesn't show?**
A: Make sure it's in `Resources/icon/` folder with Build Action: Resource

**Q: Fonts don't appear?**
A: Check all .ttf files have Build Action set to Resource, then rebuild

## 🚀 Ready to Go!

1. Add the 8 font files ✅
2. Add dark-bg.jpg ✅
3. Add telegram.png ✅
4. Copy all code files ✅
5. Set Build Actions ✅
6. Rebuild & Run ✅

Your beautiful glassmorphism WPF app will be ready! 🎨✨

---

**Note:** The duplicate `MainWindow_UseYourImages.*` files have been removed. All changes are now in the main `MainWindow.xaml` file.
