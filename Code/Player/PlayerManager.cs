using Sandbox;
using System;
using System.Linq;

partial class Downmatch : Game
{
	public override void ClientJoined(Client cl)
	{
		base.ClientJoined(cl);

		var pl = new DownmatchPlayer();
		pl.Respawn();

		cl.Pawn = pl;
	}
}
