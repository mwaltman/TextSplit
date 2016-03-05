using System.Drawing;

namespace TextSplit
{
    public class Theme
    {
        public string Name { get; set; }
        public Font Font { get; set; }
        public Color ColorT { get; set; }
        public Color ColorB { get; set; }

        public Theme() {
        }

        public Theme(string name, Font font, Color colorT, Color colorB) {
            Name = name;
            Font = font;
            ColorT = colorT;
            ColorB = colorB;
        }

        public void Apply(TextSplitText tst, bool displaySlide) {
            tst.TextFont = Font;
            tst.Colors = new Color[] { ColorT, ColorB };
            if (displaySlide)
                Globals.CurrentWindow.DisplaySlide();
            Globals.CurrentWindow.ChangeFilenameUnsaved();
        }

        public string PackToString() {
            string s = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                Name,
                Font.Name, Font.Size, (int)Font.Style,
                ColorT.R, ColorT.G, ColorT.B,
                ColorB.R, ColorB.G, ColorB.B);
            return s;
        }
    }
}
