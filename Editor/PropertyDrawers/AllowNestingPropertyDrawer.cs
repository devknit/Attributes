
using UnityEngine;
using UnityEditor;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( AllowNestingAttribute))]
	public sealed class AllowNestingPropertyDrawer : PropertyDrawerBase
	{
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty( position, label, property);
			EditorGUI.PropertyField( position, property, label, true);
			EditorGUI.EndProperty();
		}
	}
}
