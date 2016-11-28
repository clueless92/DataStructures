namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;
    
    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private readonly Dictionary<int, HashSet<Bunny>[]> roomsByIDThenByTeam;
        private readonly OrderedSet<int> orderOfRooms;
        private readonly OrderedDictionary<string, Bunny> bunnysByName;
        private readonly Dictionary<int, OrderedSet<Bunny>> bunnysByTeam;

        public BunnyWarsStructure()
        {
            this.roomsByIDThenByTeam = new Dictionary<int, HashSet<Bunny>[]>();
            this.orderOfRooms = new OrderedSet<int>();
            this.bunnysByName = new OrderedDictionary<string, Bunny>(this.SuffixCompare);
            this.bunnysByTeam = new Dictionary<int, OrderedSet<Bunny>>();
        }

        public int BunnyCount { get { return this.bunnysByName.Count; } }

        public int RoomCount { get { return this.orderOfRooms.Count; } }

        public void AddRoom(int roomId)
        {
            if (this.roomsByIDThenByTeam.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            this.roomsByIDThenByTeam.Add(roomId, new HashSet<Bunny>[5]);
            this.orderOfRooms.Add(roomId);
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (!this.roomsByIDThenByTeam.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            Bunny newBunny = new Bunny(name, team, roomId);

            if (this.bunnysByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            if (this.roomsByIDThenByTeam[roomId][team] == null)
            {
                this.roomsByIDThenByTeam[roomId][team] = new HashSet<Bunny>();
            }

            this.roomsByIDThenByTeam[roomId][team].Add(newBunny);
            this.bunnysByName.Add(name, newBunny);

            if (!this.bunnysByTeam.ContainsKey(newBunny.Team))
            {
                this.bunnysByTeam.Add(newBunny.Team, new OrderedSet<Bunny>((b1, b2) => b2.Name.CompareTo(b1.Name)));
            }

            this.bunnysByTeam[newBunny.Team].Add(newBunny);
        }

        public void Remove(int roomId)
        {
            if (!this.roomsByIDThenByTeam.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            foreach (HashSet<Bunny> team in this.roomsByIDThenByTeam[roomId])
            {
                if (team == null)
                {
                    continue;
                }

                foreach (Bunny bunny in team)
                {
                    this.bunnysByName.Remove(bunny.Name);
                    this.bunnysByTeam[bunny.Team].Remove(bunny);
                }
            }

            this.roomsByIDThenByTeam.Remove(roomId);
            this.orderOfRooms.Remove(roomId);
        }

        public void Next(string bunnyName)
        {
            if (!this.bunnysByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            if (this.roomsByIDThenByTeam.Count == 1)
            {
                return;
            }

            Bunny bunnyToJump = this.bunnysByName[bunnyName];
            this.RemoveBunny(bunnyToJump, bunnyToJump.Team);

            int currRoomIndex = this.orderOfRooms.IndexOf(bunnyToJump.RoomId);
            int nextRoomIndex = currRoomIndex + 1;
            if (nextRoomIndex >= this.orderOfRooms.Count)
            {
                nextRoomIndex = 0;
            }

            bunnyToJump.RoomId = this.orderOfRooms[nextRoomIndex];
            this.AddBunny(bunnyName, bunnyToJump.Team, bunnyToJump.RoomId);
        }

        public void Previous(string bunnyName)
        {
            if (!this.bunnysByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            if (this.roomsByIDThenByTeam.Count == 1)
            {
                return;
            }

            Bunny bunnyToJump = this.bunnysByName[bunnyName];
            this.RemoveBunny(bunnyToJump, bunnyToJump.Team);

            int currRoomIndex = this.orderOfRooms.IndexOf(bunnyToJump.RoomId);
            int prevRoomIndex = currRoomIndex - 1;
            if (prevRoomIndex < 0)
            {
                prevRoomIndex = this.orderOfRooms.Count - 1;
            }

            bunnyToJump.RoomId = this.orderOfRooms[prevRoomIndex];
            this.AddBunny(bunnyName, bunnyToJump.Team, bunnyToJump.RoomId);
        }

        public void Detonate(string bunnyName)
        {
            if (!this.bunnysByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }

            if (this.bunnysByTeam.Count < 2)
            {
                return;
            }

            Bunny detonatingBunny = this.bunnysByName[bunnyName];
            var room = this.roomsByIDThenByTeam[detonatingBunny.RoomId];
            List<Bunny> bunnysToRemove = new List<Bunny>();
            for (int i = 0; i < 5; i++)
            {
                if (detonatingBunny.Team == i || room[i] == null)
                {
                    continue;
                }

                foreach (Bunny bunny in room[i])
                {
                    bunny.Health -= 30;
                    if (bunny.Health <= 0)
                    {
                        bunnysToRemove.Add(bunny);
                        detonatingBunny.Score++;
                    }
                }  
            }

            foreach (var bunny in bunnysToRemove)
            {
                this.RemoveBunny(bunny, bunny.Team);
            }
        }

        private void RemoveBunny(Bunny bunny, int team)
        {
            this.roomsByIDThenByTeam[bunny.RoomId][team].Remove(bunny);
            this.bunnysByName.Remove(bunny.Name);
            this.bunnysByTeam[bunny.Team].Remove(bunny);
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if (team > 4 || team < 0)
            {
                throw new IndexOutOfRangeException();
            }

            return this.bunnysByTeam[team];
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            string upperBount = char.MaxValue + suffix;
            var result = this.bunnysByName.Range(suffix, true, upperBount, false);
            return result.Values;
        }

        private int SuffixCompare(string b1, string b2)
        {
            int b1I = b1.Length - 1;
            int b2I = b2.Length - 1;
            int min = Math.Min(b1I, b2I);
            for (; min >= 0; b1I--, b2I--, min--)
            {
                char ch1 = b1[b1I];
                char ch2 = b2[b2I];
                if (ch1 != ch2)
                {
                    return ch1.CompareTo(ch2);
                }
            }

            return b1.Length.CompareTo(b2.Length);
        }
    }
}
