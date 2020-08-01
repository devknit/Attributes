
using UnityEngine;
using UnityEditor;

namespace Attributes.Editor
{
	public sealed class MaxValuePropertyValidator : PropertyValidator
	{
		public override void ValidateProperty( SerializedProperty property)
		{
			MaxValueAttribute attribute = property.GetAttribute<MaxValueAttribute>();

			if( property.propertyType == SerializedPropertyType.Integer)
			{
				if( property.intValue > attribute.MaxValue)
				{
					property.intValue = (int)attribute.MaxValue;
				}
			}
			else if( property.propertyType == SerializedPropertyType.Float)
			{
				if( property.floatValue > attribute.MaxValue)
				{
					property.floatValue = attribute.MaxValue;
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
