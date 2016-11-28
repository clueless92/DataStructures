namespace BunnyWars.Core
{
    using System;
    using System.Data.Odbc;
    using System.Runtime.CompilerServices;

    public class Bunny : IComparable
    {
        private int team;
        private const int DEFAULT_HEALTH = 100;
        private const int DEFAULT_SCORE = 0;

        public Bunny(string name, int team, int roomId)
        {
            this.Name = name;
            this.Team = team;
            this.RoomId = roomId;
            this.Health = DEFAULT_HEALTH;
            this.Score = DEFAULT_SCORE;
        }

        public int RoomId { get; set; }

        public string Name { get; private set; }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Team
        {
            get { return this.team; }
            private set
            {
                if (value < 0 || value > 4)
                {
                    throw new IndexOutOfRangeException();
                }

                this.team = value;
            }
        }

        public int CompareTo(object obj)
        {
            Bunny other = obj as Bunny;
            return this.Name.CompareTo(other.Name);
        }

        public override bool Equals(object obj)
        {
            Bunny other = obj as Bunny;
            return this.Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
