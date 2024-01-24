using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace KDA.Hooks
{

    public class KeyboardHook : HookBase
    {
        public event EventHandler<KeyEventArgs> OnKeyDown;

        public event EventHandler<KeyEventArgs> OnKeyUp;

        public override int HookType => 13;

        public KeyboardHook()
        {

        }

        public override int HookProcCallback(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KeyboardData data = (KeyboardData)Marshal.PtrToStructure(lParam, typeof(KeyboardData));

                KeyEventArgs keyEventArgs = new KeyEventArgs(KeyInterop.KeyFromVirtualKey(data.KeyCode));

                switch (wParam)
                {
                    case 256:
                    case 260:
                        OnKeyDown?.Invoke(this, keyEventArgs);
                        break;
                    case 257:
                    case 261:
                        OnKeyUp?.Invoke(this, keyEventArgs);
                        break;
                }
            }
            return CallNextHookEx(hwndHook, nCode, wParam, lParam);
        }
    }
}