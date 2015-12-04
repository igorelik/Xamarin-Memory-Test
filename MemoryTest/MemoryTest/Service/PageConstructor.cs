using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace MemoryTest.Service
{
    // TODO: Move to some other folder/namespace?  Does this belong with helpers, or even pages?  Or even at the root?

    /// <summary>
    /// A wrapper class for creating Xamarin.Forms pages by invoking their constructor.
    /// </summary>
    public class PageConstructor
    {
        /// <summary>
        /// Gets the parameterless constructor for a page type.
        /// </summary>
        /// <param name="pageType">The page type</param>
        /// <returns>A page constructor that can be used to get an instance of the page</returns>
        public static PageConstructor GetParameterlessConstructor(Type pageType)
        {
            var constructorInfo = pageType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(c => !c.GetParameters().Any());

            return new PageConstructor(pageType, constructorInfo);
        }

        /// <summary>
        /// Gets the single parameter constructor for a page type.
        /// </summary>
        /// <param name="pageType">The page type</param>
        /// <param name="parameter">The parameter for the constructor</param>
        /// <returns>A page constructor that can be used to get an instance of the page</returns>
        public static PageConstructor GetSingleParameterConstructor(Type pageType, object parameter)
        {
            var constructorInfo = pageType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(c =>
                {
                    var p = c.GetParameters();
                    return p.Count() == 1 && p[0].ParameterType == parameter.GetType();
                });

            return new PageConstructor(pageType, constructorInfo, parameter);
        }

        /// <summary>
        /// Creates an instance of the page, using the type, constructor, and parameter information in this class.
        /// </summary>
        /// <returns>The page instance</returns>
        public Page CreateInstance()
        {
            if (ConstructorInfo == null)
            {
                throw new InvalidOperationException("No suitable constructor found for page " + PageType.Name);
            }

            var page = ConstructorInfo.Invoke(Parameters) as Page;
            if (page == null)
            {
                throw new InvalidOperationException("Cannot get a page instance. Is the type " + PageType.Name + " a valid Xamarin.Forms page?");
            }

            return page;
        }

        private PageConstructor(Type pageType, ConstructorInfo constructorInfo, object parameter = null)
        {
            PageType = pageType;
            ConstructorInfo = constructorInfo;
            Parameters = (parameter == null)
                ? new object[] { }
                : new object[] { parameter };
        }

        public Type PageType { get; private set; }
        public ConstructorInfo ConstructorInfo { get; private set; }
        public object[] Parameters { get; private set; }
    }
}
