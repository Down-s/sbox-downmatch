using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class ScoreUI : Panel
{
	public Panel PlayerScorePanel;
	public Panel WinnerScorePanel;

	public Label PlayerScore;
	public Label WinnerScore;

	public Label InfoLabel;

	public ScoreUI()
	{
		PlayerScorePanel = Add.Panel("player");
		WinnerScorePanel = Add.Panel("winner");

		PlayerScore = PlayerScorePanel.Add.Label("0", "label");
		WinnerScore = WinnerScorePanel.Add.Label("0", "label");
	}

	public override void Tick()
	{
		var pl = Local.Pawn as DownmatchPlayer;

		PlayerScore.Text = pl.Kills.ToString();

		int OpposingKills = 0;
		foreach (var othercl in Client.All)
		{
			if (othercl == Local.Client) continue;
			DownmatchPlayer otherpl = othercl.Pawn as DownmatchPlayer;

			if (otherpl.IsValid() && otherpl.Kills > OpposingKills)
			{
				OpposingKills = otherpl.Kills;
			}
		}

		WinnerScore.Text = OpposingKills.ToString();
	}
}
