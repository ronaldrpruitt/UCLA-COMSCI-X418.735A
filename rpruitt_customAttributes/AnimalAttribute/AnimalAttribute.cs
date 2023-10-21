namespace AnimalAttribute
{
    public class AnimalAttribute : System.Attribute
    {
        private string animalName;
        private double animalWeight;
        public AnimalAttribute(string animalName, double animalWeight)
        {
            this.animalName = animalName;
            this.animalWeight = animalWeight;
        }

        public string AnimalName { get => animalName; set => animalName = value; }
        public double AnimalWeight { get => animalWeight; set => animalWeight = value; }
    }
}
