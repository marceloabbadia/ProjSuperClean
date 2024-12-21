namespace ProjSuperClean.Class
{
    public class Floor
    {
        public string FloorName { get; set; }
        public List<Room> Rooms { get; set; }

        public Floor(string floorName)
        {
            FloorName = floorName;
            Rooms = new List<Room>();
        }

        public Floor() { }

        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }
    }
}
