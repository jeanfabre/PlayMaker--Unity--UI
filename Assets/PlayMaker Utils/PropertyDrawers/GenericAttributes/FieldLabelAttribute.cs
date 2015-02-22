using System;


[System.AttributeUsage(AttributeTargets.All)]
public class FieldLabel : Attribute
{
	private string label;
	public string tooltip;
	
	public FieldLabel(string label)
	{
		this.label = label;
		this.tooltip = "";
	}

	public FieldLabel(string label,string toolTip)
	{
		this.label = label;
		this.tooltip = toolTip;
	}
}