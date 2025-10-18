# Updated Instructions - Theme Fix & Window Controls

## ✅ What's Been Fixed

1. **Theme Changing** - Now works properly with your background images
2. **Window Controls** - Added Minimize, Maximize/Restore, Close buttons
3. **Custom Title Bar** - App title and controls in the top bar
4. **Icon Support** - Uses your sun.png and moon.png from Resources/icon folder

## 📁 Required File Structure

Make sure your files are organized like this in your Visual Studio project:

```
RathoreSearchAutomation/
├── Resources/
│   ├── blue.png          ← Light mode background (your blue gradient)
│   ├── black.png         ← Dark mode background (your black image)
│   └── icon/
│       ├── sun.png       ← Sun icon for light mode toggle
│       └── moon.png      ← Moon icon for dark mode toggle
```

## 🔧 Setup Steps

### Step 1: Add Your Images

1. Copy your images to the Resources folder:
   - `blue.png` (the blue/purple gradient - for light mode)
   - `black.png` (the black/dark image - for dark mode)

2. Create `Resources/icon/` folder and add:
   - `sun.png` (the sun icon you showed me)
   - `moon.png` (moon icon - if you don't have one, I can help create it)

### Step 2: Set Image Properties

For **ALL** images in Resources folder:
1. Right-click each image in Solution Explorer
2. Properties → **Build Action** → **Resource**
3. **Copy to Output Directory** → **Do not copy** (Resource embeds them)

### Step 3: Replace MainWindow Files

Replace these files with the updated versions:
- `MainWindow.xaml`
- `MainWindow.xaml.cs`

### Step 4: Build and Run

1. **Build** → **Clean Solution**
2. **Build** → **Rebuild Solution**
3. Press **F5** to run

## 🎨 Theme Changing

The theme now works like this:

**Light Mode (Default):**
- Background: `blue.png` (your blue gradient)
- Overlay: Light white tint (#1AFFFFFF)
- Icon: Sun icon visible
- Text: Dark colors

**Dark Mode:**
- Background: `black.png` (your black image)
- Overlay: Dark tint (#33000000)
- Icon: Moon icon visible
- Text: Light colors

Click the **theme toggle button** (top-right, next to minimize) to switch.

## 🪟 Window Controls

**Title Bar (Top):**
- **Left**: App title "RATHORE Search Automation"
- **Right**: Theme toggle, Minimize, Maximize, Close buttons

**Buttons:**
- 🌙/☀️ **Theme Toggle** - Switch between light/dark mode
- **—** **Minimize** - Minimize window to taskbar
- **☐** **Maximize** - Maximize/restore window (icon changes when maximized)
- **✕** **Close** - Exit application

**Dragging:**
- You can drag the window by clicking and holding anywhere on the title bar

## 🐛 Troubleshooting

### Images Not Loading

If backgrounds don't show:

1. **Check image names match exactly:**
   - `Resources/blue.png` (not Blue.png or blue.jpg)
   - `Resources/black.png`
   - `Resources/icon/sun.png`
   - `Resources/icon/moon.png`

2. **Verify Build Action:**
   - All images should be **Build Action: Resource**

3. **Check Output window** for errors when running

### Theme Not Changing

1. Make sure both `blue.png` and `black.png` exist
2. Click the theme toggle button (sun/moon icon, top right)
3. Check Output window for any errors

### Moon Icon Missing

If you don't have a moon.png icon, you can:
- Download one from icon sites (like flaticon.com)
- Use the same sun.png temporarily
- Or create a simple moon image in Paint (white crescent on transparent background)

## 📝 Alternative: Use Fallback Colors

If you're having trouble with images, the code has a fallback:
- Light mode: Blue-purple gradient (generated)
- Dark mode: Dark gray solid color

The app will automatically use these if images aren't found.

## 🎯 Expected Behavior

After setup:

1. ✅ App launches with blue background (light mode)
2. ✅ Click theme toggle → switches to black background (dark mode)
3. ✅ Sun/moon icons swap when toggling
4. ✅ All text colors update appropriately
5. ✅ Window controls (min/max/close) work
6. ✅ Can drag window by title bar
7. ✅ Glassmorphism effects visible on all pages

## 📸 Image Specifications

For best results, use images with these specs:

**Background Images:**
- Format: PNG or JPG
- Size: 1920x1080 or higher
- blue.png: Bright, colorful gradient
- black.png: Dark, subtle texture

**Icon Images:**
- Format: PNG with transparency
- Size: 512x512 or 256x256
- Simple, clear icons
- High contrast

---

Your app should now have fully working theme switching and window controls! 🚀
