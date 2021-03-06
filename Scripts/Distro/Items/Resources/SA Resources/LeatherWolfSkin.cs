using System;
using Server;

namespace Server.Items
{
	public class LeatherWolfSkin : Item
	{
		public override int LabelNumber { get { return 1112906; } } // leather wolf skin

		[Constructable]
		public LeatherWolfSkin()
			: this( 1 )
		{
		}

		[Constructable]
		public LeatherWolfSkin( int amount )
			: base( 0xDF8 )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Hue = 0x30;
		}

		public LeatherWolfSkin( Serial serial )
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