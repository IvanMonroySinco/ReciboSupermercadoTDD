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

    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoUnCepilloDeDientesConPrecio0_99ElTotalDelCarrito_Debe_Ser0_99()
    {
        Carrito carrito = new Carrito();
        Producto cepilloDeDientes = new Producto("Cepillo de dientes", 0.99);
        carrito.agregar(cepilloDeDientes);

        var valorTotal = carrito.CalcularTotal();
        
        valorTotal.Should().Be(0.99);
    }
    
    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoUnKiloDeManzanasConPrecio1_99ElTotalDelCarrito_Debe_Ser1_99()
    {
        Carrito carrito = new Carrito();
        Producto manzanas = new Producto("Manzanas", 1.99);
        carrito.agregar(manzanas);

        var valorTotal = carrito.CalcularTotal();
        
        valorTotal.Should().Be(1.99);
    }
 
}

public class Producto
{
    public string Nombre  { get; set; }
    public double Precio  { get; set; }
    public Producto(string nombre, double precio)
    {
        Nombre = nombre;
        Precio = precio;
    }
    
    
}

public class Carrito
{
    private List<Producto> _productos = [];
    public double CalcularTotal()
    {
        if (_productos.Count == 1)
            return _productos[0].Precio;
        return 0.0;
    }

    public void agregar(Producto producto)
    {
        _productos.Add(producto);
    }
}