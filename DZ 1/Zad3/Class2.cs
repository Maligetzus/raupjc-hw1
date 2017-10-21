using System.Collections;
using System.Collections.Generic;

namespace Zad3
{
    public class GenericListEnumerator<X> : IEnumerator<X>
    {
        private int index;
        private GenericList<X> list;

        public GenericListEnumerator(GenericList<X> list)
        {
            index = -1;
            this.list = list;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            index++;
            return index < list.GetlastIndex;
        }

        public void Reset()
        {
            index = -1;
        }

        public X Current { get => list.GetinternalStorage[index]; }

        object IEnumerator.Current { get => Current; }
    }
}