using UnityEngine;
using UnityEditor;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( CurveRangeAttribute))]
	public sealed class CurveRangePropertyDrawer : PropertyDrawerBase
	{
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty( position, label, property);

			if( property.propertyType == SerializedPropertyType.AnimationCurve)
			{
				var curveRangeAttribute = attribute as CurveRangeAttribute;
				
				EditorGUI.CurveField( position, property, curveRangeAttribute.Color.GetColor(),
					new Rect( 
						curveRangeAttribute.Min.x, 
						curveRangeAttribute.Min.y, 
						curveRangeAttribute.Max.x - curveRangeAttribute.Min.x, 
						curveRangeAttribute.Max.y - curveRangeAttribute.Min.y));
			}
			else
			{
				string message = string.Format( 
					"{0}で{1}を使用するにはAnimationCurve型でなければなりません", 
					property.name, attribute.GetType().Name);
				DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
			}
			EditorGUI.EndProperty();
		}
		protected override float GetInternalPropertyHeight( SerializedProperty property, GUIContent label)
		{
			return (property.propertyType == SerializedPropertyType.AnimationCurve)?
				GetPropertyHeight( property) : GetPropertyHeight( property) + GetHelpBoxHeight();
		}
	}
}
