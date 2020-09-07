using System;
using System.Reflection;

/// <summary>
/// Object私有成员扩展
/// </summary>
public static class ObjectPrivateDataExtension
{
	/// <summary>
	/// 访问私有字段
	/// </summary>
	/// <typeparam name="T">字段类型</typeparam>
	/// <param name="instance">实例</param>
	/// <param name="fieldName">字段名</param>
	/// <returns></returns>
	public static T GetPrivateField<T>(this object instance, string fieldName)
	{
		Type type = instance.GetType();
		BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
		FieldInfo field = type.GetField(fieldName, flag);
		return (T)field.GetValue(instance);
	}
	/// <summary>
	/// 访问私有属性
	/// </summary>
	/// <typeparam name="T">属性类型</typeparam>
	/// <param name="instance">实例</param>
	/// <param name="propertyName">属性名</param>
	/// <returns></returns>
	public static T GetPrivateProperty<T>(this object instance, string propertyName)
	{
		Type type = instance.GetType();
		BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
		PropertyInfo field = type.GetProperty(propertyName, flag);
		return (T)field.GetValue(instance, null);
	}
	/// <summary>
	/// 设置私有字段
	/// </summary>
	/// <param name="instance">实例</param>
	/// <param name="fieldName">字段名</param>
	/// <param name="value">新值</param>
	public static void SetPrivateField(this object instance, string fieldName, object value)
	{
		Type type = instance.GetType();
		BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
		FieldInfo field = type.GetField(fieldName, flag);
		field.SetValue(instance, value);
	}
	/// <summary>
	/// 设置私有属性
	/// </summary>
	/// <param name="instance">实例</param>
	/// <param name="propertyName">属性名</param>
	/// <param name="value">新值</param>
	public static void SetPrivateProperty(this object instance, string propertyName, object value)
	{
		Type type = instance.GetType();
		BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
		PropertyInfo field = type.GetProperty(propertyName, flag);
		field.SetValue(instance, value, null);
	}
	/// <summary>
	/// 访问私有方法
	/// </summary>
	/// <typeparam name="T">方法返回值</typeparam>
	/// <param name="instance">实例</param>
	/// <param name="name">方法名</param>
	/// <param name="param">参数</param>
	/// <returns></returns>
	public static T CallPrivateMethod<T>(this object instance, string name, params object[] param)
	{
		Type type = instance.GetType();
		BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
		MethodInfo method = type.GetMethod(name, flag);
		return (T)method.Invoke(instance, param);
	}
}
