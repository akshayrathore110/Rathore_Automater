# Image Setup Guide - Step by Step

## 📁 Folder Structure in Visual Studio

Your project should look like this in **Solution Explorer**:

```
RathoreSearchAutomation (Your Project)
├── Properties
├── Resources/                    ← CREATE THIS FOLDER
│   ├── blue.png                 ← ADD THIS FILE
│   ├── black.png                ← ADD THIS FILE
│   └── icon/                    ← CREATE THIS SUBFOLDER
│       ├── sun.png              ← ADD THIS FILE
│       └── moon.png             ← ADD THIS FILE
├── Pages/
│   ├── LoginPage.xaml
│   ├── ConfigPage.xaml
│   └── ...
├── Helpers/
├── App.xaml
├── MainWindow.xaml
└── ...
```

## 🔧 Step-by-Step Setup

### Step 1: Create Resources Folder

1. In Visual Studio, **right-click** your project name in Solution Explorer
2. Select **Add** → **New Folder**
3. Name it exactly: `Resources`

### Step 2: Create icon Subfolder

1. **Right-click** the `Resources` folder you just created
2. Select **Add** → **New Folder**
3. Name it exactly: `icon` (lowercase!)

### Step 3: Add Background Images

1. **Right-click** the `Resources` folder
2. Select **Add** → **Existing Item...**
3. Browse to where you have your images
4. Select your **blue/pink gradient image**
5. Click **Add**
6. **IMPORTANT:** Rename it to exactly `blue.png` in Solution Explorer

Repeat for the dark image:
1. Right-click `Resources` again
2. Add → Existing Item
3. Select your **black/dark image**
4. Rename it to exactly `black.png`

### Step 4: Add Icon Images

1. **Right-click** the `Resources/icon` folder
2. Select **Add** → **Existing Item...**
3. Select your **sun icon**
4. Rename to exactly `sun.png`

Repeat for moon:
1. Right-click `Resources/icon` again
2. Add → Existing Item
3. Select your **moon icon**
4. Rename to exactly `moon.png`

### Step 5: Set Build Action (CRITICAL!)

For **EACH** of the 4 images:

1. **Right-click** the image file in Solution Explorer
2. Click **Properties** (or press F4)
3. In Properties window:
   - **Build Action** → Change to **Resource**
   - **Copy to Output Directory** → **Do not copy**

Repeat for:
- ✅ Resources/blue.png
- ✅ Resources/black.png  
- ✅ Resources/icon/sun.png
- ✅ Resources/icon/moon.png

### Step 6: Verify File Names

Double-check in Solution Explorer that files are named **exactly**:
- `blue.png` (not Blue.png or blue.jpg)
- `black.png` (not Black.png or black.jpg)
- `sun.png` (not Sun.png)
- `moon.png` (not Moon.png)

Capitalization matters!

## 🎨 What Each Image Is For

| Image | Purpose | When Shown |
|-------|---------|------------|
| `blue.png` | Light mode background | Default theme |
| `black.png` | Dark mode background | When theme toggled |
| `icon/sun.png` | Theme toggle icon | Shows in light mode |
| `icon/moon.png` | Theme toggle icon | Shows in dark mode |

## 🔍 How to Check It's Working

After adding all images:

1. **Build** → **Clean Solution**
2. **Build** → **Rebuild Solution**
3. Press **F5** to run

You should see:
- ✅ Blue/pink gradient background
- ✅ Sun icon in top-right corner
- ✅ Click sun icon → background changes to black, icon changes to moon
- ✅ Glassmorphism card visible

## ❌ Troubleshooting

### Problem: Images don't show

**Solution 1:** Check Build Action
- Right-click each image → Properties
- Build Action must be **Resource** (not Content, not None)

**Solution 2:** Check file paths in XAML
The code uses these exact paths:
```xml
pack://application:,,,/Resources/blue.png
pack://application:,,,/Resources/black.png
pack://application:,,,/Resources/icon/sun.png
pack://application:,,,/Resources/icon/moon.png
```

Make sure your files match these paths exactly!

**Solution 3:** Check file format
- Use PNG format (not JPG/JPEG for icons)
- JPG is okay for backgrounds but PNG is better

### Problem: Theme toggle doesn't work

1. Make sure both `blue.png` AND `black.png` exist
2. Check that sun.png and moon.png are in the `icon` subfolder
3. Rebuild the solution

### Problem: "File not found" error

This means the path is wrong. Check:
1. Folder name is `Resources` (capital R)
2. Subfolder is `icon` (lowercase)
3. File names match exactly (case-sensitive)

## 📝 Quick Checklist

Before running, verify:

- [ ] Resources folder exists in project
- [ ] icon subfolder exists inside Resources
- [ ] blue.png is in Resources folder
- [ ] black.png is in Resources folder
- [ ] sun.png is in Resources/icon folder
- [ ] moon.png is in Resources/icon folder
- [ ] All 4 images have Build Action: Resource
- [ ] All file names are exactly as listed (check spelling/caps)
- [ ] Project has been rebuilt

## 🎯 Result

After setup, your app will:
1. Launch with blue gradient background
2. Show glassmorphism login card
3. Have a sun icon in top-right (in window controls)
4. Click sun → switches to dark mode with black background and moon icon
5. All pages maintain theme throughout

The glassmorphism effect is now more prominent with better opacity and shadow!
