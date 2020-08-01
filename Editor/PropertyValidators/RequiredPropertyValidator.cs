
using UnityEditor;

namespace Attributes.Editor
{
	public sealed class RequiredPropertyValidator : PropertyValidator
	{
		public override void ValidateProperty( SerializedProperty property)
		{
			RequiredAttribute attribute = property.GetAttribute<RequiredAttribute>();

			if( property.propertyType == SerializedPropertyType.ObjectReference)
			{
				if( property.objectReferenceValue == null)
				{
					string errorMessage = property.name + " is required";
					
					if( string.IsNullOrEmpty( attribute.Message) == false)
					{
						errorMessage = attribute.Message;
					}
					EditorGUIHelper.HelpBoxLayout( errorMessage, MessageType.Error, context: property.serializedObject.targetObject);
				}
			}
			else
			{
				string message = string.Format( 
					"{0}で{1}を使用するには参照型でなければなりません", 
					property.name, attribute.GetType().Name);
				EditorGUIHelper.HelpBoxLayout( message, MessageType.Warning, context: property.serializedObject.targetObject);
			}
		}
	}
}
