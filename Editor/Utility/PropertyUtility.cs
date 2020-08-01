
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Attributes.Editor
{
	public static class PropertyUtility
	{
		public static T GetAttribute<T>( this SerializedProperty property) where T : class
		{
			T[] attributes = property.GetAttributes<T>();
			return (attributes.Length > 0) ? attributes[ 0] : null;
		}
		public static T[] GetAttributes<T>( this SerializedProperty property) where T : class
		{
			FieldInfo fieldInfo = ReflectionUtility.GetField(
				property.GetTargetObjectWithProperty(), property.name);
			if( fieldInfo != null)
			{
				return fieldInfo.GetCustomAttributes( typeof( T), true) as T[];
			}
			return new T[]{};
		}
		public static string GetLabel( this SerializedProperty property)
		{
			LabelAttribute attribute = property.GetAttribute<LabelAttribute>();
			return (attribute == null)? property.displayName : attribute.Label;
		}
		public static void CallOnValueChangedCallbacks( this SerializedProperty property)
		{
			OnValueChangedAttribute[] onValueChangedAttributes = property.GetAttributes<OnValueChangedAttribute>();
			if( onValueChangedAttributes.Length > 0)
			{
				object target = property.GetTargetObjectWithProperty();
				property.serializedObject.ApplyModifiedProperties();

				foreach( var onValueChangedAttribute in onValueChangedAttributes)
				{
					MethodInfo callbackMethod = ReflectionUtility.GetMethod( target, onValueChangedAttribute.CallbackName);
					if( callbackMethod != null
					&&	callbackMethod.ReturnType == typeof( void)
					&&	callbackMethod.GetParameters().Length == 0)
					{
						callbackMethod.Invoke( target, new object[]{});
					}
					else
					{
						string message = string.Format(
							"{0} can invoke only methods with 'void' return type and 0 parameters",
							onValueChangedAttribute.GetType().Name);

						Debug.LogWarning( message, property.serializedObject.targetObject);
					}
				}
			}
		}
		public static bool IsEnabled( this SerializedProperty property)
		{
			EnableIfAttributeBase enableIfAttribute = property.GetAttribute<EnableIfAttributeBase>();
			if( enableIfAttribute == null)
			{
				return true;
			}

			object target = property.GetTargetObjectWithProperty();

			List<bool> conditionValues = GetConditionValues( target, enableIfAttribute.Conditions);
			if( conditionValues.Count > 0)
			{
				return GetConditionsFlag( conditionValues, enableIfAttribute.ConditionOperator, enableIfAttribute.Inverted);
			}
			
			string message = string.Format(
				"{0} needs a valid boolean condition field, property or method name to work",
				enableIfAttribute.GetType().Name);
			Debug.LogWarning( message, property.serializedObject.targetObject);
			
			return false;
		}
		public static bool IsVisible( this SerializedProperty property)
		{
			ShowIfAttributeBase showIfAttribute = property.GetAttribute<ShowIfAttributeBase>();
			if( showIfAttribute == null)
			{
				return true;
			}

			object target = property.GetTargetObjectWithProperty();

			List<bool> conditionValues = GetConditionValues( target, showIfAttribute.Conditions);
			if( conditionValues.Count > 0)
			{
				return GetConditionsFlag( conditionValues, showIfAttribute.ConditionOperator, showIfAttribute.Inverted);
			}
			
			string message = string.Format(
				"{0} needs a valid boolean condition field, property or method name to work",
				showIfAttribute.GetType().Name);
			Debug.LogWarning( message, property.serializedObject.targetObject);
			
			return false;
		}
		public static object GetTargetObjectOfProperty( this SerializedProperty property)
		{
			if( property != null)
			{
				string[] elements = property.propertyPath.Replace( ".Array.data[", "[").Split('.');
				object targetObject = property.serializedObject.targetObject;
				
				foreach( var element in elements)
				{
					if( element.Contains( "[") == false)
					{
						targetObject = GetValue( targetObject, element);
					}
					else
					{
						int index = Convert.ToInt32( element.Substring( element.IndexOf( "[")).Replace( "[", string.Empty).Replace( "]", string.Empty));
						string elementName = element.Substring( 0, element.IndexOf( "["));
						targetObject = GetValue( targetObject, elementName, index);
					}
				}
				return targetObject;
			}
			return null;
		}
		public static object GetTargetObjectWithProperty( this SerializedProperty property)
		{
			string[] elements = property.propertyPath.Replace( ".Array.data[", "[").Split( '.');
			object targetObject = property.serializedObject.targetObject;

			for( int i0 = 0; i0 < elements.Length - 1; ++i0)
			{
				string element = elements[ i0];
				
				if( element.Contains( "[") == false)
				{
					targetObject = GetValue( targetObject, element);
				}
				else
				{
					int index = Convert.ToInt32( element.Substring( element.IndexOf( "[")).Replace( "[", string.Empty).Replace( "]", string.Empty));
					string elementName = element.Substring( 0, element.IndexOf( "["));
					targetObject = GetValue( targetObject, elementName, index);
				}
			}
			return targetObject;
		}
		public static bool SetValue( this SerializedProperty property, object value)
		{
			switch( property.propertyType)
			{
				case SerializedPropertyType.Generic:
				{
					break;
				}
				case SerializedPropertyType.Integer:
				{
					if( value is int intValue)
					{
						property.intValue = intValue;
						return true;
					}
					if( value is long longValue)
					{
						property.longValue = longValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.Boolean:
				{
					if( value is bool boolValue)
					{
						property.boolValue = boolValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.Float:
				{
					if( value is float floatValue)
					{
						property.floatValue = floatValue;
						return true;
					}
					if( value is double doubleValue)
					{
						property.doubleValue = doubleValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.String:
				{
					if( value is string stringValue)
					{
						property.stringValue = stringValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.Color:
				{
					if( value is Color colorValue)
					{
						property.colorValue = colorValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.ObjectReference:
				{
					break;
				}
				case SerializedPropertyType.LayerMask:
				{
					break;
				}
				case SerializedPropertyType.Enum:
				{
					if( value is Enum enumValue)
					{
						string enumName = enumValue.ToString();
						
						for( int i0 = 0; i0 < property.enumNames.Length; ++i0)
						{
							if( property.enumNames[ i0].Equals( enumName) != false)
							{
								property.enumValueIndex = i0;
								return true;
							}
						}
					}
					break;
				}
				case SerializedPropertyType.Vector2:
				{
					if( value is Vector2 vector2Value)
					{
						property.vector2Value = vector2Value;
						return true;
					}
					break;
				}
				case SerializedPropertyType.Vector3:
				{
					if( value is Vector3 vector3Value)
					{
						property.vector3Value = vector3Value;
						return true;
					}
					break;
				}
				case SerializedPropertyType.Vector4:
				{
					if( value is Vector4 vector4Value)
					{
						property.vector4Value = vector4Value;
						return true;
					}
					break;
				}
				case SerializedPropertyType.Rect:
				{
					if( value is Rect rectValue)
					{
						property.rectValue = rectValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.ArraySize:
				{
					break;
				}
				case SerializedPropertyType.Character:
				{
					if( value is char charValue)
					{
						property.intValue = charValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.AnimationCurve:
				{
					if( value is AnimationCurve animationCurveValue)
					{
						property.animationCurveValue = animationCurveValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.Bounds:
				{
					if( value is Bounds boundsValue)
					{
						property.boundsValue = boundsValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.Gradient:
				{
					break;
				}
				case SerializedPropertyType.Quaternion:
				{
					if( value is Quaternion quaternionValue)
					{
						property.quaternionValue = quaternionValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.ExposedReference:
				{
					break;
				}
				case SerializedPropertyType.FixedBufferSize:
				{
					break;
				}
				case SerializedPropertyType.Vector2Int:
				{
					if( value is Vector2Int vector2IntValue)
					{
						property.vector2IntValue = vector2IntValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.Vector3Int:
				{
					if( value is Vector3Int vector3IntValue)
					{
						property.vector3IntValue = vector3IntValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.RectInt:
				{
					if( value is RectInt rectIntValue)
					{
						property.rectIntValue = rectIntValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.BoundsInt:
				{
					if( value is BoundsInt boundsIntValue)
					{
						property.boundsIntValue = boundsIntValue;
						return true;
					}
					break;
				}
				case SerializedPropertyType.ManagedReference:
				{
					break;
				}
			}
			Debug.LogErrorFormat( $"Unimplemented: ({property.propertyType}<{property.type}>){property.name} = ({value.GetType().Name}){value.ToString()}\n");
			return false;
		}
		internal static List<bool> GetConditionValues( object target, string[] conditions)
		{
			var conditionValues = new List<bool>();
			
			foreach( var condition in conditions)
			{
				FieldInfo conditionField = ReflectionUtility.GetField( target, condition);
				if( conditionField != null
				&&	conditionField.FieldType == typeof( bool))
				{
					conditionValues.Add( (bool)conditionField.GetValue( target));
				}

				PropertyInfo conditionProperty = ReflectionUtility.GetProperty( target, condition);
				if( conditionProperty != null
				&&	conditionProperty.PropertyType == typeof( bool))
				{
					conditionValues.Add( (bool)conditionProperty.GetValue( target));
				}

				MethodInfo conditionMethod = ReflectionUtility.GetMethod( target, condition);
				if( conditionMethod != null
				&&	conditionMethod.ReturnType == typeof( bool)
				&&	conditionMethod.GetParameters().Length == 0)
				{
					conditionValues.Add( (bool)conditionMethod.Invoke( target, null));
				}
			}
			return conditionValues;
		}
		internal static bool GetConditionsFlag( List<bool> conditionValues, ConditionOperator conditionOperator, bool invert)
		{
			bool flag;
			
			if( conditionOperator == ConditionOperator.kAnd)
			{
				flag = true;
				
				foreach( var value in conditionValues)
				{
					flag = flag && value;
				}
			}
			else
			{
				flag = false;
				
				foreach( var value in conditionValues)
				{
					flag = flag || value;
				}
			}
			if( invert != false)
			{
				flag = !flag;
			}
			return flag;
		}
		static object GetValue( object source, string name)
		{
			if( source != null)
			{
				Type type = source.GetType();

				while( type != null)
				{
					FieldInfo field = type.GetField( name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
					if( field != null)
					{
						return field.GetValue( source);
					}

					PropertyInfo property = type.GetProperty( name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
					if( property != null)
					{
						return property.GetValue( source, null);
					}

					type = type.BaseType;
				}
			}
			return null;
		}
		static object GetValue( object source, string name, int index)
		{
			var enumerable = GetValue( source, name) as IEnumerable;
			if( enumerable == null)
			{
				return null;
			}

			IEnumerator enumerator = enumerable.GetEnumerator();
			for( int i0 = 0; i0 <= index; ++i0)
			{
				if( enumerator.MoveNext() == false)
				{
					return null;
				}
			}
			return enumerator.Current;
		}
	}
}