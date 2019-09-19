using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src
{
    public class Player
    {
        public string Name { get; }
        public Guid ID { get; }

        public Player(string name, Guid ID)
        {
            this.Name = name;
            this.ID = ID;
        }

        public override bool Equals(object obj)
        {
            Player client = obj as Player;

            if (client == null)
            {
                return false;
            }

            return client.ID == this.ID;
        }

        public override int GetHashCode()
        {
            return 1213502048 + EqualityComparer<Guid>.Default.GetHashCode(ID);
        }

        public static bool operator ==(Player client1, Player client2)
        {
            return client1?.Equals(client2) ?? ReferenceEquals(client2, null);
        }

        public static bool operator !=(Player client1, Player client2)
        {
            return !(client1 == client2);
        }
    }
}
