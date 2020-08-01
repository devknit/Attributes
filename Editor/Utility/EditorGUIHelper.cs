
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
using System;
using System.Linq;
using System.Reflection;
using System.Collections;

namespace Attributes.Editor
{
	public static class EditorGUIHelper
	{
		public static void PropertyFieldLayout( SerializedProperty property, bool includeChildren)
		{
			SpecialCaseDrawerAttribute specialCaseAttribute = property.GetAttribute<SpecialCaseDrawerAttribute>();
			if( specialCaseAttribute != null)
			{
				specialCaseAttribute.GetDrawer().OnGUI( property);
			}
			else
			{
				var label = new GUIContent( PropertyUtility.GetLabel( property));

				if( property.GetAttributes<DrawerAttribute>().Any() == false)
				{
					if( property.IsVisible() != false)
					{
						ValidatorAttribute[] validatorAttributes = PropertyUtility.GetAttributes<ValidatorAttribute>(property);
						foreach( var validatorAttribute in validatorAttributes)
						{
							validatorAttribute.GetValidator().ValidateProperty( property);
						}

						EditorGUI.BeginChangeCheck();
						bool enabled = PropertyUtility.IsEnabled( property);
						GUI.enabled = enabled;
						EditorGUILayout.PropertyField( property, label, includeChildren);
						GUI.enabled = true;

						if( EditorGUI.EndChangeCheck() != false)
						{
							PropertyUtility.CallOnValueChangedCallbacks( property);
						}
					}
				}
				else
				{
					EditorGUILayout.PropertyField( property, label, includeChildren);
				}
			}
		}
		public static void MethodButton(UnityEngine.Object target, MethodInfo methodInfo)
		{
			if( ButtonUtility.IsVisible( target, methodInfo) != false)
			{
				if( methodInfo.GetParameters().All( p => p.IsOptional) != false)
				{
					var buttonAttribute = methodInfo.GetCustomAttributes( typeof( ButtonAttribute), true)[0] as ButtonAttribute;
					string buttonText = string.IsNullOrEmpty( buttonAttribute.Label)?
						ObjectNames.NicifyVariableName( methodInfo.Name) : buttonAttribute.Label;

					ButtonEnableMode mode = buttonAttribute.SelectedEnableMode;
					bool buttonEnabled = ButtonUtility.IsEnabled( target, methodInfo);
					buttonEnabled &=
						mode == ButtonEnableMode.kAlways ||
						mode == ButtonEnableMode.kEditor && Application.isPlaying == false ||
						mode == ButtonEnableMode.kPlaymode && Application.isPlaying != false;

					if( methodInfo.ReturnType == typeof( IEnumerator))
					{
						buttonEnabled &= (Application.isPlaying? true : false);
					}
					EditorGUI.BeginDisabledGroup( buttonEnabled == false);

					if( GUILayout.Button(buttonText) != false)
					{
						object[] defaultParams = methodInfo.GetParameters().Select( p => p.DefaultValue).ToArray();
						IEnumerator methodResult = methodInfo.Invoke( target, defaultParams) as IEnumerator;

						if( Application.isPlaying == false)
						{
							EditorUtility.SetDirty( target);

							PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();
							if( stage != null)
							{
								EditorSceneManager.MarkSceneDirty( stage.scene);
							}
							else
							{
								EditorSceneManager.MarkSceneDirty( EditorSceneManager.GetActiveScene());
							}
						}
						else if( methodResult != null && target is MonoBehaviour behaviour)
						{
							behaviour.StartCoroutine( methodResult);
						}
					}
					EditorGUI.EndDisabledGroup();
				}
				else
				{
					string message = string.Format(
						"{0}は引数の無いメソッドを指定した場合のみ機能します",
						typeof( ButtonAttribute).Name);
					HelpBoxLayout( message, MessageType.Warning, context: target, logToConsole: true);
				}
			}
		}
		public static void NativePropertyLayout( UnityEngine.Object target, PropertyInfo property)
		{
			object value = property.GetValue( target, null);
			if( value == null)
			{
				string message = string.Format(
					"{0}はnullです\n{1}は参照型がnullの場合、機能しません", 
					property.Name, typeof(ShowNativePropertyAttribute).Name);
				HelpBoxLayout( message, MessageType.Warning, context: target);
			}
			else if( FieldLayout(value, property.Name) == false)
			{
				string message = string.Format(
					"{0}は{1}型をサポートしていません", 
					typeof(ShowNativePropertyAttribute).Name, property.PropertyType.Name);
				HelpBoxLayout( message, MessageType.Warning, context: target);
			}
		}
		public static void NonSerializedFieldLayout( UnityEngine.Object target, FieldInfo field)
		{
			object value = field.GetValue( target);
			if( value == null)
			{
				string message = string.Format( 
					"{0}はnullです\n{1}は参照型がnullの場合、機能しません", 
					field.Name, typeof(ShowNonSerializedFieldAttribute).Name);
				HelpBoxLayout( message, MessageType.Warning, context: target);
			}
			else if( FieldLayout( value, field.Name) == false)
			{
				string message = string.Format( 
					"{0}は{1}型をサポートしていません", 
					typeof(ShowNonSerializedFieldAttribute).Name, field.FieldType.Name);
				HelpBoxLayout( message, MessageType.Warning, context: target);
			}
		}
		public static float GetIndentLength( Rect sourceRect)
		{
			return EditorGUI.IndentedRect( sourceRect).x - sourceRect.x;
		}
		public static void Line( Rect position, float thickness, Color color)
		{
			position.height = thickness;
			EditorGUI.DrawRect( position, color);
		}
		public static void HelpBox( Rect position, string message, MessageType type, UnityEngine.Object context=null, bool logToConsole=false)
		{
			EditorGUI.HelpBox( position, message, type);

			if( logToConsole != false)
			{
				DebugLogMessage( message, type, context);
			}
		}
		public static void HelpBoxLayout( string message, MessageType type, UnityEngine.Object context=null, bool logToConsole=false)
		{
			EditorGUILayout.HelpBox( message, type);

			if( logToConsole != false)
			{
				DebugLogMessage( message, type, context);
			}
		}
		static void DebugLogMessage( string message, MessageType type, UnityEngine.Object context)
		{
			switch( type)
			{
				case MessageType.None:
				case MessageType.Info:
				{
					Debug.Log( message, context);
					break;
				}
				case MessageType.Warning:
				{
					Debug.LogWarning( message, context);
					break;
				}
				case MessageType.Error:
				{
					Debug.LogError( message, context);
					break;
				}
			}
		}
		public static bool FieldLayout( object value, string label)
		{
			Type valueType = value.GetType();
			bool ret = true;
			
			GUI.enabled = false;
			
			if( valueType == typeof( bool))
			{
				EditorGUILayout.Toggle( label, (bool)value);
			}
			else if( valueType == typeof( int))
			{
				EditorGUILayout.IntField( label, (int)value);
			}
			else if( valueType == typeof( long))
			{
				EditorGUILayout.LongField( label, (long)value);
			}
			else if( valueType == typeof( float))
			{
				EditorGUILayout.FloatField( label, (float)value);
			}
			else if( valueType == typeof( double))
			{
				EditorGUILayout.DoubleField( label, (double)value);
			}
			else if( valueType == typeof( string))
			{
				EditorGUILayout.TextField( label, (string)value);
			}
			else if( valueType == typeof( Vector2))
			{
				EditorGUILayout.Vector2Field( label, (Vector2)value);
			}
			else if( valueType == typeof( Vector3))
			{
				EditorGUILayout.Vector3Field( label, (Vector3)value);
			}
			else if( valueType == typeof( Vector4))
			{
				EditorGUILayout.Vector4Field( label, (Vector4)value);
			}
			else if( valueType == typeof( Color))
			{
				EditorGUILayout.ColorField( label, (Color)value);
			}
			else if( valueType == typeof( Bounds))
			{
				EditorGUILayout.BoundsField( label, (Bounds)value);
			}
			else if( valueType == typeof( Rect))
			{
				EditorGUILayout.RectField( label, (Rect)value);
			}
			else if( typeof( UnityEngine.Object).IsAssignableFrom(valueType))
			{
				EditorGUILayout.ObjectField( label, (UnityEngine.Object)value, valueType, true);
			}
			else if( valueType.BaseType == typeof( Enum))
			{
				EditorGUILayout.EnumPopup( label, (Enum)value);
			}
			else
			{
				ret = false;
			}
			GUI.enabled = true;

			return ret;
		}
	}
}
