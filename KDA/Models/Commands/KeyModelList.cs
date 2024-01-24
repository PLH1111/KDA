using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDA.Models.Commands
{
    public class KeyModelList : List<KeyModel>
    {
        public KeyModel this[Key key]
        {
            get => this.FirstOrDefault(x => x.Key == key);
        }
    }
}