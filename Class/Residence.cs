namespace ProjSuperClean.Class
{
    public class Residence
    {
        public string ResidenceName { get; set; }

        public List<Floor> ResidenceFloors { get; set; }

        public Residence(string residenceName)
        {
            ResidenceName = residenceName;
            ResidenceFloors = new List<Floor>();
        }

        public Residence() { }

        public void AddFloor(Floor floor)
        {
            ResidenceFloors.Add(floor);
        }

    }
}
