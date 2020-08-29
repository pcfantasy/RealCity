using System;

namespace RealCity.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ArrayLengthAttribute : Attribute
	{
		public int MaximumLength { get; }
		public ArrayLengthAttribute(int maxLength) {
			this.MaximumLength = maxLength;
		}
	}
}
