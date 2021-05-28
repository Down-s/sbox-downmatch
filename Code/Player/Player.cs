using Sandbox;

partial class DownmatchPlayer : Player
{
	public float Armor { get; set; } = 100;

	private DamageInfo LastDamage;

	public DownmatchPlayer()
	{
		Inventory = new BaseInventory(this);
	}

	public override void Respawn()
	{
		SetModel("models/citizen/citizen.vmdl");
		
		Controller = new PlayerController();
		Animator = new StandardPlayerAnimator();
		Camera = new FirstPersonCamera();

		EnableAllCollisions = true;
		EnableDrawing = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;

		Inventory.Add(new LaserGun(), true);

		Health = 100;
		Armor = 100;

		base.Respawn();
	}

	public override void Simulate( Client cl )
	{
		base.Simulate( cl );

		if ( Input.ActiveChild != null )
		{
			ActiveChild = Input.ActiveChild;
		}

		SimulateActiveChild( cl, ActiveChild );
	}

	public override void OnKilled()
	{
		base.OnKilled();

		BecomeRagdollOnClient( Velocity, LastDamage.Flags, LastDamage.Position, LastDamage.Force, GetHitboxBone( LastDamage.HitboxIndex ) );
		Camera = new SpectateRagdollCamera();
		Controller = null;

		EnableAllCollisions = false;
		EnableDrawing = false;

		Inventory.DropActive();
		Inventory.DeleteContents();
	}

	public override void TakeDamage(DamageInfo dmg)
	{
		LastDamage = dmg;

		base.TakeDamage(dmg);
	}
}
