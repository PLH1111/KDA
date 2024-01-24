using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDA.Models
{
    public class FFTBarList : List<FFTBar>
    {
        public FFTBarList()
        {
        }

        public FFTBarList(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(new FFTBar());
            }
        }
    }
}
