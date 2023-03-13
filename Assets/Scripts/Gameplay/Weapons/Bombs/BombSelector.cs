using System;

namespace Gameplay.Weapons.Bombs
{
	[Serializable]
	public class BombSelector : WeaponSelector<BombBase>
	{
		protected override void OnGet(BombBase component)
		{
			component.ExplodeEvent += ExplodeEventHandler;
			base.OnGet(component);
		}

		protected override void OnRelease(BombBase component)
		{
			component.ExplodeEvent -= ExplodeEventHandler;
			base.OnRelease(component);
		}

		private void ExplodeEventHandler(BombBase bomb)
		{
			ReleaseInstance(bomb);
		}
	}
}
