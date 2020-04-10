using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm.Memento
{
    public interface MementoState
    {
        Object getSave();
        void setSave(Object obj);
    }
}
