using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SummonFeyScroll : SpellScroll
	{
		[Constructable]
		public SummonFeyScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public SummonFeyScroll( int amount )
			: base( 606, 0x2D57, amount )
		{
			Hue = 2301;
		}

		public SummonFeyScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			/*int version = */
			reader.ReadInt();
		}
	}
}