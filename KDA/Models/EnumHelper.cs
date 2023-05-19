using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models;
public static class EnumHelper
{
    public static List<T> ToList<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).OfType<T>().ToList();
    }
}
