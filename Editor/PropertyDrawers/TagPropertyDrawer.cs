
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( TagAttribute))]
	public sealed class TagPropertyDrawer : PropertyDrawerBase
	{
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			if( property.propertyType == SerializedPropertyType.String)
			{
				var tagList = new List<string>{ "(Empty)", "Untagged" };
				tagList.AddRange( UnityEditorInternal.InternalEditorUtility.tags);
				string[] tags = tagList.ToArray();

				string propertyString = property.stringValue;
				int index;
				
				for( index = tags.Length - 1; index > 0; --index)
				{
					if( tags[ index] == propertyString)
					{
						break;
					}
				}
				EditorGUI.BeginChangeCheck();
				
				index = EditorGUI.Popup( position, label.text, index, tags);
				
				if( EditorGUI.EndChangeCheck() != false)
				{
					property.stringValue = (index > 0)? tags[ index] : string.Empty;
				}
			}
			else
			{
				string message = string.Format( 
					"{0}で{1}を使用するにはstring型でなければなりません", 
					property.name, attribute.GetType().Name);
				DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
			}
		}
		protected override float GetInternalPropertyHeight( SerializedProperty property, GUIContent label)
		{
			return (property.propertyType == SerializedPropertyType.String)?
				GetPropertyHeight( property) : GetPropertyHeight( property) + GetHelpBoxHeight();
		}
	}
}
