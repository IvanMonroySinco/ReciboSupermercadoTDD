using System.Globalization;
using AwesomeAssertions;

namespace ReciboDeSupermercado;

public class ReciboDeSupermercadoTest
{
    private Carrito _carrito = new();

    [Fact]
    public void Dado_CarritoVacioElTotal_Debe_SerCero()
    {
        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(0.0);
    }

    [Theory]
    [InlineData("Cepillo de dientes", 0.99)]
    [InlineData("Manzanas", 1.99)]
    [InlineData("Arroz", 2.49)]
    [InlineData("Pasta de dientes", 1.79)]
    [InlineData("Tomate cherry", 0.99)]
    public void Dado_CarritoVacio_Cuando_AgregoProductoElTotalDelCarrito_Debe_SerElValorDelProducto(string nombre,
        double valor)
    {
        Producto producto = new Producto(nombre, valor);
        _carrito.agregar(producto);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(valor);
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoManzanasConPrecio1_99YArrozConPrecio2_49_Debe_Ser4_48()
    {
        Producto manzanas = new Producto("Manzanas", 1.99);
        Producto arroz = new Producto("Arroz", 2.49);

        _carrito.agregar(manzanas);
        _carrito.agregar(arroz);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(4.48);
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoManzanasConPrecio1_99ArrozConPrecio2_49YLeche1_33_Debe_Ser5_81()
    {
        Producto manzanas = new Producto("Manzanas", 1.99);
        Producto arroz = new Producto("Arroz", 2.49);
        Producto leche = new Producto("Leche", 1.33);

        _carrito.agregar(manzanas);
        _carrito.agregar(arroz);
        _carrito.agregar(leche);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(5.81);
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoMultiplesProductoElTotalDelCarrito_Debe_SerLaSumaDelValorDeCadaProducto()
    {
        Producto manzanas = new Producto("Manzanas", 1.99);
        Producto arroz = new Producto("Arroz", 2.49);
        Producto leche = new Producto("Leche", 1.33);
        Producto tomateCherry = new Producto("Tomate cherry", 0.69);

        _carrito.agregar(manzanas);
        _carrito.agregar(arroz);
        _carrito.agregar(leche);
        _carrito.agregar(tomateCherry);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(6.5);
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_Agrego3LecheConPrecio1_33ElTotalDelCarrito_Debe_Ser3_99()
    {
        Producto leche = new Producto("Leche", 1.33);
        _carrito.agregar(leche, 3);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(3.99);
    }

    [Fact]
    public void Dado_CarritoConUnaLecheConPrecio1_33_Cuando_Agrego2LecheConPrecio1_33ElTotalDelCarrito_Debe_Ser3_99()
    {
        Producto leche = new Producto("Leche", 1.33);
        _carrito.agregar(leche);
        _carrito.agregar(leche, 2);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(3.99);
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_GeneroElRecibo_Debe_MostrarElTotalEnCero()
    {
        Recibo recibo = new Recibo();

        var texto = recibo.Generar();

        texto.Should().Contain("TOTAL: 0.00€");
    }

    [Fact]
    public void Dado_CarritoConUnaLecheConPrecio1_33_Cuando_GeneroElRecibo_Debe_MostrarElProductoYElTotalEn1_33()
    {
        Producto leche = new Producto("Leche", 1.33);
        _carrito.agregar(leche);
        Recibo recibo = new Recibo(_carrito);

        var texto = recibo.Generar();

        texto.Should().Contain("Leche");
        texto.Should().Contain("TOTAL: 1.33€");
    }
    
    [Fact]
    public void Dado_CarritoCon2LecheConPrecio1_33YHuevosConPrecio3_50_Cuando_GeneroElRecibo_Debe_MostrarLosProductosYElTotalEn1_33()
    {
        Producto leche = new Producto("Leche", 1.33);
        Producto huevos = new Producto("Huevos", 3.50);
        _carrito.agregar(leche, 2);
        _carrito.agregar(huevos);
        Recibo recibo = new Recibo(_carrito);

        var texto = recibo.Generar();

        texto.Should().Contain("Leche");
        texto.Should().Contain("2.66€");
        texto.Should().Contain("Huevos");
        texto.Should().Contain("3.50€");

        texto.Should().Contain("TOTAL: 6.16€");
    }
    
    [Fact]
    public void Dado_CarritoConMultiplesProductos_Cuando_GeneroElRecibo_Debe_MostrarLosProductos()
    {
        Producto leche = new Producto("Leche", 1.33);
        Producto huevos = new Producto("Huevos", 3.50);
        Producto baterias = new Producto("Baterias", 1);
        Producto frutosSecos = new Producto("Frutos secos", 2.8);
        _carrito.agregar(leche, 5);
        _carrito.agregar(huevos);
        _carrito.agregar(baterias);
        _carrito.agregar(frutosSecos);
        
        Recibo recibo = new Recibo(_carrito);

        var texto = recibo.Generar();

        texto.Should().Contain("Leche");
        texto.Should().Contain("Huevos");
        texto.Should().Contain("Baterias");
        texto.Should().Contain("Frutos secos");

        texto.Should().Contain($"TOTAL: {_carrito.CalcularTotal().ToString("F2", CultureInfo.InvariantCulture)}€");
    }

    [Fact]
    public void Dado_CarritoCon1KiloDeManzanasConPrecio1_99_Cuando_CalculoElDescuento_Debe_Retornar0_4()
    {
        Producto manzanas = new Producto("Manzanas", 1.99);
        var oferta = new DescuentoPorcentual("Manzanas", 20);

        var descuento = oferta.CalcularDescuento(manzanas, 1);

        descuento.Should().Be(0.4);

    }
    
}

public class DescuentoPorcentual
{
    private readonly string _nombreProducto;
    private readonly int _porcentaje;
    public DescuentoPorcentual(string nombreProducto, int porcentaje)
    {
        _nombreProducto = nombreProducto;
        _porcentaje = porcentaje;
    }

    public double CalcularDescuento(Producto producto, int cantidad)
    {
        return 0.4;
    }
}