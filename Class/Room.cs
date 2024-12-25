using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Security.Cryptography.X509Certificates;

namespace ProjSuperClean.Class
{
    public class Room
    {

        public string RoomName { get; set; }
        public int CleanTime { get; set; }

        public DateTime DayClean { get; set; }
        public int CleanInterval { get; set; }

        public Guid RoomId { get; set; }
        public DateTime NextClean { get; set; }

        public static List<Room> rooms = new List<Room>();

        public Room(string name, int cleanTime, int cleanInterval)
        {


            RoomName = name;
            CleanTime = cleanTime;
            CleanInterval = cleanInterval;
            RoomId = Guid.NewGuid();

            DayClean = DateTime.Today;

            NextClean = DayClean.AddDays(CleanInterval);

        }

        public Room() { }

        public static void CleanDone(Guid roomId, DateTime dateClean)
        {
            var room = rooms.FirstOrDefault(r => r.RoomId == roomId);


            if (room != null)
            {

                room.DayClean = dateClean;

                Console.WriteLine($"Limpeza concluída na sala: {room.RoomName}");
                Console.WriteLine($"Data da última limpeza: {room.DayClean}");
                Console.WriteLine($"Próxima limpeza agendada para: {room.NextClean}");
            }
            else
            {
                Console.WriteLine("Sala não encontrada.");
            }

        }

        public static int[] GenerationPypes(Room room)
        {
            DateTime today = DateTime.Today.AddDays(7);

            DateTime dayClean = room.DayClean;
            DateTime nextClean = dayClean.AddDays(room.CleanInterval);

            int daysSinceClean = (today - dayClean).Days;
            int totalBars = Math.Min(daysSinceClean, 20);

            int greenPipe = 0, yellowPipe = 0, redPipe = 0;

            if (daysSinceClean <= room.CleanInterval - 1)
            {
                greenPipe = totalBars;
            }
            else if (daysSinceClean <= room.CleanInterval + 2)
            {
                greenPipe = room.CleanInterval - 1;
                yellowPipe = Math.Min(daysSinceClean - greenPipe, totalBars - greenPipe);
            }
            else
            {
                greenPipe = room.CleanInterval - 1;
                yellowPipe = 2;
                redPipe = totalBars - (greenPipe + yellowPipe);
            }

            greenPipe = Math.Abs(greenPipe);
            yellowPipe = Math.Abs(yellowPipe);
            redPipe = Math.Abs(redPipe);

            return new int[] { greenPipe, yellowPipe, redPipe };
        }




    }

}


