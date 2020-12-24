using System;
using System.Threading;
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
            await Task.Run(() =>
            {
                while (true)
                {
                    if (this.CurrentAction.Equals(ActionType.Died)) return;
                    if (Rival.CurrentAction.Equals(ActionType.Died)) return;

                    var randNum = new Random().Next(1, 4);

                    switch (randNum)
                    {
                        case 1: _ = this.PirouetteAttackAsync(); break;
                        case 2: _ = this.VortexAttackAsync(); break;
                        case 3: _ = this.UseSkill(); break;
                        case 4: _ = this.SetBlockedAsync(); break;
                    }

                    Thread.Sleep(new Random().Next(400, 700));
                }
            });
        }
    }
}
