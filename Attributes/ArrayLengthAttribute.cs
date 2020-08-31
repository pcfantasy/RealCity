using System;

namespace RealCity.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ArrayLengthAttribute : Attribute
	{
		//TODO: use ArrayExtension.Ensure() to auto-fix bad arrays

		public int MaximumLength { get; }
		public ArrayLengthAttribute(int maxLength) {
			this.MaximumLength = maxLength;
		}
	}
}
