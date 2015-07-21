using System;
using Server;

namespace Server.Items
{
	public class EmbroideredOakLeafCloak : BaseOuterTorso
	{
		public override int LabelNumber { get { return 1094901; } } // Embroidered Oak Leaf Cloak [Replica]

		public override int InitMinHits { get { return 150; } }
		public override int InitMaxHits { get { return 150; } }

		public override bool CanFortify { get { return false; } }

		public override int StrengthReq { get { return 0; } }

		[Constructable]
		public EmbroideredOakLeafCloak()
			: base( 0x2684 )
		{
			Hue = 0x557;

			SkillBonuses.Skill_1_Name = SkillName.Stealth;
			SkillBonuses.Skill_1_Value = 5;
		}

		public EmbroideredOakLeafCloak( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			/*int version = */
			reader.ReadInt();
		}
	}
}