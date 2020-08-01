
using UnityEngine;
using UnityEditor;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( AssetPreviewAttribute))]
	public sealed class AssetPreviewPropertyDrawer : PropertyDrawerBase
	{
		protected override float GetInternalPropertyHeight( SerializedProperty property, GUIContent label)
		{
			if( property.propertyType == SerializedPropertyType.ObjectReference)
			{
				if( GetAssetPreview( property) != null)
				{
					return GetPropertyHeight( property) + GetAssetPreviewSize( property).y;
				}
				else
				{
					return GetPropertyHeight( property);
				}
			}
			return GetPropertyHeight( property) + GetHelpBoxHeight();
		}
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty( position, label, property);

			if( property.propertyType == SerializedPropertyType.ObjectReference)
			{
				var propertyRect = new Rect()
				{
					x = position.x,
					y = position.y,
					width = position.width,
					height = EditorGUIUtility.singleLineHeight
				};
				EditorGUI.PropertyField( propertyRect, property, label);

				Texture2D previewTexture = GetAssetPreview(property);
				if( previewTexture != null)
				{
					Vector2 previewSize = GetAssetPreviewSize( property);
					previewSize.x = Mathf.Min( previewSize.x, position.width);
					
					var previewRect = new Rect()
					{
						x = position.xMax - previewSize.x,
						y = position.y + EditorGUIUtility.singleLineHeight,
						width = position.width,
						height = previewSize.y
					};
					GUI.Label( previewRect, previewTexture);
				}
			}
			else
			{
				string message = string.Format( "{0} のアセットプレビューはありません", property.name);
				DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
			}
			EditorGUI.EndProperty();
		}
		Texture2D GetAssetPreview( SerializedProperty property)
		{
			if( property.propertyType == SerializedPropertyType.ObjectReference)
			{
				if( property.objectReferenceValue != null)
				{
					if( property.objectReferenceValue is Component component)
					{
						return AssetPreview.GetAssetPreview( component.gameObject);
					}
					else
					{
						return AssetPreview.GetAssetPreview( property.objectReferenceValue);
					}
				}
			}
			return null;
		}
		Vector2 GetAssetPreviewSize( SerializedProperty property)
		{
			Texture2D previewTexture = GetAssetPreview( property);
			if( previewTexture != null)
			{
				AssetPreviewAttribute showAssetPreviewAttribute = property.GetAttribute<AssetPreviewAttribute>();
				int width = Mathf.Clamp( showAssetPreviewAttribute.Width, 0, previewTexture.width);
				int height = Mathf.Clamp( showAssetPreviewAttribute.Height, 0, previewTexture.height);
				return new Vector2(width, height);
			}
			return Vector2.zero;
		}
	}
}
