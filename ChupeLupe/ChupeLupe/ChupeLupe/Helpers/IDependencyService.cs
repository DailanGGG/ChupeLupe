using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChupeLupe.Helpers
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }

    public class DependencyServiceWrapper : IDependencyService
    {
        public T Get<T>() where T : class
        {
            return DependencyService.Get<T>();
        }
    }
}
