namespace ProjSuperClean.Class
{
    public class Room
    {

        public string RoomName { get; set; }
        public int CleanTime { get; set; }
        public int CleanInterval { get; set; }


        public Room(string name, int cleanTime, int cleanInterval)
        {
            if (name.Length > 10)
                throw new ArgumentException("O nome da comodo deve ter no máximo 10 caracteres.");

            RoomName = name;
            CleanTime = cleanTime;
            CleanInterval = cleanInterval;

        }
    }
}

