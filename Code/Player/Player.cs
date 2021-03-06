using Sandbox;

partial class DownmatchPlayer : Player
{
	[Net]
	public int Kills { get; set; } = 0;

	public float Armor { get; set; } = 100;

	private DamageInfo LastDamage;
	static SoundEvent KillSound = new( "sounds/downmatch/misc/kill.vsnd" )
	{
		Volume = 1,
		DistanceMax = 500.0f
	};

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

	[ClientRpc]
	public void OnKilledPlayer()
	{
		PlaySound(KillSound.Name);
	}

	public override void OnKilled()
	{
		base.OnKilled();

		var Attacker = LastDamage.Attacker as DownmatchPlayer;

		if (Attacker != null && Attacker != this)
		{
			Attacker.OnKilledPlayer();
			Attacker.Kills++;
		}

		BecomeRagdollOnClient( Velocity, LastDamage.Flags, LastDamage.Position, LastDamage.Force, GetHitboxBone( LastDamage.HitboxIndex ) );
		Camera = new SpectateRagdollCamera();
		Controller = null;

		EnableAllCollisions = false;
		EnableDrawing = false;

		Inventory.DeleteContents();
		Inventory.DropActive();
	}

	public override void TakeDamage(DamageInfo dmg)
	{
		LastDamage = dmg;

		base.TakeDamage(dmg);
	}
}
