using System;
using Ninject;
using Ninject.Modules;

namespace Triggerfish.Ninject
{
	/// <summary>
	/// Object factory using Ninject
	/// </summary>
    public static class ObjectFactory
    {
		private static readonly IKernel m_kernel = new StandardKernel();

		/// <summary>
		/// Static accessor to the Ninject kernel
		/// </summary>
		public static IKernel Kernel
		{
			get { return m_kernel; }
		}

		/// <summary>
		/// Gets an object using Ninject
		/// </summary>
		/// <typeparam name="T">The type of object to get</typeparam>
		/// <returns>The object if successful, throws an exception if failed</returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

		/// <summary>
		/// Gets an object using Ninject
		/// </summary>
		/// <typeparam name="T">The type of object to return</typeparam>
		/// <param name="type">The type of the object to get</param>
		/// <returns>The object if successful, throws an exception if failed</returns>
		public static T Get<T>(Type type)
		{
			return (T)Kernel.Get(type);
		}

		/// <summary>
		/// Gets a named object using Ninject
		/// </summary>
		/// <typeparam name="T">The type of object to get</typeparam>
		/// <param name="name">The name of the object</param>
		/// <returns>The object if successful, throws an exception if failed</returns>
		public static T Get<T>(string name)
		{
			return Kernel.Get<T>(name);
		}

		/// <summary>
		/// Trys to get an object using Ninject
		/// </summary>
		/// <typeparam name="T">The type of object to get</typeparam>
		/// <returns>The object if successful, null if failed</returns>
		public static T TryGet<T>()
        {
			return Kernel.TryGet<T>();
        }

		/// <summary>
		/// Trys to get an object using Ninject
		/// </summary>
		/// <typeparam name="T">The type of object to return</typeparam>
		/// <param name="type">The type of the object to get</param>
		/// <returns>The object if successful, throws an exception if failed</returns>
		public static T TryGet<T>(Type type)
		{
			return (T)Kernel.TryGet(type);
		}

		/// <summary>
		/// Trys to get an object using Ninject
		/// </summary>
		/// <typeparam name="T">The type of object to get</typeparam>
		/// <param name="name">The name of the object</param>
		/// <returns>The object if successful, null if failed</returns>
		public static T TryGet<T>(string name)
		{
			return Kernel.TryGet<T>(name);
		}

		/// <summary>
		/// Load a new Ninject dependency module into the kernel
		/// </summary>
		/// <param name="module">The module to load</param>
        public static void Load(INinjectModule module)
        {
			Kernel.Load(module);
        }
    }
}