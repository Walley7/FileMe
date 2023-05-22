using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace FileMe.Utility {

    public static class UColor {
        // AVERAGE ================================================================================
        //--------------------------------------------------------------------------------
        public static int Average(Color colour) {
            return ((int)colour.R + (int)colour.G + (int)colour.B) / 3;
        }


        // BRIGHTNESS ================================================================================
        //--------------------------------------------------------------------------------
        public static Color Brighten(Color colour, int amount) {
            HSB hsb = ToHSB(colour);
            hsb.brightness = Math.Min(hsb.brightness + amount, 240);
            return FromHSB(hsb);
        }
        
        //--------------------------------------------------------------------------------
        public static Color Darken(Color colour, int amount) {
            HSB hsb = ToHSB(colour);
            hsb.brightness = Math.Max(hsb.brightness - amount, 0);
            return FromHSB(hsb);
        }
        
        //--------------------------------------------------------------------------------
        public static double WeightedBrightness(Color colour) {
            return (0.2126 * (double)colour.R / 255.0 + 0.7152 + (double)colour.G / 255.0 + 0.0722 * (double)colour.B / 255.0) / 3;
        }


        // HSB ================================================================================
        //--------------------------------------------------------------------------------
        public static HSB ToHSB(Color colour) {
            HSB hsb = new HSB();
            ColorRGBToHLS(colour.ToArgb(), out hsb.hue, out hsb.brightness, out hsb.saturation);
            return hsb;
        }

        //--------------------------------------------------------------------------------
        public static Color FromHSB(int hue, int saturation, int brightness) {
            return Color.FromArgb(255, Color.FromArgb(ColorHLSToRGB(hue, brightness, saturation)));
        }

        //--------------------------------------------------------------------------------
        public static Color FromHSB(HSB hsb) {
            return FromHSB(hsb.hue, hsb.saturation, hsb.brightness);
        }
        
        //--------------------------------------------------------------------------------
        [DllImport("shlwapi.dll")] private static extern int ColorRGBToHLS(int rgb, out int h, out int l, out int s);
        [DllImport("shlwapi.dll")] private static extern int ColorHLSToRGB(int h, int l, int s);

            
        //================================================================================
        //********************************************************************************
        public struct HSB {
            public int hue;
            public int saturation;
            public int brightness;
        }
    }

}
