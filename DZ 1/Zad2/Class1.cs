using System;

namespace Zad2
{
    public class GenericList<X> : IGenericList<X>, IEquatable<X>
    {
        private X[] _internalStorage;
        private int _lastIndex;

        public GenericList()
        {
            _internalStorage = new X[4];
            _lastIndex = -1;
        }

        public GenericList(int initialSize)
        {
            _internalStorage = new X[initialSize];
            _lastIndex = -1;
        }


        public void Add(X item)
        {
            _lastIndex++;
            if (_internalStorage.Length == _lastIndex) Array.Resize(ref _internalStorage, _lastIndex + 1);
            _internalStorage[_lastIndex] = item;
        }

        public bool Remove(X item)
        {
            for (int i = 0; i <= _lastIndex; i++)
            {
                if (Object.Equals(_internalStorage[i], item))
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
            if (index > _lastIndex || index < 0) throw new IndexOutOfRangeException();
            for (int i = index; i <= _lastIndex; i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];
            }
            _lastIndex--;
            return true;
        }

        public X GetElement(int index)
        {
            if (index > _lastIndex) throw new IndexOutOfRangeException();
            return _internalStorage[index];
        }

        public int IndexOf(X item)
        {
            for (int i = 0; i < _lastIndex; i++)
            {
                if (Object.Equals(_internalStorage[i], item))
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

        public bool Contains(X item)
        {
            for (int i = 0; i <= _lastIndex; i++)
            {
                if (Object.Equals(_internalStorage[i], item)) return true;
            }
            return false;
        }

        protected bool Equals(GenericList<X> other)
        {
            return Equals(_internalStorage, other._internalStorage) && _lastIndex == other._lastIndex;
        }

        public bool Equals(X other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GenericList<X>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_internalStorage != null ? _internalStorage.GetHashCode() : 0) * 397) ^ _lastIndex;
            }
        }

        public static bool operator ==(GenericList<X> left, GenericList<X> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GenericList<X> left, GenericList<X> right)
        {
            return !Equals(left, right);
        }
    }
}
