using System.Collections;
using System.Collections.Generic;

namespace Less.Utils.Linq
{
    internal class FlatEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<IEnumerable<T>> _items;

        public FlatEnumerable(IEnumerable<IEnumerable<T>> items)
        {
            _items = items;
        }

        private class FlatEnumerator : IEnumerator<T>
        {
            private readonly IEnumerator<IEnumerable<T>> _items;
            private IEnumerator<T> currentEnumertor;
            public T Current
            {
                get
                {
                    if (currentEnumertor == null)
                    {
                        return default;
                    }

                    return currentEnumertor.Current;
                }
            }

            object IEnumerator.Current => Current;

            public FlatEnumerator(IEnumerable<IEnumerable<T>> items)
            {
                this._items = items.GetEnumerator();
            }

            public void Dispose()
            {
                currentEnumertor?.Dispose();
                _items.Dispose();
            }

            public bool MoveNext()
            {
                if (_items == null)
                {
                    return false;
                }

                if (currentEnumertor == null || !currentEnumertor.MoveNext())
                {
                    currentEnumertor = null;

                    while (_items.MoveNext())
                    {
                        if (_items.Current == null) continue;

                        currentEnumertor = _items.Current.GetEnumerator();
                        // skip empty enumerator
                        if (!currentEnumertor.MoveNext())
                        {
                            continue;
                        }

                        break;
                    }

                    if (currentEnumertor == null)
                    {
                        return false;
                    }
                }

                return true;
            }

            public void Reset()
            {
                currentEnumertor = null;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FlatEnumerator(_items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
