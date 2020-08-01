
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Attributes.Editor
{
	public abstract class PropertyValidator
	{
		public abstract void ValidateProperty( SerializedProperty property);
	}
	public static class ValidatorAttributeExtensions
	{
		static ValidatorAttributeExtensions()
		{
			validatorsByAttributeType = new Dictionary<Type, PropertyValidator>();
			validatorsByAttributeType[ typeof( MinValueAttribute)] = new MinValuePropertyValidator();
			validatorsByAttributeType[ typeof( MaxValueAttribute)] = new MaxValuePropertyValidator();
			validatorsByAttributeType[ typeof( RequiredAttribute)] = new RequiredPropertyValidator();
			validatorsByAttributeType[ typeof( ValidateInputAttribute)] = new ValidateInputPropertyValidator();
		}
		public static PropertyValidator GetValidator( this ValidatorAttribute attr)
		{
			PropertyValidator validator;
			
			if( validatorsByAttributeType.TryGetValue( attr.GetType(), out validator) != false)
			{
				return validator;
			}
			return null;
		}
		static Dictionary<Type, PropertyValidator> validatorsByAttributeType;
	}
}
