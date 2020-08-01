
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
							EditorGUIHelper.HelpBoxLayout( property.name + "�͖����ł�", 
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
								EditorGUIHelper.HelpBoxLayout( property.name + "�͖����ł�", 
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
						const string message = "�t�B�[���h�̌^���R�[���o�b�N�����̌^�ƈ�v���Ă��܂���";
						EditorGUIHelper.HelpBoxLayout( message, MessageType.Warning, context: property.serializedObject.targetObject);
					}
				}
				else
				{
					string message = string.Format(
						"{0}���g�p����ɂ̓R�[���o�b�N�̖߂�l bool �^�Ńt�B�[���h�Ɠ����^�̒P������ł���K�v����܂�",
						validateInputAttribute.GetType().Name);
					EditorGUIHelper.HelpBoxLayout( message, MessageType.Warning, context: property.serializedObject.targetObject);
				}
			}
		}
	}
}
