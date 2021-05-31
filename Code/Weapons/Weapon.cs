using Sandbox;

partial class DMWeapon : BaseWeapon
{
	public float Damage = 100.0f;

	static SoundEvent HitSound = new( "sounds/downmatch/hitmarker.vsnd" )
	{
		Volume = 1,
		DistanceMax = 500.0f
	};

	public override void AttackPrimary()
	{
	}

	public void Shoot(TraceResult tr)
	{
		using (Prediction.Off())
		{
			var dmg = DamageInfo.FromBullet(tr.EndPos, tr.Normal, Damage).
				UsingTraceResult(tr).
				WithAttacker(Owner).
				WithWeapon(this);

			tr.Entity.TakeDamage(dmg);

			if (IsServer && tr.Entity is Player)
			{
				PlaySound(HitSound.Name);
			}
		}
	}

	public override void CreateHudElements()
	{
		if ( Local.Hud == null ) return;

		CrosshairPanel = new CrosshairPanel();
		CrosshairPanel.Parent = Local.Hud;
	}
}
