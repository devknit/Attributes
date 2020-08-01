
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Attributes.Editor
{
	[CanEditMultipleObjects]
	[CustomEditor( typeof( UnityEngine.Object), true)]
	public class ExpansionAttributes : UnityEditor.Editor
	{
		protected virtual void OnEnable()
		{
			nonSerializedFields = ReflectionUtility.GetAllFields(
				target, f => f.GetCustomAttributes( typeof( ShowNonSerializedFieldAttribute), true).Length > 0);

			nativeProperties = ReflectionUtility.GetAllProperties(
				target, p => p.GetCustomAttributes( typeof( ShowNativePropertyAttribute), true).Length > 0);

			methods = ReflectionUtility.GetAllMethods(
				target, m => m.GetCustomAttributes( typeof( ButtonAttribute), true).Length > 0);
		}
		protected virtual void OnDisable()
		{
			ReorderableListPropertyDrawer.Instance.ClearCache();
		}
		public override void OnInspectorGUI()
		{
			GetSerializedProperties( ref serializedProperties);

			DrawButtons();

			if( serializedProperties.Any( property => property.GetAttribute<IAttribute>() != null) == false)
			{
				DrawDefaultInspector();
			}
			else
			{
				DrawSerializedProperties();
			}
			DrawNativeProperties();
			DrawNonSerializedFields();
		}
		protected void GetSerializedProperties( ref List<SerializedProperty> serializedProperties)
		{
			serializedProperties.Clear();
			
			using( var iterator = serializedObject.GetIterator())
			{
				if( iterator.NextVisible( true) != false)
				{
					do
					{
						serializedProperties.Add( serializedObject.FindProperty( iterator.name));
					}
					while( iterator.NextVisible( false));
				}
			}
		}
		protected void DrawSerializedProperties()
		{
			serializedObject.Update();

			foreach( var property in GetNonGroupedProperties( serializedProperties))
			{
				if( property.name.Equals( "m_Script", System.StringComparison.Ordinal) != false)
				{
					GUI.enabled = false;
					EditorGUILayout.PropertyField( property);
					GUI.enabled = true;
				}
				else
				{
					EditorGUIHelper.PropertyFieldLayout( property, true);
				}
			}
			foreach( var group in GetGroupedProperties( serializedProperties))
			{
				IEnumerable<SerializedProperty> visibleProperties = group.Where( property => property.IsVisible());
				if( visibleProperties.Any() != false)
				{
					EditorGUILayout.BeginVertical( GUI.skin.box);
					{
						if( string.IsNullOrEmpty( group.Key) == false)
						{
							EditorGUILayout.LabelField( group.Key, EditorStyles.boldLabel);
						}
						foreach( var property in visibleProperties)
						{
							EditorGUIHelper.PropertyFieldLayout( property, true);
						}
					}
					EditorGUILayout.EndVertical();
				}
			}
			serializedObject.ApplyModifiedProperties();
		}
		protected void DrawButtons()
		{
			if( methods.Any() != false)
			{
				EditorGUILayout.Space();
				EditorGUILayout.LabelField( "Buttons", GetHeaderGUIStyle());
				EditorGUIHelper.Line(
					EditorGUILayout.GetControlRect( false), 
					LineAttribute.kDefaultThickness, 
					LineAttribute.kDefaultColor.GetColor());

				foreach( var method in methods)
				{
					EditorGUIHelper.MethodButton( serializedObject.targetObject, method);
				}
			}
		}
		protected void DrawNativeProperties()
		{
			if( nativeProperties.Any() != false)
			{
				EditorGUILayout.Space();
				EditorGUILayout.LabelField( "Native Properties", GetHeaderGUIStyle());
				EditorGUIHelper.Line( 
					EditorGUILayout.GetControlRect( false), 
					LineAttribute.kDefaultThickness, 
					LineAttribute.kDefaultColor.GetColor());

				foreach( var property in nativeProperties)
				{
					EditorGUIHelper.NativePropertyLayout( serializedObject.targetObject, property);
				}
			}
		}
		protected void DrawNonSerializedFields()
		{
			if( nonSerializedFields.Any() != false)
			{
				EditorGUILayout.Space();
				EditorGUILayout.LabelField( "Non-Serialized Fields", GetHeaderGUIStyle());
				EditorGUIHelper.Line( 
					EditorGUILayout.GetControlRect( false), 
					LineAttribute.kDefaultThickness, 
					LineAttribute.kDefaultColor.GetColor());

				foreach( var field in nonSerializedFields)
				{
					EditorGUIHelper.NonSerializedFieldLayout( serializedObject.targetObject, field);
				}
			}
		}
		static IEnumerable<SerializedProperty> GetNonGroupedProperties( IEnumerable<SerializedProperty> properties)
		{
			return properties.Where( property => property.GetAttribute<BoxGroupAttribute>() == null);
		}
		static IEnumerable<IGrouping<string, SerializedProperty>> GetGroupedProperties( IEnumerable<SerializedProperty> properties)
		{
			return properties
				.Where( property => property.GetAttribute<BoxGroupAttribute>() != null)
				.GroupBy( property => property.GetAttribute<BoxGroupAttribute>().Name);
		}

		static GUIStyle GetHeaderGUIStyle()
		{
			GUIStyle style = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
			style.fontStyle = FontStyle.Bold;
			style.alignment = TextAnchor.UpperCenter;

			return style;
		}
		
		List<SerializedProperty> serializedProperties = new List<SerializedProperty>();
		IEnumerable<FieldInfo> nonSerializedFields;
		IEnumerable<PropertyInfo> nativeProperties;
		IEnumerable<MethodInfo> methods;
	}
}
