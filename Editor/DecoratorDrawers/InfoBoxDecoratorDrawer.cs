
using UnityEditor;
using UnityEngine;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( InfoBoxAttribute))]
	public sealed class InfoBoxDecoratorDrawer : DecoratorDrawer
	{
		public override void OnGUI( Rect position)
		{
			var infoBoxAttribute = attribute as InfoBoxAttribute;
			
			float indentLength = EditorGUIHelper.GetIndentLength( position);
			var infoBoxPosition = new Rect(
				position.x + indentLength,
				position.y,
				position.width - indentLength,
				GetHelpBoxHeight() - 2.0f);
				
			var messageType = MessageType.Info;
			
			switch( infoBoxAttribute.Type)
			{
				case InfoBoxType.kWarning:
				{
					messageType = MessageType.Warning;
					break;
				}
				case InfoBoxType.kError:
				{
					messageType = MessageType.Error;
					break;
				}
			}
			EditorGUIHelper.HelpBox( infoBoxPosition, infoBoxAttribute.Text, messageType);
		}
		public override float GetHeight()
		{
			return GetHelpBoxHeight();
		}
		float GetHelpBoxHeight()
		{
			return EditorGUIUtility.singleLineHeight * 2.25f;
		}
	}
}
