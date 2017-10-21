using System;

namespace DZ_1
{
    public class IntegerList : IIntegerList
    {
        private int[] _internalStorage;
        private int _lastIndex;

        public IntegerList()
        {
            _internalStorage = new int[4];
            _lastIndex = -1;
        }

        public IntegerList(int initialSize)
        {
            _internalStorage = new int[initialSize];
            _lastIndex = -1;
        }


        public void Add(int item)
        {
            _lastIndex++;
            if (_internalStorage.Length == _lastIndex) Array.Resize(ref _internalStorage, _lastIndex + 1);
            _internalStorage[_lastIndex] = item;
        }

        public bool Remove(int item)
        {
            for(int i=0;i<=_lastIndex;i++)
            {
                if (_internalStorage[i] == item)
                {
                    for (int j = i; j < _lastIndex; j++)
                    {
                        _internalStorage[j] = _internalStorage[j + 1];
                    }
                    _lastIndex--;
                    return true;
                }
            }
            return false;
        }

        public bool RemoveAt(int index)
        {
            if(index>_lastIndex || index<0) throw new IndexOutOfRangeException();
            for (int i = index; i <= _lastIndex; i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }
            _lastIndex--;
            return true;
        }

        public int GetElement(int index)
        {
            if (index > _lastIndex) throw new IndexOutOfRangeException();
            return _internalStorage[index];
        }

        public int IndexOf(int item)
        {
            for (int i = 0; i < _lastIndex; i++)
            {
                if (_internalStorage[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public int Count { get => _lastIndex + 1; }

        public void Clear()
        {
            _lastIndex = -1;
        }

        public bool Contains(int item)
        {
            for (int i = 0; i <= _lastIndex; i++)
            {
                if(_internalStorage[i]==item) return true;
            }
            return false;
        }
    }
}
