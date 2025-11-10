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
    
    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoManzanasConPrecio1_99ArrozConPrecio2_49YLeche1_33_Debe_Ser5_81()
    {
        Carrito carrito = new Carrito();
        Producto manzanas = new Producto("Manzanas", 1.99);
        Producto arroz = new Producto("Arroz", 2.49);
        Producto leche = new Producto("Leche", 1.33);
        
        carrito.agregar(manzanas);
        carrito.agregar(arroz);
        carrito.agregar(leche);

        var valorTotal = carrito.CalcularTotal();
        
        valorTotal.Should().Be(5.81);   
    }
    
    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoMultiplesProductoElTotalDelCarrito_Debe_SerLaSumaDelValorDeCadaProducto()
    {
        Carrito carrito = new Carrito();
        Producto manzanas = new Producto("Manzanas", 1.99);
        Producto arroz = new Producto("Arroz", 2.49);
        Producto leche = new Producto("Leche", 1.33);
        Producto tomateCherry = new Producto("Tomate cherry", 0.69);
        
        carrito.agregar(manzanas);
        carrito.agregar(arroz);
        carrito.agregar(leche);
        carrito.agregar(tomateCherry);

        var valorTotal = carrito.CalcularTotal();
        
        valorTotal.Should().Be(6.5);   
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_Agrego3LecheConPrecio1_33ElTotalDelCarrito_Debe_Ser3_99()
    {
        Carrito carrito = new Carrito();
        Producto leche = new Producto("Leche", 1.33);
        carrito.agregar(leche, 3);
        
        var valorTotal = carrito.CalcularTotal();
        
        valorTotal.Should().Be(3.99);   
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
    private Dictionary<Producto, int> _productos = new();

    public double CalcularTotal()
    {
        if (_productos.Count > 1 )
            return Math.Round(_productos.Sum(p => (double)p.Key.Precio * p.Value),2);
        
        if (_productos.Count == 1)
            return _productos.Keys.First().Precio * _productos.First().Value;
        return 0.0;
    }

    public void agregar(Producto producto)
    {
       agregar(producto, 1);
    }

    public void agregar(Producto producto, int cantidad)
    {
        _productos.Add(producto, cantidad);
    }
}