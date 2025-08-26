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

        private static void Main(string[] args){

            Stat healthbar = new Stat{Name = "health", Value = 50};
            Stat attackbar = new Stat{Name = "Attack", Value = 18};

            Player player = new Player();
            PlayerStats playerStats = new  PlayerStats(player, healthbar, attackbar);

            player.GetDamage();

            foreach (Stat stat in playerStats){
                Console.WriteLine(stat.ToString());
            }


        }
    }
}
