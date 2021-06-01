using System.Collections;
using System.Collections.Generic;

namespace Platformer.Domain
{
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        public readonly T Value;
        public readonly SinglyLinkedList<T> Previous;

        public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
        {
            Value = value;
            Previous = previous;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return Value;
            var current = Previous;
            while (current != null)
            {
                yield return current.Value;
                current = current.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}