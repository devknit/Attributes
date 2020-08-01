
using UnityEngine;
using UnityEditor;

namespace Attributes.Editor
{
	public sealed class MinValuePropertyValidator : PropertyValidator
	{
		public override void ValidateProperty( SerializedProperty property)
		{
			MinValueAttribute attribute = property.GetAttribute<MinValueAttribute>();

			if( property.propertyType == SerializedPropertyType.Integer)
			{
				if( property.intValue < attribute.MinValue)
				{
					property.intValue = (int)attribute.MinValue;
				}
			}
			else if( property.propertyType == SerializedPropertyType.Float)
			{
				if( property.floatValue < attribute.MinValue)
				{
					property.floatValue = attribute.MinValue;
				}
			}
			else
			{
				string message = string.Format( 
					"{0}で{1}を使用するにはint型、またはfloat型でなければなりません", 
					property.name, attribute.GetType().Name);
				Debug.LogWarning( message, property.serializedObject.targetObject);
			}
		}
	}
}
