using System;
using System.Collections;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace TextSplit
{
    // The object used for storing the .tst file
    [Serializable()]
    public class TextSplitText : ISerializable
    {
        public ArrayList TextList { get; set; } // The texts on each slide
        public Font TextFont { get; set; } // The font of the text (name, size, style)
        public Color[] Colors { get; set; } // { font color, background color }
        public int[] Size { get; set; } // Size of the inner window { width, height }
        public int[] Margins { get; set; } // Space between inner window and textbox { Left, Right, Up, Down }
        public bool SlideWrap { get; set; } // Continuous slideshow indicator
        public Keys[] Hotkeys { get; set; } // Hotkeys

        public TextSplitText() {
        }

        public TextSplitText(TextSplitText other) {
            TextList = other.TextList;
            TextFont = other.TextFont;
            Colors = other.Colors;
            Size = other.Size;
            Margins = other.Margins;
            SlideWrap = other.SlideWrap;
            Hotkeys = other.Hotkeys;
        }

        public TextSplitText(SerializationInfo info, StreamingContext ctxt) {
            // v1.1 Attributes
            this.TextList = (ArrayList)info.GetValue("List", typeof(ArrayList));
            this.TextFont = (Font)info.GetValue("Font", typeof(Font));
            this.Colors = (Color[])info.GetValue("Colors", typeof(Color[]));
            this.Size = (int[])info.GetValue("Size", typeof(int[]));

            // v1.4 Attributes
            try {
                this.Margins = (int[])info.GetValue("Margins", typeof(int[]));
            } catch (Exception) { this.Margins = new int[] { 5, 5, 5, 5 }; }
            try {
                this.SlideWrap = (bool)info.GetValue("SlideWrap", typeof(bool));
            } catch (Exception) { this.SlideWrap = false; }
            try {
                this.Hotkeys = (Keys[])info.GetValue("Hotkeys", typeof(Keys[]));
            } catch (Exception) { this.Hotkeys = new Keys[8]; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) {
            info.AddValue("List", this.TextList);
            info.AddValue("Font", this.TextFont);
            info.AddValue("Colors", this.Colors);
            info.AddValue("Size", this.Size);
            info.AddValue("Margins", this.Margins);
            info.AddValue("SlideWrap", this.SlideWrap);
            info.AddValue("Hotkeys", this.Hotkeys);
        }

        public void Empty(string WelcomeText) {
            TextList = new ArrayList();
            TextList.Add(WelcomeText);
            TextFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            Colors = new Color[] { Color.Black, Color.White };
            Size = new int[] { 284, 262 };
            Margins = new int[] { 5, 5, 5, 5 };
            SlideWrap = false;
            Hotkeys = new Keys[8];
        }
    }
}
