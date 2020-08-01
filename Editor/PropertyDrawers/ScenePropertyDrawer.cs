
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( SceneAttribute))]
	public sealed class ScenePropertyDrawer : PropertyDrawerBase
	{
		protected override float GetInternalPropertyHeight( SerializedProperty property, GUIContent label)
		{
			bool validPropertyType = property.propertyType == SerializedPropertyType.String || property.propertyType == SerializedPropertyType.Integer;
			bool anySceneInBuildSettings = GetScenes().Length > 0;

			return (validPropertyType && anySceneInBuildSettings)?
				GetPropertyHeight( property) : GetPropertyHeight( property) + GetHelpBoxHeight();
		}
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			string[] scenes = GetScenes();
			
			if( scenes.Length > 0)
			{
				string[] sceneOptions = GetSceneOptions( scenes);
				
				switch( property.propertyType)
				{
					case SerializedPropertyType.String:
					{
						int index = Mathf.Clamp( Array.IndexOf( scenes, property.stringValue), 0, scenes.Length - 1);
						
						EditorGUI.BeginChangeCheck();
						int newIndex = EditorGUI.Popup( position, label.text, index, sceneOptions);
						if( EditorGUI.EndChangeCheck() != false)
						{
							property.stringValue = scenes[ newIndex];
						}
						break;
					}
					case SerializedPropertyType.Integer:
					{
						int index = property.intValue;
						
						EditorGUI.BeginChangeCheck();
						int newIndex = EditorGUI.Popup( position, label.text, index, sceneOptions);
						if( EditorGUI.EndChangeCheck() != false)
						{
							property.intValue = newIndex;
						}
						break;
					}
					default:
					{
						string message = string.Format( 
							"{0}で{1}を使用するにはstring型、またはint型でなければなりません", 
							property.name, attribute.GetType().Name);
						DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
						break;
					}
				}
			}
			else
			{
				DrawDefaultPropertyAndHelpBox( position, property, "ビルド設定にシーンが存在していません", MessageType.Warning);
			}
		}
		string[] GetScenes()
		{
			return EditorBuildSettings.scenes
				.Where( scene => scene.enabled)
				.Select( scene => Regex.Match( scene.path, @".+\/(.+)\.unity").Groups[ 1].Value)
				.ToArray();
		}
		string[] GetSceneOptions( string[] scenes)
		{
			return scenes.Select( (s, i) => string.Format( "{0} ({1})", s, i)).ToArray();
		}
	}
}
