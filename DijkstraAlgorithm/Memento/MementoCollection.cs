using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm.Memento
{
    public class MementoCollection
    {
        //List<Memento> mementos = new List<Memento>();
        List<List<Memento>> mementosPhases = new List<List<Memento>>();

        public bool savePhase(IEnumerable<object> objs)
        {
            mementosPhases.Add(new List<Memento>());

            foreach (object obj in objs)
            {
                saveState(obj, mementosPhases.Count - 1);
            }
            return true;
        }

        private bool saveState(object obj, int index)
        {
            if (((Node) obj) != null)
            {
                MementoState st = new NodeState();
                st.setSave(obj);
                add(st, index);
                return true;
            }
            return false;
        }

        private void add(MementoState state, int index)
        {
            mementosPhases[index].Add(new Memento(state));
        }

        public List<Memento> getMementosPhase(int index)
        {
            if (mementosPhases.Count > index && index >= 0)
            {
                return mementosPhases[index];
            }
            return null;
        }

        public void removePhase(int index)
        {
            mementosPhases.RemoveAt(index);
        }

        public int amountOfMementosPhases()
        {
            return mementosPhases.Count;
        }
    }
}
