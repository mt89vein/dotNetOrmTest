using System;
using System.Reflection;

namespace Infrastructure.DataProvider.Helpers
{
	public static class CustomAttributeProviderExtensions
	{
		public static T GetOneAttribute<T>(this Assembly assembly) where T : Attribute
		{
			var attributes = assembly.GetCustomAttributes(typeof(T)) as T[];

			if (attributes == null || attributes.Length == 0)
			{
				return null;
			}

			return attributes[0];
		}

		public static T GetOneAttribute<T>(this Module module) where T : Attribute
		{
			var attributes = GetAllAttributes<T>(module);

			if (attributes == null || attributes.Length == 0)
			{
				return null;
			}

			return attributes[0];
		}

		public static T[] GetAllAttributes<T>(this Module module) where T : Attribute
		{
			return module.GetCustomAttributes(typeof(T)) as T[];
		}

		public static bool HasAttribute<T>(this Module module) where T : Attribute
		{
			var attributes = GetAllAttributes<T>(module);

			return attributes?.Length > 0;
		}

		public static T GetOneAttribute<T>(this MemberInfo member) where T : Attribute
		{
			return GetOneAttribute<T>(member, false);
		}

		public static T GetOneAttribute<T>(this MemberInfo member, bool inherit) where T : Attribute
		{
			var attributes = GetAllAttributes<T>(member, inherit);

			if (attributes == null || attributes.Length == 0)
			{
				return null;
			}

			return attributes[0];
		}

		public static T[] GetAllAttributes<T>(this MemberInfo member) where T : Attribute
		{
			return member.GetCustomAttributes(typeof(T), false) as T[];
		}

		public static T[] GetAllAttributes<T>(this MemberInfo member, bool inherit) where T : Attribute
		{
			return member.GetCustomAttributes(typeof(T), inherit) as T[];
		}

		public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute
		{
			return member.HasAttribute<T>(false);
		}

		public static bool HasAttribute<T>(this MemberInfo member, bool inherit) where T : Attribute
		{
			return member.IsDefined(typeof(T), inherit);
		}

		public static T GetOneAttribute<T>(this ParameterInfo parameter) where T : Attribute
		{
			return GetOneAttribute<T>(parameter, false);
		}

		public static T GetOneAttribute<T>(this ParameterInfo parameter, bool inherit) where T : Attribute
		{
			var attributes = GetAllAttributes<T>(parameter, inherit);

			if (attributes == null || attributes.Length == 0)
			{
				return null;
			}

			return attributes[0];
		}

		public static T[] GetAllAttributes<T>(this ParameterInfo parameter) where T : Attribute
		{
			return parameter.GetCustomAttributes(typeof(T), false) as T[];
		}

		public static T[] GetAllAttributes<T>(this ParameterInfo parameter, bool inherit) where T : Attribute
		{
			return parameter.GetCustomAttributes(typeof(T), inherit) as T[];
		}

		public static bool HasAttribute<T>(this ParameterInfo parameter) where T : Attribute
		{
			return parameter.HasAttribute<T>(false);
		}

		public static bool HasAttribute<T>(this ParameterInfo parameter, bool inherit) where T : Attribute
		{
			return parameter.IsDefined(typeof(T), inherit);
		}
	}
}