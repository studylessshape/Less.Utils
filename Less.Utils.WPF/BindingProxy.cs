using System.Windows;

namespace Less.Utils.WPF
{
    /// <summary>
    /// <seealso href="https://www.cnblogs.com/czwy/p/17638959.html"/>
    /// </summary>
    public class BindingProxy : Freezable
    {
        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        /// <summary>
        /// Bind data
        /// </summary>
        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// Using a <see cref="DependencyProperty"/> as the backing store for <see cref="Data"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));
    }
}
