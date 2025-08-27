using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Characters{

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

        public interface IDamageable<TEventArgs> where TEventArgs : EventArgs{
            event EventHandler<TEventArgs> OnDamaged;
        }

        public class GenericStats <TCharacterType, TStat> : IEnumerable<TStat>
        where TStat : Stat, new()
        where TCharacterType : class, IDamageable<CharacterDamagedEventArgs>
        {

            public TCharacterType TypeCharacter{get;set;}
            public TStat Health {get;set;}= new TStat{Name = "Health"};
            public TStat Attack_Power{get; set;} = new TStat{Name = "Attack Power"};

            public GenericStats(TCharacterType TypeCharacter){
                this.TypeCharacter = TypeCharacter;
                TypeCharacter.OnDamaged += Char_OnCharDamaged;
            }


            public IEnumerator<TStat> GetEnumerator(){
                return new CharacterStatEnumerator(this);
            }

            IEnumerator IEnumerable.GetEnumerator(){
                return GetEnumerator();
            }

            private void Char_OnCharDamaged(
                object sender,
                CharacterDamagedEventArgs e
            ){
                Health.Value = Health.Value - e.DamageReceived;
            }

            public class CharacterStatEnumerator : IEnumerator<TStat>{

                private GenericStats<TCharacterType, TStat> characterStats;
                private int index;

                public CharacterStatEnumerator(GenericStats<TCharacterType, TStat> characterStats){
                    this.characterStats = characterStats;
                    this.index = -1;
                }

                public TStat Current{
                    get{
                        switch(index){
                            default:
                            case 0: return characterStats.Health;
                            case 1: return characterStats.Attack_Power;
                        }
                    }
                }

                object IEnumerator.Current => Current;

                public void Dispose(){

                }

                //liczba statystyk: 2
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

        public class CharacterDamagedEventArgs : EventArgs
        {
            public int DamageReceived { get; set; }
        }


        public class GenericCharacter<TStat> : IDamageable<CharacterDamagedEventArgs>
            where TStat : Stat, new()
        {

            private string characterName;
            public GenericStats<GenericCharacter<TStat>, TStat> Stats {get;set;}
            public event EventHandler<CharacterDamagedEventArgs> OnDamaged;

            public GenericCharacter(string name){
                this.characterName = name;
                Stats = new GenericStats<GenericCharacter<TStat>, TStat> (this);
            }

            public void DealDamage(int amount){
                Console.WriteLine($"{characterName} deals {amount} damage!");
            }

            public void ReceiveDamage(int damage){
                OnDamaged?.Invoke(this, new CharacterDamagedEventArgs {DamageReceived = damage});
                Console.WriteLine($"{characterName} recieved {damage} damage!\n{characterName} health ater damage: {Stats.Health.Value}");

            }

        }


}

