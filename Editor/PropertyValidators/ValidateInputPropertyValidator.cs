
using UnityEditor;
using System;
using System.Reflection;

namespace Attributes.Editor
{
	public sealed class ValidateInputPropertyValidator : PropertyValidator
	{
		public override void ValidateProperty( SerializedProperty property)
		{
			ValidateInputAttribute validateInputAttribute = property.GetAttribute<ValidateInputAttribute>();
			object target = property.GetTargetObjectWithProperty();

			MethodInfo validationCallback = ReflectionUtility.GetMethod( target, validateInputAttribute.CallbackName);

			if( validationCallback != null
			&&	validationCallback.ReturnType == typeof( bool))
			{
				ParameterInfo[] callbackParameters = validationCallback.GetParameters();

				if( callbackParameters.Length == 0)
				{
					if( (bool)validationCallback.Invoke( target, null) == false)
					{
						if( string.IsNullOrEmpty( validateInputAttribute.Message) != false)
						{
							EditorGUIHelper.HelpBoxLayout( property.name + "は無効です", 
								MessageType.Error, context: property.serializedObject.targetObject);
						}
						else
						{
							EditorGUIHelper.HelpBoxLayout( validateInputAttribute.Message, 
								MessageType.Error, context: property.serializedObject.targetObject);
						}
					}
				}
				else if( callbackParameters.Length == 1)
				{
					FieldInfo fieldInfo = ReflectionUtility.GetField( target, property.name);
					Type parameterType = callbackParameters[ 0].ParameterType;
					Type fieldType = fieldInfo.FieldType;

					if( fieldType == parameterType)
					{
						if( (bool)validationCallback.Invoke( target, new object[]{ fieldInfo.GetValue( target) }) == false)
						{
							if( string.IsNullOrEmpty( validateInputAttribute.Message) != false)
							{
								EditorGUIHelper.HelpBoxLayout( property.name + "は無効です", 
									MessageType.Error, context: property.serializedObject.targetObject);
							}
							else
							{
								EditorGUIHelper.HelpBoxLayout( validateInputAttribute.Message, 
									MessageType.Error, context: property.serializedObject.targetObject);
							}
						}
					}
					else
					{
						const string message = "フィールドの型がコールバック引数の型と一致していません";
						EditorGUIHelper.HelpBoxLayout( message, MessageType.Warning, context: property.serializedObject.targetObject);
					}
				}
				else
				{
					string message = string.Format(
						"{0}を使用するにはコールバックの戻り値 bool 型でフィールドと同じ型の単一引数である必要がります",
						validateInputAttribute.GetType().Name);
					EditorGUIHelper.HelpBoxLayout( message, MessageType.Warning, context: property.serializedObject.targetObject);
				}
			}
		}
	}
}
