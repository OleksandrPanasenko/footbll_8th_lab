using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football
{
    internal class StadiumList
    {
        internal List<Stadium> list;
        public StadiumList()
        {
            list = new List<Stadium>();
        }
        public void Add(Stadium Stadium)
        {
            list.Add(Stadium);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Stadium Stadium in list)
            {
                yield return Stadium;
            };
        }
        public IEnumerator StadiumsInfo()
        {
            foreach (Stadium Stadium in list)
            {
                yield return Stadium.Info;
            }
        }
        public Stadium this[string name]
        {
            get
            {
                foreach (Stadium Stadium in list)
                {
                    if (Stadium.name == name)
                    {
                        return Stadium;
                    }
                }
                if (name == Game.noName.name)
                {
                    return Game.noName;
                }
                throw new ArgumentException($"there is no Stadium with name {name}");
            }
        }
        public void Remove(Stadium Stadium)
        {
            if (list.Contains(Stadium))
            {
                list.Remove(Stadium);
            }
        }

    }
}
