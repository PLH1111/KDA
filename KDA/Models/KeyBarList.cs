using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models
{
    public class KeyBarList : List<KeyBar>
    {
        public KeyBarList()
        {
        }

        public KeyBarList(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(new KeyBar());
            }
        }
    }
}
