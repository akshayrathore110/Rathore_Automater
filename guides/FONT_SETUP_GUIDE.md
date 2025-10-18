# Font Setup Guide - SF Pro Rounded & Roboto

## 📁 Folder Structure

Create this structure in your Visual Studio project:

```
RathoreSearchAutomation/
├── Resources/
│   ├── Fonts/                    ← CREATE THIS FOLDER
│   │   ├── SF-Pro-Rounded-Bold.ttf
│   │   ├── SF-Pro-Rounded-Semibold.ttf
│   │   ├── SF-Pro-Rounded-Medium.ttf
│   │   ├── SF-Pro-Rounded-Regular.ttf
│   │   ├── Roboto-Bold.ttf
│   │   ├── Roboto-Medium.ttf
│   │   ├── Roboto-Regular.ttf
│   │   └── Roboto-Light.ttf
│   ├── icon/
│   │   ├── sun.png
│   │   ├── moon.png
│   │   └── telegram.png          ← ADD YOUR TELEGRAM LOGO HERE
│   ├── light-bg.jpg
│   └── dark-bg.jpg
```

## 🔧 Step-by-Step Setup

### Step 1: Create Fonts Folder

1. In Visual Studio **Solution Explorer**
2. **Right-click** `Resources` folder
3. **Add** → **New Folder**
4. Name it: `Fonts`

### Step 2: Get Font Files

**SF Pro Rounded:**
- Download from Apple's website or font repositories
- You need these weights:
  - SF-Pro-Rounded-Bold.ttf
  - SF-Pro-Rounded-Semibold.ttf
  - SF-Pro-Rounded-Medium.ttf
  - SF-Pro-Rounded-Regular.ttf

**Roboto:**
- Download from Google Fonts: https://fonts.google.com/specimen/Roboto
- You need these weights:
  - Roboto-Bold.ttf
  - Roboto-Medium.ttf
  - Roboto-Regular.ttf
  - Roboto-Light.ttf

### Step 3: Add Font Files to Project

1. **Right-click** the `Fonts` folder in Visual Studio
2. **Add** → **Existing Item...**
3. **Browse** to your font files
4. **Select all** .ttf files
5. Click **Add**

### Step 4: Set Font Properties (IMPORTANT!)

For **EACH** .ttf file:

1. **Right-click** the font file in Solution Explorer
2. Click **Properties** (or press F4)
3. Set these properties:
   - **Build Action** → **Resource**
   - **Copy to Output Directory** → **Do not copy**

Repeat for all 8 font files!

### Step 5: Add Telegram Logo

1. Put your `telegram.png` in the `Resources/icon/` folder
2. **Right-click** `telegram.png`
3. **Properties**:
   - **Build Action** → **Resource**
   - **Copy to Output Directory** → **Do not copy**

### Step 6: Verify Font Names

After adding fonts, you need to check the **internal font family name**.

**Method 1: Install and Check (Easiest)**
1. Double-click each .ttf file
2. Look at the font name shown in Windows Font Viewer
3. Note the exact name (e.g., "SF Pro Rounded" or "SFProRounded")

**Method 2: Use Font Tool**
- Use a tool like FontForge to check the font family name

### Step 7: Update XAML if Needed

If your font's internal name is different, update the XAML:

```xaml
<!-- If internal name is "SFProRounded" (no spaces): -->
<FontFamily x:Key="SFProRounded">pack://application:,,,/Resources/Fonts/#SFProRounded</FontFamily>

<!-- If internal name is "SF Pro Rounded" (with spaces): -->
<FontFamily x:Key="SFProRounded">pack://application:,,,/Resources/Fonts/#SF Pro Rounded</FontFamily>
```

## 📝 Font Usage in App

### Where Each Font is Used:

**SF Pro Rounded (Headings & Buttons):**
- "RATHORE" title
- "Welcome" text
- All button text
- Window title bar

**Roboto (Body Text & Labels):**
- Input labels (User ID, Key, etc.)
- Input field text
- Subtitles
- Descriptions
- General body text

## 🎨 Font Weights Used

| Font | Weights |
|------|---------|
| SF Pro Rounded | Bold (RATHORE title), SemiBold (headings), Medium (buttons) |
| Roboto | Medium (labels), Regular (inputs), Light (subtitles) |

## ✅ Quick Checklist

Before building:

- [ ] `Resources/Fonts/` folder created
- [ ] All 8 .ttf files added to Fonts folder
- [ ] All fonts have **Build Action: Resource**
- [ ] `telegram.png` added to `Resources/icon/`
- [ ] `telegram.png` has **Build Action: Resource**
- [ ] Font family names match in XAML
- [ ] Project rebuilt successfully

## 🔍 Testing Fonts

After setup:

1. **Build** → **Clean Solution**
2. **Build** → **Rebuild Solution**
3. Press **F5** to run

You should see:
- ✅ "RATHORE" in SF Pro Rounded Bold
- ✅ "Welcome" in SF Pro Rounded SemiBold
- ✅ Labels in Roboto Medium
- ✅ Telegram logo visible on button

## ❌ Troubleshooting

### Fonts Don't Appear

**Problem:** Text shows in default font (Segoe UI)

**Solution 1:** Check Build Action
- All .ttf files must have **Build Action: Resource**

**Solution 2:** Check Font Family Name
- Open .ttf file to see the actual font name
- Update XAML to match exactly:
  ```xaml
  <FontFamily x:Key="SFProRounded">pack://application:,,,/Resources/Fonts/#[EXACT NAME HERE]</FontFamily>
  ```

**Solution 3:** Rebuild
- Clean and rebuild the solution
- Fonts are embedded at build time

### Telegram Logo Not Showing

1. Check `telegram.png` is in `Resources/icon/` folder
2. Check Build Action is **Resource**
3. Check file name is exactly `telegram.png` (lowercase)
4. Rebuild solution

### Build Errors

If you get "Cannot find resource" errors:
1. Right-click project → **Unload Project**
2. Right-click → **Edit Project File**
3. Look for `<Resource Include="..."/>` entries
4. Make sure all fonts are listed
5. Right-click → **Reload Project**

## 📋 Alternative: Embedded Fonts Code

If the simple method doesn't work, you can reference fonts by filename:

```xaml
<TextBlock FontFamily="pack://application:,,,/Resources/Fonts/#SF-Pro-Rounded-Bold.ttf#SF Pro Rounded"
           FontWeight="Bold"/>
```

Format: `pack://application:,,,/Resources/Fonts/#[FILENAME].ttf#[FONT NAME]`

## 🎯 Expected Result

After setup, your app will have:
- Professional SF Pro Rounded font for titles and buttons
- Clean Roboto font for body text
- Telegram logo on the button (instead of black circle)
- Consistent typography throughout all pages

## 📸 Font File Specifications

**Recommended:**
- Format: TrueType (.ttf) or OpenType (.otf)
- Size: Usually 50-200KB per file
- Make sure to get the official versions for best quality

**Where to Download:**
- **SF Pro Rounded:** Apple Developer (requires free account) or font repositories
- **Roboto:** Google Fonts (100% free)

---

Your app will look professional with proper typography! 🎨✨
