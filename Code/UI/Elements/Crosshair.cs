using Sandbox;
using Sandbox.UI;

public class CrosshairPanel : Panel
{
	public Panel Crosshair;

	public CrosshairPanel()
	{
		Crosshair = AddChild<Crosshair>();
	}
}

public class Crosshair : Panel
{
	int FireCount = 0;

	public Crosshair()
	{
		
	}

	public override void OnEvent(string EventName)
	{
		if (EventName == "onattack")
		{
			FireCount += 10;
		}

		base.OnEvent(EventName);
	}

	public override void Tick()
	{
		Player pl = Local.Pawn as Player;
		TraceResult tr = Trace.Ray(pl.EyePos, pl.EyePos + (pl.EyeRot.Forward * 4096)).
			Radius(5).
			Ignore(pl).
			Run();

		if (tr.Entity is Player)
		{
			SetClass("player", true);
		}
		else
		{
			SetClass("player", false);
		}

		SetClass("fire", FireCount > 0);

		if (FireCount > 0)
			FireCount--;
	}
}
