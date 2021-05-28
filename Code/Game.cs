using Sandbox;

[Library("downmatch", Title="Downmatch")]
partial class Downmatch : Game
{
	public Downmatch()
	{
		if (IsServer)
		{
			new HUD();
		}
	}
}
