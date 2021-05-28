using Sandbox;
using Sandbox.UI;

[Library]
public partial class HUD : HudEntity<RootPanel>
{
	public HUD()
	{
		if (!IsClient) return;

		RootPanel.StyleSheet.Load("/ui/HUD.scss");

		RootPanel.AddChild<HUDContainer>();
		RootPanel.AddChild<CrosshairPanel>();
		RootPanel.AddChild<HUDContainer>();
		RootPanel.AddChild<KillFeed>();
		RootPanel.AddChild<ChatBox>();
	}
}
