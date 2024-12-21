namespace ProjSuperClean.Class
{
    public class Residence
    {
        public string ResidenceName { get; set; }
        public List<Floor> ResidenceFloor { get; set; }

        public Residence(string name)
        {
            ResidenceName = name;
            ResidenceFloor = new List<Floor>();
        }

        public void AddFloor(Floor floor)
        {
            ResidenceFloor.Add(floor);
        }
    }
}
