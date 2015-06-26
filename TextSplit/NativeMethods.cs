using System;
using System.Runtime.InteropServices;

namespace TextSplit
{
    public class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotkey(IntPtr windowHandle, int hotkeyIdentifier, uint modifierCode, uint keyCode);
    }
}
