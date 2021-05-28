using Sandbox;

class Inventory : BaseInventory
{
	public Inventory(Player pl) : base (pl)
	{
		
	}

	public override bool Add(Entity ent, bool setactive = false)
	{
		DownmatchPlayer pl = Owner as DownmatchPlayer;
		DMWeapon weapon = ent as DMWeapon;
		
		return base.Add(ent, setactive);
	}
}
