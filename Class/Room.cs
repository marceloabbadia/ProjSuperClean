namespace ProjSuperClean.Class
{
    public class Room
    {

        public string RoomName { get; set; }
        public int CleanTime { get; set; }
        public int CleanInterval { get; set; }


        public Room(string name, int cleanTime, int cleanInterval)
        {

            RoomName = name;
            CleanTime = cleanTime;
            CleanInterval = cleanInterval;

        }

        public Room() { }
    }
}

