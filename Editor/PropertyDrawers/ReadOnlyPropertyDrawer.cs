
using UnityEngine;
using UnityEditor;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( ReadOnlyAttribute))]
	public sealed class ReadOnlyPropertyDrawer : PropertyDrawerBase
	{
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty( position, label, property);
			{
				GUI.enabled = false;
				EditorGUI.PropertyField( position, property, label, true);
				GUI.enabled = true;
			}
			EditorGUI.EndProperty();
		}
		protected override float GetInternalPropertyHeight( SerializedProperty property, GUIContent label)
		{
			return GetPropertyHeight( property);
		}
	}
}
