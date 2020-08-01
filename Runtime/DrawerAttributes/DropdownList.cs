
using System;
using System.Collections;
using System.Collections.Generic;

namespace Attributes
{
	public sealed class DropdownList<T> : IDropdownList
	{
		public DropdownList()
		{
			values = new List<KeyValuePair<string, object>>();
		}
		public void Add( string displayName, T value)
		{
			values.Add( new KeyValuePair<string, object>( displayName, value));
		}
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return values.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		public static explicit operator DropdownList<object>( DropdownList<T> elements)
		{
			var result = new DropdownList<object>();
			
			foreach( var element in elements)
			{
				result.Add( element.Key, element.Value);
			}
			return result;
		}
		
		List<KeyValuePair<string, object>> values;
	}
}
