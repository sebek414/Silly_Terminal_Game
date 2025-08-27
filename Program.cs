using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Characters;

namespace Glowna{

    class Program{

        public const int WARRIOR_DEFAULT_HEALTH = 70;
        public const int MAGE_DEFAULT_HEALTH = 50;
        public const int WRETCH_DEFAULT_HEALTH = 30;


        private static void Main(string[] args){

            GenericCharacter<Stat> player = new GenericCharacter<Stat>("Jaś");
            GenericCharacter<Stat> enemy = new GenericCharacter<Stat>("Małgosia");

            player.OnDamaged += (sender, e) =>
            {
                Console.WriteLine($"Event received: {e.DamageReceived} damage");
            };


            player.Stats.Health.Value = WARRIOR_DEFAULT_HEALTH;
            player.ReceiveDamage(6);


            foreach (var stat in player.Stats)
            {
                Console.WriteLine(stat);
            }



        }
    }
}
