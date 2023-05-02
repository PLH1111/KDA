using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;


namespace KDA
{





    [StructLayout(LayoutKind.Sequential)]
    public class KeyboardData
    {
        public int KeyCode;  //定一个虚拟键码。该代码必须有一个价值的范围1至254   
        public int ScanCode; // 指定的硬件扫描码的关键     
        public int Flags;  // 键标志     
        public int Time; // 指定的时间戳记的这个讯息        
        public int ExtraInfo; // 指定额外信息相关的信息
    }

    public class KeyEventArgs
    {
        public Key Key { get; }

        public KeyEventArgs(Key key)
        {
            Key = key;
        }
    }

    public abstract class HookBase
    {

        public delegate int HookProc(int code, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        protected static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        protected static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        protected static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string name);

        public abstract int HookType { get; }

        private HookProc HookHandle;

        protected int hwndHook;


        public HookBase()
        {

        }

        ~HookBase()
        {
            Stop();
        }

        public virtual void Start()
        {
            if (hwndHook == 0)
            {
                HookHandle = HookProcCallback;
                string name = Process.GetCurrentProcess().MainModule.ModuleName;
                IntPtr handle = GetModuleHandle(name);
                hwndHook = SetWindowsHookEx(HookType, HookHandle, handle, 0);
                if (hwndHook == 0)
                {
                    Stop();
                }
            }
        }

        public virtual void Stop()
        {
            bool flag = true;
            if (hwndHook != 0)
            {
                flag = UnhookWindowsHookEx(hwndHook);
                hwndHook = 0;
            }
        }

        public abstract int HookProcCallback(int nCode, int wParam, IntPtr lParam);
    }
}
