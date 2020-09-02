using System;

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
	/// <summary>
	/// Initializes every element of the value-type System.Array by a given value.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="arr"></param>
	/// <param name="value"></param>
	public static void Initialize<T>(this Array arr, T value) {
		for (int i = 0; i < arr.Length; i++) {
			arr.SetValue(value, i);
		}
	}
	/// <summary>
	/// Returns a random element in the Array.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="arr"></param>
	/// <param name="random"></param>
	/// <returns></returns>
	public static T GetRandomElement<T>(this T[] arr, Random random) {
		return arr[random.Next(arr.Length)];
	}
}
