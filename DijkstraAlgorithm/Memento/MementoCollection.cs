using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm.Memento
{
    public class MementoCollection
    {
        List<Memento> mementos = new List<Memento>();

        public bool saveState(object obj)
        {
            if (((Node) obj) != null)
            {
                MementoState st = new NodeState();
                st.setSave(obj);
                add(st);
                return true;
            }
            return false;
        }

        public void add(MementoState state)
        {
            mementos.Add(new Memento(state));
        }

        public List<Memento> getMementos()
        {
            return mementos;
        }

        public void remove(int index)
        {
            mementos.RemoveAt(index);
        }
    }
}
