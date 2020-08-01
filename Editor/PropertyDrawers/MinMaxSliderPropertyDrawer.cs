using UnityEditor;
using UnityEngine;

namespace Attributes.Editor
{
	[CustomPropertyDrawer( typeof( MinMaxSliderAttribute))]
	public sealed class MinMaxSliderPropertyDrawer : PropertyDrawerBase
	{
		protected override void OnInternalGUI( Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty( position, label, property);

			var minMaxSliderAttribute = attribute as MinMaxSliderAttribute;
			
			if( minMaxSliderAttribute.MinValue > minMaxSliderAttribute.MaxValue)
			{
				string message = string.Format( 
					$"{attribute.GetType().Name}: 最小値が最大値を超過しています");
				DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
			}
			else if( minMaxSliderAttribute.MaxValue - minMaxSliderAttribute.MinValue < minMaxSliderAttribute.Padding)
			{
				string message = string.Format( 
					$"{attribute.GetType().Name}: パディングを確保できません");
				DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
			}
			else if( property.propertyType == SerializedPropertyType.Vector2
			||		 property.propertyType == SerializedPropertyType.Vector2Int)
			{
				EditorGUI.BeginProperty( position, label, property);

				float indentLength = EditorGUIHelper.GetIndentLength( position);
				float labelWidth = EditorGUIUtility.labelWidth;
				float valueFieldWidth = EditorGUIUtility.fieldWidth;
				float sliderWidth = position.width - labelWidth - 2.0f * valueFieldWidth;
				float sliderPadding = 5.0f;

				var labelPosition = new Rect(
					position.x,
					position.y,
					labelWidth,
					position.height);
				var sliderPosition = new Rect(
					position.x + labelWidth + valueFieldWidth + sliderPadding - indentLength,
					position.y,
					sliderWidth - 2f * sliderPadding + indentLength,
					position.height);
				var minFieldPosition = new Rect(
					position.x + labelWidth - indentLength,
					position.y,
					valueFieldWidth + indentLength,
					position.height);
				var maxFieldPosition = new Rect(
					position.x + labelWidth + valueFieldWidth + sliderWidth - indentLength,
					position.y,
					valueFieldWidth + indentLength,
					position.height);

				EditorGUI.LabelField( labelPosition, label.text);
				
				if( property.propertyType == SerializedPropertyType.Vector2)
				{
					Vector2Slider( sliderPosition, minFieldPosition, maxFieldPosition, 
						property, minMaxSliderAttribute.MinValue, minMaxSliderAttribute.MaxValue, minMaxSliderAttribute.Padding);
				}
				else
				{
					Vector2IntSlider( sliderPosition, minFieldPosition, maxFieldPosition, 
						property, (int)minMaxSliderAttribute.MinValue, (int)minMaxSliderAttribute.MaxValue, (int)minMaxSliderAttribute.Padding);
				}
				EditorGUI.EndProperty();
			}
			else
			{
				string message = string.Format( 
					"{0}で{1}を使用するにはVector2型、またはVector2Int型でなければなりません", 
					property.name, attribute.GetType().Name);
				DrawDefaultPropertyAndHelpBox( position, property, message, MessageType.Warning);
			}
			EditorGUI.EndProperty();
		}
		static void Vector2Slider( 
			Rect sliderPosition, Rect minFieldPosition, Rect maxFieldPosition, 
			SerializedProperty property, float minValue, float maxValue, float padding)
		{
			float smallValue = property.vector2Value.x;
			float largeValue = property.vector2Value.y;
			float cachedSmallValue = smallValue;
			float cachedLargeValue = largeValue;
			
			EditorGUI.BeginChangeCheck();
			EditorGUI.MinMaxSlider( sliderPosition, ref smallValue, ref largeValue, minValue, maxValue);
			smallValue = EditorGUI.FloatField( minFieldPosition, smallValue);
			smallValue = Mathf.Clamp( smallValue, minValue, Mathf.Min( maxValue, largeValue));
			largeValue = EditorGUI.FloatField( maxFieldPosition, largeValue);
			largeValue = Mathf.Clamp( largeValue, Mathf.Max( minValue, smallValue), maxValue);
			
			while( largeValue - smallValue < padding)
			{
				if( cachedSmallValue != smallValue
				&&	cachedLargeValue == largeValue)
				{
					cachedSmallValue = smallValue = largeValue - padding;
				}
				else if( cachedSmallValue == smallValue
				&&		 cachedLargeValue != largeValue)
				{
					cachedLargeValue = largeValue = smallValue + padding;
				}
				else
				{
					largeValue = smallValue + padding;
					if( largeValue > maxValue)
					{
						float difference = largeValue - maxValue;
						largeValue -= difference;
						smallValue += difference;
					}
					break;
				}
			}
			if( EditorGUI.EndChangeCheck() != false)
			{
				property.vector2Value = new Vector2( smallValue, largeValue);
			}
		}
		static void Vector2IntSlider( 
			Rect sliderPosition, Rect minFieldPosition, Rect maxFieldPosition, 
			SerializedProperty property, int minValue, int maxValue, int padding)
		{
			int smallValue = property.vector2IntValue.x;
			int largeValue = property.vector2IntValue.y;
			int cachedSmallValue = smallValue;
			int cachedLargeValue = largeValue;
			float smallFloat = smallValue;
			float largeFloat = largeValue;
				
			EditorGUI.BeginChangeCheck();
			EditorGUI.MinMaxSlider( sliderPosition, ref smallFloat, ref largeFloat, minValue, maxValue);
			smallValue = (int)smallFloat;
			largeValue = (int)largeFloat;
			
			smallValue = EditorGUI.IntField( minFieldPosition, smallValue);
			smallValue = Mathf.Clamp( smallValue, minValue, Mathf.Min( maxValue, largeValue));
			largeValue = EditorGUI.IntField( maxFieldPosition, largeValue);
			largeValue = Mathf.Clamp( largeValue, Mathf.Max( minValue, smallValue), maxValue);
			
			while( largeValue - smallValue < padding)
			{
				if( cachedSmallValue != smallValue
				&&	cachedLargeValue == largeValue)
				{
					cachedSmallValue = smallValue = largeValue - padding;
				}
				else if( cachedSmallValue == smallValue
				&&		 cachedLargeValue != largeValue)
				{
					cachedLargeValue = largeValue = smallValue + padding;
				}
				else
				{
					largeValue = smallValue + padding;
					if( largeValue > maxValue)
					{
						int difference = largeValue - maxValue;
						largeValue -= difference;
						smallValue += difference;
					}
				}
			}
			if( EditorGUI.EndChangeCheck() != false)
			{
				property.vector2IntValue = new Vector2Int( smallValue, largeValue);
			}
		}
		protected override float GetInternalPropertyHeight( SerializedProperty property, GUIContent label)
		{
			var minMaxSliderAttribute = attribute as MinMaxSliderAttribute;
			
			if( minMaxSliderAttribute.MinValue > minMaxSliderAttribute.MaxValue
			||	minMaxSliderAttribute.MaxValue - minMaxSliderAttribute.MinValue < minMaxSliderAttribute.Padding
			||	(property.propertyType != SerializedPropertyType.Vector2 && property.propertyType != SerializedPropertyType.Vector2Int))
			{
				return GetPropertyHeight( property) + GetHelpBoxHeight();
			}
			return GetPropertyHeight( property);
		}
	}
}
