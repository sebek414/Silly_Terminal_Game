using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Characters{

        /*
        public interface ICharacter{
            //public Stat Health{get;set;}
            //public Stat Attack_Power{get;set;}

            public int DealDamage();
        }
        */


        public class Player /*: ICharacter*/{

            //public Stat Health{get;set;} = new Stat{Name = "health", Value = 50};
            //public Stat Attack_Power{get;set;} = new Stat{Name = "atak", Value = 4};



            public int PlayerDealDamage(){
                int damageDealt = 10;
                Console.WriteLine($"player deals damage: {damageDealt}");
                return damageDealt;
            }

            public class OnPlayerDamagedEventArgs : EventArgs{
                public int DamageRecieved{get;set;}
            }

            public event EventHandler<OnPlayerDamagedEventArgs> OnPlayerDamaged;

            public void GetDamage(Enemy enemy){
                if (OnPlayerDamaged != null){
                    OnPlayerDamaged(this, new OnPlayerDamagedEventArgs{
                        DamageRecieved = enemy.EnemyDealDamage()
                    });
                }
            }



        }


        public class PlayerStats : IEnumerable<Stat>{

            private Player player;

            public Stat Health {get;set;}= new Stat{Name = "Health"};
            public Stat Attack_Power{get; set;} = new Stat{Name = "Attack Power"};

            public IEnumerator<Stat> GetEnumerator(){
                return new PlayerStatEnumerator(this);
            }

            IEnumerator IEnumerable.GetEnumerator(){
                return GetEnumerator();
            }

            public PlayerStats(Player player, Stat health, Stat attack_power){
                this.player = player;
                this.player.OnPlayerDamaged += Player_OnPlayerDamaged;

                this.Health.Value = health.Value;
                this.Attack_Power.Value = attack_power.Value;
                this.Health.Name = health.Name;
                this.Attack_Power.Name = attack_power.Name;
            }

            public PlayerStats(){

            }

            private void Player_OnPlayerDamaged(
                object sender,
                Player.OnPlayerDamagedEventArgs e
                ){
                    Health.Value = Health.Value - e.DamageRecieved;
                    Console.WriteLine($"Recieved {e.DamageRecieved} damage!\nPlayer health ater damage: {Health.Value}");
                }

            public class PlayerStatEnumerator : IEnumerator<Stat>{

                private PlayerStats playerStats;
                private int index;

                public PlayerStatEnumerator(PlayerStats playerStats){
                    this.playerStats = playerStats;
                    this.index = -1;
                }

                public Stat Current{
                    get{
                        switch(index){
                            default:
                            case 0: return playerStats.Health;
                            case 1: return playerStats.Attack_Power;
                        }
                    }
                }

                object IEnumerator.Current => Current;

                public void Dispose(){

                }

                public bool MoveNext(){
                    index++;
                    if(index > 1){
                        index = -1;
                    }
                    return index != -1;
                }

                public void Reset(){

                }


            }

            ~PlayerStats(){
                this.player.OnPlayerDamaged -= Player_OnPlayerDamaged;
            }
        }


        public class Enemy /*: ICharacter*/{

            public Stat Health{get;set;} = new Stat{Name = "enemy health", Value = 8};
            public Stat Attack_Power{get; set;} = new Stat{Name = "atak", Value = 67};

            public int EnemyDealDamage(){
                int damageDealt = 10;
                Console.WriteLine($"enemy deals damage: {damageDealt}");
                return damageDealt;
            }

        }


        public class Stat{
            public string Name{get; set;}
            public int Value{get; set;}

            public override string ToString(){
                return Name + ": " + Value;
            }
        }


        public enum Player_Class{
            Warrior,
            Mage,
            Wretch,

        }



}

