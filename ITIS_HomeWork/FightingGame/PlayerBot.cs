using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    class PlayerBot : Player
    {
        public PlayerBot(string name, ControlKeys? keyBinds, Point point, int maxHealth = 100, int maxAdrenaline = 3)
            : base(name, new ControlKeys(), point, maxHealth, maxAdrenaline)
        {

        }

        public async Task AsyncBotActions()
        {
            
        }
    }
}
