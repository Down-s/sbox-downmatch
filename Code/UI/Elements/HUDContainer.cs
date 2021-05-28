using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class HUDContainer : Panel
{
	public HUDContainer()
	{
		AddChild<Health>();
		AddChild<Armor>();
	}
}
