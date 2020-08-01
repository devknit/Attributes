
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace Attributes.Editor
{
	public class ReorderableListPropertyDrawer : SpecialCasePropertyDrawerBase
	{
		public static readonly ReorderableListPropertyDrawer Instance = new ReorderableListPropertyDrawer();

		protected override void OnInternalGUI( SerializedProperty property, GUIContent label)
		{
			if( property.isArray != false)
			{
				string key = property.serializedObject.targetObject.GetInstanceID() + "/" + property.name;

				if( reorderableListsByPropertyName.ContainsKey( key) == false)
				{
					var reorderableList = new ReorderableList(property.serializedObject, property, true, true, true, true)
					{
						drawHeaderCallback = (Rect position) =>
						{
							EditorGUI.LabelField( position, string.Format( $"{label.text}: {property.arraySize}"), EditorStyles.boldLabel);
						},
						drawElementCallback = (Rect position, int index, bool isActive, bool isFocused) =>
						{
							SerializedProperty element = property.GetArrayElementAtIndex( index);
							position.y += 1.0f;
							position.xMin += 10.0f;
							EditorGUI.PropertyField( new Rect( position.x, position.y, position.width, 0.0f), element, true);
						},
						elementHeightCallback = (int index) =>
						{
							return EditorGUI.GetPropertyHeight( property.GetArrayElementAtIndex( index)) + 4.0f;
						}
					};
					reorderableListsByPropertyName[ key] = reorderableList;
				}
				reorderableListsByPropertyName[ key].DoLayoutList();
			}
			else
			{
				string message = string.Format( 
					"{0}で{1}を使用するには配列、またはリストでなければなりません", 
					property.name, typeof(ReorderableListAttribute).Name);
				EditorGUIHelper.HelpBoxLayout( message, MessageType.Warning, context: property.serializedObject.targetObject);
				EditorGUILayout.PropertyField( property, true);
			}
		}
		public void ClearCache()
		{
			reorderableListsByPropertyName.Clear();
		}
		readonly Dictionary<string, ReorderableList> reorderableListsByPropertyName = new Dictionary<string, ReorderableList>();
	}
}
