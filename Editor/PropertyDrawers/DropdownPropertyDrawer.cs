
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( DropdownAttribute))]
	public sealed class DropdownPropertyDrawer : PropertyDrawerBase
	{
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty( position, label, property);
			
			if( property.propertyType == SerializedPropertyType.Generic)
			{
				string message = string.Format(
					"SerializedPropertyType が {0} として扱われるフィールドは {1} を使用できません",
					property.propertyType, attribute.GetType().Name);
				DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
			}
			else
			{
				var dropdownAttribute = attribute as DropdownAttribute;
				object target = property.GetTargetObjectWithProperty();

				object valuesObject = GetValues( property, dropdownAttribute.ValuesName);
				FieldInfo dropdownField = ReflectionUtility.GetField( target, property.name);

				if( AreValuesValid( valuesObject, dropdownField) != false)
				{
					if( valuesObject is IList && dropdownField.FieldType == GetElementType( valuesObject))
					{
						object selectedValue = dropdownField.GetValue( target);

						var valuesList = valuesObject as IList;
						var values = new object[ valuesList.Count];
						var displayOptions = new string[ valuesList.Count];

						for( int i0 = 0; i0 < values.Length; ++i0)
						{
							object value = valuesList[ i0];
							values[ i0] = value;
							displayOptions[ i0] = value == null ? "<null>" : value.ToString();
						}
						int selectedValueIndex = Array.IndexOf( values, selectedValue);
						
						if( selectedValueIndex < 0)
						{
							selectedValueIndex = 0;
						}
						Dropdown( position, property, label.text, selectedValueIndex, values, displayOptions);
					}
					else if( valuesObject is IDropdownList)
					{
						object selectedValue = dropdownField.GetValue( target);

						int index = -1;
						int selectedValueIndex = -1;
						var values = new List<object>();
						var displayOptions = new List<string>();
						var dropdown = valuesObject as IDropdownList;

						using( IEnumerator<KeyValuePair<string, object>> dropdownEnumerator = dropdown.GetEnumerator())
						{
							while( dropdownEnumerator.MoveNext() != false)
							{
								++index;

								KeyValuePair<string, object> current = dropdownEnumerator.Current;
								
								if( current.Value?.Equals( selectedValue) == true)
								{
									selectedValueIndex = index;
								}
								values.Add(current.Value);

								if( current.Key == null)
								{
									displayOptions.Add( "<null>");
								}
								else if( string.IsNullOrWhiteSpace( current.Key) != false)
								{
									displayOptions.Add( "<empty>");
								}
								else
								{
									displayOptions.Add( current.Key);
								}
							}
						}
						if( selectedValueIndex < 0)
						{
							selectedValueIndex = 0;
						}
						Dropdown( position, property, label.text, selectedValueIndex, values.ToArray(), displayOptions.ToArray());
					}
				}
				else
				{
					string message = string.Format(
						"無効な定義名 {0} が {1} に設定されています\n定義名が正しくないか、ターゲットフィールドの型と値のフィールド/プロパティ/メソッドが一致していません",
						dropdownAttribute.ValuesName, attribute.GetType().Name);
					DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
				}
			}
			EditorGUI.EndProperty();
		}
		static void Dropdown( Rect position, SerializedProperty property, string label, int selectedValueIndex, object[] values, string[] displayOptions)
		{
			EditorGUI.BeginChangeCheck();
			
			int newIndex = EditorGUI.Popup( position, label, selectedValueIndex, displayOptions);
			
			if( EditorGUI.EndChangeCheck() != false)
			{
				property.SetValue( values[ newIndex]);
			}
		}
		protected override float GetInternalPropertyHeight( SerializedProperty property, GUIContent label)
		{
			if( property.propertyType == SerializedPropertyType.Generic)
			{
				return base.GetPropertyHeight( property) + GetHelpBoxHeight();
			}
			
			var dropdownAttribute = attribute as DropdownAttribute;
			object values = GetValues( property, dropdownAttribute.ValuesName);
			FieldInfo fieldInfo = ReflectionUtility.GetField( property.GetTargetObjectWithProperty(), property.name);

			return AreValuesValid( values, fieldInfo)?
				GetPropertyHeight( property) : GetPropertyHeight( property) + GetHelpBoxHeight();
		}
		object GetValues( SerializedProperty property, string valuesName)
		{
			object target = property.GetTargetObjectWithProperty();

			FieldInfo valuesFieldInfo = ReflectionUtility.GetField( target, valuesName);
			if( valuesFieldInfo != null)
			{
				return valuesFieldInfo.GetValue( target);
			}

			PropertyInfo valuesPropertyInfo = ReflectionUtility.GetProperty( target, valuesName);
			if( valuesPropertyInfo != null)
			{
				return valuesPropertyInfo.GetValue( target);
			}

			MethodInfo methodValuesInfo = ReflectionUtility.GetMethod( target, valuesName);
			if( methodValuesInfo != null
			&&	methodValuesInfo.ReturnType != typeof( void)
			&&	methodValuesInfo.GetParameters().Length == 0)
			{
				return methodValuesInfo.Invoke( target, null);
			}
			return null;
		}
		bool AreValuesValid( object values, FieldInfo dropdownField)
		{
			if( values != null && dropdownField != null)
			{
				if( (values is IList && dropdownField.FieldType == GetElementType( values)) || values is IDropdownList)
				{
					return true;
				}
			}
			return false;
		}
		Type GetElementType( object values)
		{
			Type valuesType = values.GetType();
			
			if( valuesType.IsGenericType != false)
			{
				return valuesType.GetGenericArguments()[ 0];
			}
			return valuesType.GetElementType();
		}
	}
}