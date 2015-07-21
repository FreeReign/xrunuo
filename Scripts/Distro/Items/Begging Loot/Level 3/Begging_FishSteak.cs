using System;
using Server;

namespace Server.Items
{
	public class Begging_FishSteak : FishSteak
	{
		[Constructable]
		public Begging_FishSteak()
		{
		}

		public Begging_FishSteak( Serial serial )
			: base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1075129 ); // Acquired by begging
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