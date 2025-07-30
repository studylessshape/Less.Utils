using System;

namespace Less.Utils.Mapper
{
    /// <summary>
    /// Map current class to <see cref="To"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MapToAttribute : Attribute
    {
        /// <summary>
        /// Wanted type map to
        /// </summary>
        public Type To { get; }

        /// <summary>
        /// Specify to type
        /// </summary>
        /// <param name="to"></param>
        public MapToAttribute(Type to)
        {
            To = to;
        }
    }
}
