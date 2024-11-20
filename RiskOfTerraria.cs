using System;
using System.Diagnostics;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace RiskOfTerraria
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class RiskOfTerraria : Mod
	{
        public static RiskOfTerraria ModID;

        public override void Load()
        {
            ModID = this;

            Debug.WriteLine("������� ���� �������� ������ �� ����");
            CustomMusics.CustomMusicLoader(this);
            Debug.WriteLine("������ ���������!");
        }
    }
}
