
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Attributes.Editor
{
	public static class ReflectionUtility
	{
		public static FieldInfo GetField( object target, string fieldName)
		{
			return GetAllFields( target, f => f.Name.Equals( fieldName, StringComparison.InvariantCulture)).FirstOrDefault();
		}
		public static IEnumerable<FieldInfo> GetAllFields( object target, Func<FieldInfo, bool> predicate)
		{
			var types = new List<Type>()
			{
				target.GetType()
			};
			while( types.Last().BaseType != null)
			{
				types.Add( types.Last().BaseType);
			}
			for( int i0 = types.Count - 1; i0 >= 0; --i0)
			{
				IEnumerable<FieldInfo> fieldInfos = types[ i0]
					.GetFields( BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
					.Where( predicate);

				foreach( var fieldInfo in fieldInfos)
				{
					yield return fieldInfo;
				}
			}
		}
		public static PropertyInfo GetProperty( object target, string propertyName)
		{
			return GetAllProperties( target, p => p.Name.Equals( propertyName, StringComparison.InvariantCulture)).FirstOrDefault();
		}
		public static IEnumerable<PropertyInfo> GetAllProperties( object target, Func<PropertyInfo, bool> predicate)
		{
			var types = new List<Type>()
			{
				target.GetType()
			};
			while( types.Last().BaseType != null)
			{
				types.Add( types.Last().BaseType);
			}
			for( int i0 = types.Count - 1; i0 >= 0; --i0)
			{
				IEnumerable<PropertyInfo> propertyInfos = types[ i0]
					.GetProperties( BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
					.Where( predicate);

				foreach( var propertyInfo in propertyInfos)
				{
					yield return propertyInfo;
				}
			}
		}
		public static MethodInfo GetMethod( object target, string methodName)
		{
			return GetAllMethods( target, m => m.Name.Equals( methodName, StringComparison.InvariantCulture)).FirstOrDefault();
		}
		public static IEnumerable<MethodInfo> GetAllMethods( object target, Func<MethodInfo, bool> predicate)
		{
			IEnumerable<MethodInfo> methodInfos = target.GetType()
				.GetMethods( BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
				.Where( predicate);
			
			return methodInfos;
		}
	}
}