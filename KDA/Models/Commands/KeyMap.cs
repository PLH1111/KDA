﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDA.Models.Commands
{
    public class KeyMap
    {
        public static Dictionary<byte, Key> Keys { get; set; } = new()
        {
            { 0x00,Key.Escape},
            { 0x02,Key.F1},
            { 0x03,Key.F2},
            { 0x04,Key.F3},
            { 0x05,Key.F4},
            { 0x06,Key.F5},
            { 0x07,Key.F6},
            { 0x08,Key.F7},
            { 0x09,Key.F8},
            { 0x0A,Key.F9},
            { 0x0B,Key.F10},
            { 0x0C,Key.F11},
            { 0x0D,Key.F12},
            { 0x0E,Key.PrintScreen},
            { 0x0F,Key.Scroll},

            { 0x10,Key.Pause},
       
            { 0x15,Key.Oem3},
            { 0x16,Key.D1},
            { 0x17,Key.D2},
            { 0x18,Key.D3},
            { 0x19,Key.D4},
            { 0x1A,Key.D5},
            { 0x1B,Key.D6},
            { 0x1C,Key.D7},
            { 0x1D,Key.D8},
            { 0x1E,Key.D9},
            { 0x1F,Key.D0},

            { 0x20,Key.OemMinus},
            { 0x21,Key.OemPlus},
            { 0x22,Key.Back},
            { 0x23,Key.Insert},
            { 0x24,Key.Home},
            { 0x25,Key.PageUp},
            { 0x26,Key.NumLock},
            { 0x27,Key.Divide},
            { 0x28,Key.Multiply},
            { 0x29,Key.Subtract},
            { 0x2A,Key.Tab},
            { 0x2B,Key.Q},
            { 0x2C,Key.W},
            { 0x2D,Key.E},
            { 0x2E,Key.R},
            { 0x2F,Key.T},

            { 0x30,Key.Y},
            { 0x31,Key.U},
            { 0x32,Key.I},
            { 0x33,Key.O},
            { 0x34,Key.P},
            { 0x35,Key.Oem4},
            { 0x36,Key.Oem6},
            { 0x37,Key.Oem5},
            { 0x38,Key.Delete},
            { 0x39,Key.End},
            { 0x3A,Key.PageDown},
            { 0x3B,Key.NumPad7},
            { 0x3C,Key.NumPad8},
            { 0x3D,Key.NumPad9},
            { 0x3E,Key.Add},
            { 0x3F,Key.CapsLock},

            { 0x40,Key.A},
            { 0x41,Key.S},
            { 0x42,Key.D},
            { 0x43,Key.F},
            { 0x44,Key.G},
            { 0x45,Key.H},
            { 0x46,Key.J},
            { 0x47,Key.K},
            { 0x48,Key.L},
            { 0x49,Key.OemSemicolon},
            { 0x4A,Key.OemQuotes},
            { 0x4B,Key.Enter},

            { 0x50,Key.NumPad4},
            { 0x51,Key.NumPad5},
            { 0x52,Key.NumPad6},
            { 0x53,Key.Add},
            { 0x54,Key.LeftShift},
            { 0x55,Key.Z},
            { 0x56,Key.X},
            { 0x57,Key.C},
            { 0x58,Key.V},
            { 0x59,Key.B},
            { 0x5A,Key.N},
            { 0x5B,Key.M},
            { 0x5C,Key.OemComma},
            { 0x5D,Key.OemPeriod},
            { 0x5E,Key.OemQuestion},


            { 0x61,Key.RightShift},
            { 0x63,Key.Up},
            { 0x65,Key.NumPad1},
            { 0x66,Key.NumPad2},
            { 0x67,Key.NumPad3},
            { 0x68,Key.Return},
            { 0x69,Key.LeftCtrl},
            { 0x6A,Key.LWin},
            { 0x6B,Key.LeftAlt},
            { 0x6E,Key.Space},

            { 0x72,Key.RightAlt},
            //{ 0x73,Key.LWin},
            { 0x74,Key.Apps},
      
            { 0x76,Key.RightCtrl},
            { 0x77,Key.Left},
            { 0x78,Key.Down},
            { 0x79,Key.Right},
            { 0x7A,Key.NumPad0},
            { 0x7C,Key.Decimal},
        };
    }
}
