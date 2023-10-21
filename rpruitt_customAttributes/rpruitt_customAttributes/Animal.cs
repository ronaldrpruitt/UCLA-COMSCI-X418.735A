namespace rpruitt_customAttributes
{
    [AnimalAttribute.Animal("Canis lupus familiaris", 50.5)]
    class Dog
    {
        private string name;
        private double weight;

        public Dog()
        {
            var customAttributes = (AnimalAttribute.AnimalAttribute[])typeof(Dog).GetCustomAttributes(typeof(AnimalAttribute.AnimalAttribute), true);
            if (customAttributes.Length > 0)
            {
                var myAttribute = customAttributes[0];
                name = myAttribute.AnimalName;
                weight = myAttribute.AnimalWeight;
            }
        }
        public string Name { get => name; }
        public double Weight { get => weight; }

        public override string ToString()
        {
            return $"A Dog's Scientific Name is {Name} with an Average Weight of {Weight}";
        }
    }
}
