using System;

[AttributeUsage(AttributeTargets.All)]
public class EventTargetVariable : Attribute
{
	public string variable;

	public EventTargetVariable(string variable)
	{
		this.variable = variable;
	}
}