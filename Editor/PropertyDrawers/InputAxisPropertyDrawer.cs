
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( InputAxisAttribute))]
	public sealed class InputAxisPropertyDrawer : PropertyDrawerBase
	{
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			if( property.propertyType == SerializedPropertyType.String)
			{
				var inputManagerAsset = AssetDatabase.LoadAssetAtPath( kAssetPath, typeof( object));
				var inputManager = new SerializedObject( inputManagerAsset);

				var axesProperty = inputManager.FindProperty( kAxesPropertyPath);
				var axesSet = new HashSet<string>{ "(Empty)" };
				int index;

				for( index = 0; index < axesProperty.arraySize; ++index)
				{
					axesSet.Add( axesProperty.GetArrayElementAtIndex( index).FindPropertyRelative( kNamePropertyPath).stringValue);
				}

				string propertyString = property.stringValue;
				string[] axes = axesSet.ToArray();
				
				for( index = axes.Length - 1; index > 0; --index)
				{
					if( axes[ index] == propertyString)
					{
						break;
					}
				}
				EditorGUI.BeginChangeCheck();
				
				index = EditorGUI.Popup( position, label.text, index, axes);
				
				if( EditorGUI.EndChangeCheck() != false)
				{
					property.stringValue = (index > 0)? axes[ index] : string.Empty;
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
		
		static readonly string kAssetPath = Path.Combine( "ProjectSettings", "InputManager.asset");
		const string kAxesPropertyPath = "m_Axes";
		const string kNamePropertyPath = "m_Name";
	}
}
