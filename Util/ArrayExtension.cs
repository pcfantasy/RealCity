using System;
using System.Collections.Generic;

public static class ArrayExtension
{
	/// <summary>
	/// Ensure the length of a <see cref="System.Array"/> is same to a given number.
	/// </summary>
	/// <param name="arr"></param>
	/// <param name="expectedLength"></param>
	/// <returns></returns>
	public static T[] EnsureLength<T>(this T[] arr, int expectedLength) {
		if (arr.Length != expectedLength) {
			T[] correctData = new T[expectedLength];
			if (arr.Length < expectedLength) {
				// expected: arr = { _, _, _, _, _, _, }, len = 6;
				// inputs: arr = { 4, 2, 1, 3, }, len = 4;
				// return: arr = { 4, 2, 1, 3, 0, 0, }, len = 6;
				Array.Copy(arr, correctData, arr.Length);
				// fill the remaining part
				for (int i = arr.Length; i < expectedLength; ++i) {
					correctData[i] = (T)Activator.CreateInstance(typeof(T));
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
	/// Initializes every element of the value-type <see cref="System.Array"/> by a given value.
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
	/// Returns a random element in an <see cref="System.Array"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="arr"></param>
	/// <param name="random"></param>
	/// <returns></returns>
	public static T GetRandomElement<T>(this T[] arr, Random random) {
		return arr[random.Next(arr.Length)];
	}
	/// <summary>
	/// Performs the specified action on each element of the <see cref="IEnumerable{T}"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <param name="action">The <see cref="Action"/> delegate to perform on each element of the <see cref="IEnumerable{T}"/></param>
	public static void ForEach<T>(this IEnumerable<T> value, Action<T> action) {
		foreach (T item in value) {
			action(item);
		}
	}
}
