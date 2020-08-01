
using UnityEngine;
using UnityEditor;

namespace Attributes.Editor
{
	public abstract class PropertyDrawerBase : PropertyDrawer
	{
		protected abstract void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label);
		
		public sealed override float GetPropertyHeight( SerializedProperty property, GUIContent label)
		{
			if( property.IsVisible() != false)
			{
				return GetInternalPropertyHeight( property, label);
			}
			return 0.0f;
		}
		protected virtual float GetInternalPropertyHeight( SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight( property, label);
		}
		protected virtual float GetPropertyHeight( SerializedProperty property)
		{
			return EditorGUI.GetPropertyHeight( property, true);
		}
		public virtual float GetHelpBoxHeight()
		{
			return EditorGUIUtility.singleLineHeight * 2.25f;
		}
		public sealed override void OnGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			if( property.IsVisible() != false)
			{
				ValidatorAttribute[] validatorAttributes = property.GetAttributes<ValidatorAttribute>();
				
				foreach( ValidatorAttribute validatorAttribute in validatorAttributes)
				{
					validatorAttribute.GetValidator().ValidateProperty( property);
				}
				
				EditorGUI.BeginChangeCheck();
				GUI.enabled = property.IsEnabled();
				OnInternalGUI( position, property, new GUIContent( property.GetLabel()));
				GUI.enabled = true;
				
				if( EditorGUI.EndChangeCheck() != false)
				{
					property.CallOnValueChangedCallbacks();
				}
			}
		}
		public void DrawDefaultPropertyAndHelpBox( Rect position, SerializedProperty property, string message, MessageType messageType)
		{
			float indentLength = EditorGUIHelper.GetIndentLength( position);
			var helpBoxPosition = new Rect(
				position.x + indentLength,
				position.y,
				position.width - indentLength,
				GetHelpBoxHeight() - 2.0f);
			var propertyPosition = new Rect(
				position.x,
				position.y + GetHelpBoxHeight(),
				position.width,
				GetPropertyHeight( property));

			EditorGUIHelper.HelpBox( helpBoxPosition, message, MessageType.Warning, context: property.serializedObject.targetObject);
			EditorGUI.PropertyField( propertyPosition, property, true);
		}
		
	}
}