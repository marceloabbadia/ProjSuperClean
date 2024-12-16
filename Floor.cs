namespace ProjSuperClean
{
    public class Floor
    {
        public string FloorName { get; set; }
        public List<Room> RoomSpace { get; set; }

        public Floor(string name)
        {
            FloorName = name;
            RoomSpace = new List<Room>();
        }

        public void AddRoom(Room room)
        {
            RoomSpace.Add(room);
        }
    }
}
