
using UnityEngine;
using UnityEditor;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( LineAttribute))]
	public sealed class LineDecoratorDrawer : DecoratorDrawer
	{
		public override void OnGUI( Rect position)
		{
			position = EditorGUI.IndentedRect( position);
			position.y += EditorGUIUtility.singleLineHeight / 3.0f;
			var lineAttr = attribute as LineAttribute;
			EditorGUIHelper.Line( position, lineAttr.Thickness, lineAttr.Color.GetColor());
		}
		public override float GetHeight()
		{
			var lineAttr = attribute as LineAttribute;
			return EditorGUIUtility.singleLineHeight + lineAttr.Thickness;
		}
	}
}
