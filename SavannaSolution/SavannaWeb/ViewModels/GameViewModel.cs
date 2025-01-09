namespace SavannaWeb.ViewModels
{
    public class GameViewModel
    {
        public List<AnimalViewModel> Animals { get; set; }
    }

    public class AnimalViewModel
    {
        public string Type { get; set; }
        public int X { get; set; } // Position X
        public int Y { get; set; } // Position Y
    }
}