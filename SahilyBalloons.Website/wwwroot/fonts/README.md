# SF Pro Fonts Installation Guide

## The fonts folder is ready at: `wwwroot/fonts/`

## How to get the font files:

### Option 1: Extract from .pkg using TransMac (Recommended for Windows)
1. Download TransMac trial: https://www.acutesystems.com/scrtm.htm
2. Open the `SF Pro Fonts.pkg` file with TransMac
3. Extract the font files to the `wwwroot/fonts/` folder

### Option 2: Online Conversion
The .pkg files contain .otf fonts that need to be converted to .woff2 for web use:

1. You need to extract the .pkg first using a tool like:
   - **The Unarchiver** (if you have access to a Mac)
   - **TransMac** on Windows
   - Or use https://extract.me/ to upload and extract the .pkg

2. Once you have the .otf files, convert them to .woff2 at:
   - https://cloudconvert.com/otf-to-woff2
   - https://everythingfonts.com/otf-to-woff2

3. You need these specific files (Semibold weight):
   - `SF-Pro-Display-Semibold.otf` → convert to → `SF-Pro-Display-Semibold.woff2`
   - `SF-Pro-Text-Semibold.otf` → convert to → `SF-Pro-Text-Semibold.woff2`

4. Optional (for full coverage):
   - SF-Pro-Display-Regular.woff2
   - SF-Pro-Display-Medium.woff2
   - SF-Pro-Display-Bold.woff2
   - SF-Pro-Text-Regular.woff2
   - SF-Pro-Text-Medium.woff2

5. Place the converted .woff2 files in: `wwwroot/fonts/`

### Option 3: Use Alternative Font (Quick Fix)
If you want a similar look without SF Pro, I can update the CSS to use **Inter** or **Roboto** fonts from Google Fonts which are very similar.

## Files Created:
- ✅ `/wwwroot/fonts/` folder created
- ✅ `/wwwroot/css/fonts.css` with @font-face declarations ready
- ✅ `_Layout.cshtml` updated to include fonts.css

## Next Steps:
Once you place the font files in `/wwwroot/fonts/`, rebuild and the fonts will load automatically.
