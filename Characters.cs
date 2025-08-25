using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Characters{

        public interface Character{
            public Stat Health{get;set;}
            public Stat Attack_Power{get;set;}

            public void DealDamage();
        }

        public class Player : Character{

            public Stat Health{get;set;} = new Stat{Name = "health", Value = 50};
            public Stat Attack_Power{get;set;} = new Stat{Name = "atak", Value = 4};

            public void DealDamage(){
                Console.WriteLine("player amage");
            }

        }

        public class PlayerStats : IEnumerable<Stat>{

            public Stat Health {get;set;}= new Stat{Name = "Health"};
            public Stat Attack_Power{get; set;} = new Stat{Name = "Attack Power"};

            public IEnumerator<Stat> GetEnumerator(){
                return new PlayerStatEnumerator(this);
            }

            IEnumerator IEnumerable.GetEnumerator(){
                return GetEnumerator();
            }

            public PlayerStats(Stat health, Stat attack_power){
                this.Health.Value = health.Value;
                this.Attack_Power.Value = attack_power.Value;
                this.Health.Name = health.Name;
                this.Attack_Power.Name = attack_power.Name;
            }

            public PlayerStats(){

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
        }

        public class Enemy : Character{

            public Stat Health{get;set;} = new Stat{Name = "enemy health", Value = 8};
            public Stat Attack_Power{get; set;} = new Stat{Name = "atak", Value = 67};

            public void DealDamage(){
                Console.WriteLine("enem amage");
            }

        }

        public class Stat{
            public string Name{get; set;}
            public int Value{get; set;}

            public override string ToString(){
                return Name + ": " + Value;
            }
        }

        public enum Character_Class{

        }

}

