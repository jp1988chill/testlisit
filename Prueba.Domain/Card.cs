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

        public System.Guid Id { get; private set; } //ID objeto interno .NET
        public string Name { get; private set; }    //nombre tarjetahabiente
        public string Pan { get; private set; } //número de tarjeta(PAN) formato: 1234-1234-1234-1234.
        public string Pin { get; private set; } //clave de 4 dígitos para usuario.
        public string Estado { get; private set; }  //“vigente” o “no vigente”
        public decimal Amount { get; set; } //cantidad
    }
}
