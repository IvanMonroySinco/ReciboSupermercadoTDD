using AwesomeAssertions;

namespace ReciboDeSupermercado;

public class ReciboDeSupermercadoTest
{
    [Fact]
    public void Dado_CarritoVacioElTotal_Debe_SerCero()
    {
        Carrito carrito = new Carrito();

        var valorTotal = carrito.CalcularTotal();
        
        valorTotal.Should().Be(0.0);

    }
}

public class Carrito
{
    public double CalcularTotal()
    {
        return 0.0;
    }
}