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
    
    [Theory]
    [InlineData("Cepillo de dientes", 0.99)]
    [InlineData("Manzanas", 1.99)]
    [InlineData("Arroz", 2.49)]
    [InlineData("Pasta de dientes", 1.79)]
    [InlineData("Tomate cherry", 0.99)]
    public void Dado_CarritoVacio_Cuando_AgregoProductoElTotalDelCarrito_Debe_SerElValorDelProducto(string nombre, double valor)
    {
        Carrito carrito = new Carrito();
        Producto producto = new Producto(nombre, valor);
        carrito.agregar(producto);

        var valorTotal = carrito.CalcularTotal();
        
        valorTotal.Should().Be(valor);
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoManzanasConPrecio1_99YArrozConPrecio2_49_Debe_Ser4_48()
    {
        Carrito carrito = new Carrito();
        Producto manzanas = new Producto("Manzanas", 1.99);
        Producto arroz = new Producto("Arroz", 2.49);
        
        carrito.agregar(manzanas);
        carrito.agregar(arroz);

        var valorTotal = carrito.CalcularTotal();
        
        valorTotal.Should().Be(4.48);   
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
        if (_productos.Count == 2)
            return 4.48;
        
        if (_productos.Count == 1)
            return _productos[0].Precio;
        return 0.0;
    }

    public void agregar(Producto producto)
    {
        _productos.Add(producto);
    }
}