using System;

[AttributeUsage(AttributeTargets.All)]
public class DefaultEvent : Attribute
{
	public string value;

	public DefaultEvent(string value)
	{
		this.value = value;
	}
}