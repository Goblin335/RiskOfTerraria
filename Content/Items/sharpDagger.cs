using RiskOfTerraria.Content;
using System;
using Terraria;

using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class sharpDagger : ModItem
    {

		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.RiskOfTerraria.hjson' file.

		public override void SetDefaults()
		{
			// ������������ ����������.
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 10;
			Item.sellPrice(0, 0, 0, 20);
			Item.AllowReforgeForStackableItem = true;
			Item.value = Item.buyPrice(0, 1, 10, 8);
			Item.accessory = true;
			Item.rare = ItemRarityID.White;
			Item.crit = 4;
			Item.material = true;
			Item.legSlot = -1;
            
        }
        public override void AddRecipes()
		{
			//Recipe recipe = CreateRecipe();
			//recipe.AddIngredient(ItemID.DirtBlock, 10);
			//recipe.AddTile(TileID.WorkBenches);
			//recipe.Register();

			// ������ �� 2 ������� ������ ��� ������, � 5 ������ ������.
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IronBar, 2);
			recipe.AddIngredient(ItemID.Glass, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			// ������, � ������������ ������

			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LeadBar, 2);
			recipe.AddIngredient(ItemID.Glass, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
			
		}

        // ������ �������� ������ �������� 1.25% ����� �� ������������� ���������� �� � ������ � 5%. ���� ������������� �� ���������� ���������� ����� 5 ��.  
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<Sharp>(), 1);
            PlayerModding.clearPrecentDamage = 1.25 * Item.stack;
        }
    }

}
