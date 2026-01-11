using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Less.Utils.MVVM
{
    /// <summary>
    /// Extend range method for <see cref="ObservableCollection{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        #region From https://github.com/microsoft/referencesource/blob/main/System/compmod/system/collections/objectmodel/observablecollection.cs
        private const string CountString = "Count";

        // This must agree with Binding.IndexerName.  It is declared separately
        // here so as to avoid a dependency on PresentationFramework.dll.
        private const string IndexerName = "Item[]";

        /// <summary>
        /// Helper to raise a PropertyChanged event  />).
        /// </summary>
        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        /// <inheritdoc />
        public ObservableCollectionEx() : base() { }

        /// <inheritdoc />
        public ObservableCollectionEx(IEnumerable<T> collection) : base(collection) { }

        /// <inheritdoc />
        public ObservableCollectionEx(List<T> list) : base(list) { }

        /// <summary>
        /// Add range to <see cref="ObservableCollection{T}"/>.
        /// </summary>
        /// <param name="values"></param>
        /// <exception cref="ArgumentNullException">When <paramref name="values"/> is <see langword="null"/>.</exception>
        public void AddRange(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            Contract.EndContractBlock();

            InsertRange(Count, values);
        }

        /// <summary>
        /// Insert <paramref name="values"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="values"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void InsertRange(int index, IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if ((uint)index > (uint)Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, $"Index {index} must be bigger than zero or less-than {Count}");
            }
            Contract.EndContractBlock();

            if (!values.Any())
            {
                return;
            }

            var originIndex = index;
            var inserted = new List<T>(values);
            foreach (var item in inserted)
            {
                Items.Insert(index++, item);
            }
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, inserted, originIndex));
        }

        /// <summary>
        /// Remove range start <paramref name="index"/> with <paramref name="count"/> long.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RemoveRange(int index, int count)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, $"Index {index} is less-than zero");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, $"Count {count} is less-than zero");
            }

            if (Count - index < count)
            {
                throw new ArgumentException("Remove range has invalid length.");
            }

            if (count == 0)
            {
                return;
            }

            Contract.EndContractBlock();
            var removedValues = new List<T>();
            for (; count > 0; count--)
            {
                removedValues.Add(Items[index]);
                Items.RemoveAt(index);
            }
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedValues, index));
        }
    }
}
