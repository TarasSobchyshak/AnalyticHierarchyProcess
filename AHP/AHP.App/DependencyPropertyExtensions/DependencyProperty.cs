using System;
using System.Linq.Expressions;
using System.Windows;

namespace AHP.App.DependencyPropertyExtensions
{
    public static class DependencyProperty<T> where T : DependencyObject
    {
        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            return Register<TProperty>(propertyExpression, default(TProperty), null);
        }

        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty defaultValue)
        {
            return Register<TProperty>(propertyExpression, defaultValue, null);
        }

        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression, Func<T, PropertyChangedCallback<TProperty>> propertyChangedCallbackFunc)
        {
            return Register<TProperty>(propertyExpression, default(TProperty), propertyChangedCallbackFunc);
        }

        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty defaultValue, Func<T, PropertyChangedCallback<TProperty>> propertyChangedCallbackFunc)
        {
            string propertyName = propertyExpression.RetrieveMemberName();
            PropertyChangedCallback callback = ConvertCallback(propertyChangedCallbackFunc);

            return DependencyProperty.Register(propertyName, typeof(TProperty), typeof(T), new PropertyMetadata(defaultValue, callback));
        }

        private static PropertyChangedCallback ConvertCallback<TProperty>(Func<T, PropertyChangedCallback<TProperty>> propertyChangedCallbackFunc)
        {
            if (propertyChangedCallbackFunc == null)
                return null;
            return new PropertyChangedCallback((d, e) =>
            {
                PropertyChangedCallback<TProperty> callback = propertyChangedCallbackFunc((T)d);
                if (callback != null)
                    callback(new DependencyPropertyChangedEventArgs<TProperty>(e));
            });
        }
    }

    public delegate void PropertyChangedCallback<TProperty>(DependencyPropertyChangedEventArgs<TProperty> e);
}
