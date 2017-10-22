using System;
using System.Collections;
using System.Collections.Generic;

namespace Zad3
{
    public class GenericList<X> : IGenericList<X>, IEquatable<X>
    {
        private X[] _internalStorage;
        private int _lastIndex = -1;

        public GenericList()
        {
            _internalStorage = new X[4];
        }

        public GenericList(int initialSize)
        {
            _internalStorage = new X[initialSize];
        }


        public void Add(X item)
        {
            if (_internalStorage.Length - 1 == _lastIndex)
            {
                Array.Resize(ref _internalStorage, _internalStorage.Length + 1);
                _internalStorage[_internalStorage.Length - 1] = item;
            }
            else _internalStorage[_lastIndex + 1] = item;
            _lastIndex++;
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
            for (int i = index; i < _lastIndex; i++)
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
            for (int i = 0; i <= _lastIndex; i++)
            {
                if (Object.Equals(_internalStorage[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        public int Count{ get => _lastIndex + 1; }

        public X[] GetinternalStorage { get => _internalStorage; }

        public int GetlastIndex { get => _lastIndex; }

        public void Clear()
        {
            _lastIndex = -1;
        }

        public bool Contains(X item)
        {
            for (int i = 0; i < _lastIndex; i++)
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

        // IEnumerable <X> implementation
        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
