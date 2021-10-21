namespace Prueba.Domain
{
    public class Card
    {
        public Card(string name, string pan)
        {
            Id = System.Guid.NewGuid();
            Name = name;
            Pan = pan;
        }

        public System.Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Pan { get; private set; }
        public decimal Amount { get; set; }
    }
}
