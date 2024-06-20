
using System.Collections;

namespace Football
{
    public class Players:IEnumerable{
        internal List<Player> list;
        public Players(){
            list = new List<Player>();
        }
        public void Add(Player player){
            list.Add(player);
        }

        public IEnumerator GetEnumerator()
        {
            foreach(Player player in list){
                yield return player;
            };
        }
        public IEnumerator PlayersInfo(){
            foreach(Player player in list){
                yield return player.Info;
            }
        }
        public Player this[string name]{
            get{
                foreach (Player player in list){
                    if(player.Name+' '+player.Surname==name){
                        return player;
                    }
                }
                throw new ArgumentException($"there is no player with name {name}");
            }
        }

        public void Remove(Player player){
            list.Remove(player);
        }
    }
}