using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RiskOfTerraria.Content.Commands
{
    public class DebugCommand : ModCommand
    {
        public override string Command => "debug";

        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
