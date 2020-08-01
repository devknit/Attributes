
using UnityEngine;
using UnityEditor;
using System;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( EnumFlagsAttribute))]
	public sealed class EnumFlagsPropertyDrawer : PropertyDrawerBase
	{
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty( position, label, property);

			if( property.GetTargetObjectOfProperty() is Enum targetEnum)
			{
				Enum enumNew = EditorGUI.EnumFlagsField( position, label.text, targetEnum);
				property.intValue = (int)Convert.ChangeType( enumNew, targetEnum.GetType());
			}
			else
			{
				string message = string.Format( 
					"{0}で{1}を使用するにはenum型でなければなりません", 
					property.name, attribute.GetType().Name);
				DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
			}
			EditorGUI.EndProperty();
		}
		protected override float GetInternalPropertyHeight( SerializedProperty property, GUIContent label)
		{
			return ((property.GetTargetObjectOfProperty() as Enum) != null)?
				GetPropertyHeight( property) : GetPropertyHeight( property) + GetHelpBoxHeight();
		}
	}
}
