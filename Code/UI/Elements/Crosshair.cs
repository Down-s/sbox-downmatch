using Sandbox;
using Sandbox.UI;

public class CrosshairPanel : Panel
{
	public CrosshairPanel()
	{
		AddChild<Crosshair>();
	}
}

public class Crosshair : Panel
{
	public Crosshair()
	{
		
	}

	public override void OnEvent(string EventName)
	{
		Log.Info(EventName);

		if (EventName == "onattack")
		{
			Log.Info("TestEvent");
			AddClass("shoot");
			return;
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
	}
}
