using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealCity.Util
{
	public static class ArrayExtension
	{
		/// <summary>
		/// 修正数组长度
		/// </summary>
		/// <param name="arr"></param>
		/// <param name="expectedLength"></param>
		/// <returns></returns>
		public static T[] EnsureLength<T>(this T[] arr, int expectedLength) {
			if (arr.Length != expectedLength) {
				T[] correctData = new T[expectedLength];
				if (arr.Length < expectedLength) {
					// expected: 5 3 4 5
					// actually: 5 3 4 _
					Array.Copy(arr, correctData, arr.Length);
					// fill the remaining part
					for (int i = arr.Length; i < expectedLength; ++i) {
						correctData[i] = default;
					}
				} else {
					// throw the useless part
					Array.Copy(arr, correctData, expectedLength);
				}
				return correctData;
			}
			return arr;
		}
	}
}
