using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm.Memento
{
    public class Memento
    {
        MementoState state;

        public Memento(MementoState state)
        {
            this.state = state;
        }

        public MementoState getState()
        {
            return state;
        }

        public void setState(MementoState state)
        {
            this.state = state;
        }
    }
}
