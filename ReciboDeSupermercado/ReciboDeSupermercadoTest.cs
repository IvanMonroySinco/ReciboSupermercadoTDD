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
        _carrito.Agregar(producto);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(valor);
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoManzanasConPrecio1_99YArrozConPrecio2_49_Debe_Ser4_48()
    {
        Producto manzanas = new Producto("Manzanas", 1.99);
        Producto arroz = new Producto("Arroz", 2.49);

        _carrito.Agregar(manzanas);
        _carrito.Agregar(arroz);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(4.48);
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_AgregoManzanasConPrecio1_99ArrozConPrecio2_49YLeche1_33_Debe_Ser5_81()
    {
        Producto manzanas = new Producto("Manzanas", 1.99);
        Producto arroz = new Producto("Arroz", 2.49);
        Producto leche = new Producto("Leche", 1.33);

        _carrito.Agregar(manzanas);
        _carrito.Agregar(arroz);
        _carrito.Agregar(leche);

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

        _carrito.Agregar(manzanas);
        _carrito.Agregar(arroz);
        _carrito.Agregar(leche);
        _carrito.Agregar(tomateCherry);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(6.5);
    }

    [Fact]
    public void Dado_CarritoVacio_Cuando_Agrego3LecheConPrecio1_33ElTotalDelCarrito_Debe_Ser3_99()
    {
        Producto leche = new Producto("Leche", 1.33);
        _carrito.Agregar(leche, 3);

        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(3.99);
    }

    [Fact]
    public void Dado_CarritoConUnaLecheConPrecio1_33_Cuando_Agrego2LecheConPrecio1_33ElTotalDelCarrito_Debe_Ser3_99()
    {
        Producto leche = new Producto("Leche", 1.33);
        _carrito.Agregar(leche);
        _carrito.Agregar(leche, 2);

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
        _carrito.Agregar(leche);
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
        _carrito.Agregar(leche, 2);
        _carrito.Agregar(huevos);
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
        _carrito.Agregar(leche, 5);
        _carrito.Agregar(huevos);
        _carrito.Agregar(baterias);
        _carrito.Agregar(frutosSecos);
        
        Recibo recibo = new Recibo(_carrito);

        var texto = recibo.Generar();

        texto.Should().Contain("Leche");
        texto.Should().Contain("Huevos");
        texto.Should().Contain("Baterias");
        texto.Should().Contain("Frutos secos");

        texto.Should().Contain($"TOTAL: {_carrito.CalcularTotal().ToString("F2", CultureInfo.InvariantCulture)}€");
    }

    [Fact]
    public void Dado_1KiloDeManzanasConPrecio1_99_Cuando_CalculoElDescuentoDel20Porciento_Debe_Retornar0_4()
    {
        Producto manzanas = new Producto("Manzanas", 1.99);
        var oferta = new DescuentoPorcentual("Manzanas", 20);

        var descuento = oferta.CalcularDescuento(manzanas, 1);

        descuento.Should().Be(0.4);
    }
    
    [Fact]
    public void Dado_1BolsaDeArrozConPrecio2_49_Cuando_CalculoElDescuentoDe10Porciento_Debe_Retornar0_25()
    {
        Producto arroz = new Producto("Arroz", 2.49);
        var oferta = new DescuentoPorcentual("Arroz", 10);

        var descuento = oferta.CalcularDescuento(arroz, 1);

        descuento.Should().Be(0.25);

    }
    
    [Fact]
    public void Dado_1BolsaDeLentejasConPrecio5_33_Cuando_CalculoElDescuentoDe15Porciento_Debe_Retornar0_8()
    {
        Producto lentejas = new Producto("Lentejas", 5.33);
        var oferta = new DescuentoPorcentual("Lentejas", 15);

        var descuento = oferta.CalcularDescuento(lentejas, 1);

        descuento.Should().Be(0.8);
    }
    
    [Fact]
    public void Dado_3KiloDeManzanasConPrecio1_99_Cuando_CalculoElDescuentoDel20Porciento_Debe_Retornar1_19()
    {
        Producto manzanas = new Producto("Manzanas", 1.99);
        var oferta = new DescuentoPorcentual("Manzanas", 20);

        var descuento = oferta.CalcularDescuento(manzanas, 3);

        descuento.Should().Be(1.19);
    }
    
    [Fact]
    public void Dado_3CepillosDeDientesConPrecio0_99_Cuando_CalculoElDescuentoDel3x2_Debe_Retornar0_99()
    {
        Producto cepilloDeDientes = new Producto("Cepillo de dientes", 0.99);
        var oferta = new DescuentoNxM("Cepillo de dientes", 3,2);

        var descuento = oferta.CalcularDescuento(cepilloDeDientes, 3);

        descuento.Should().Be(0.99);
    }
    
    [Fact]
    public void Dado_6CepillosDeDientesConPrecio0_99_Cuando_CalculoElDescuentoDel3x2_Debe_Retornar1_98()
    {
        Producto cepilloDeDientes = new Producto("Cepillo de dientes", 0.99);
        var oferta = new DescuentoNxM("Cepillo de dientes", 3,2);

        var descuento = oferta.CalcularDescuento(cepilloDeDientes, 6);

        descuento.Should().Be(1.98);
    }
    
    [Fact]
    public void Dado_9CepillosDeDientesConPrecio0_99_Cuando_CalculoElDescuentoDel3x2_Debe_Retornar2_97()
    {
        Producto cepilloDeDientes = new Producto("Cepillo de dientes", 0.99);
        var oferta = new DescuentoNxM("Cepillo de dientes", 3,2);

        var descuento = oferta.CalcularDescuento(cepilloDeDientes, 9);

        descuento.Should().Be(2.97);
    }
    
    [Fact]
    public void Dado_10CepillosDeDientesConPrecio0_99_Cuando_CalculoElDescuentoDel3x2_Debe_Retornar2_97()
    {
        Producto cepilloDeDientes = new Producto("Cepillo de dientes", 0.99);
        var oferta = new DescuentoNxM("Cepillo de dientes", 3,2);

        var descuento = oferta.CalcularDescuento(cepilloDeDientes, 9);

        descuento.Should().Be(2.97);
    }
    
    [Fact]
    public void Dado_5TubosDePastaDeDientesConPrecio1_79_Cuando_CalculoElDescuentoEspecial_Debe_Retornar1_46()
    {
        Producto pastaDeDientes = new Producto("Pasta de dientes", 1.79);
        var oferta = new DescuentoEspecial("Pasta de dientes", 5, 7.49);

        var descuento = oferta.CalcularDescuento(pastaDeDientes, 5);

        descuento.Should().Be(1.46);
    }
    
    [Fact]
    public void Dado_10TubosDePastaDeDientesConPrecio1_79_Cuando_CalculoElDescuentoEspecial_Debe_Retornar2_92()
    {
        Producto pastaDeDientes = new Producto("Pasta de dientes", 1.79);
        var oferta = new DescuentoEspecial("Pasta de dientes", 5, 7.49);

        var descuento = oferta.CalcularDescuento(pastaDeDientes, 10);

        descuento.Should().Be(2.92);
    }
    
    [Fact]
    public void Dado_15TubosDePastaDeDientesConPrecio1_79_Cuando_CalculoElDescuentoEspecial_Debe_Retornar4_38()
    {
        Producto pastaDeDientes = new Producto("Pasta de dientes", 1.79);
        var oferta = new DescuentoEspecial("Pasta de dientes", 5, 7.49);

        var descuento = oferta.CalcularDescuento(pastaDeDientes, 15);

        descuento.Should().Be(4.38);
    }
    
    [Fact]
    public void Dado_2CajasDeTomateCherryConPrecio0_99_Cuando_CalculoElDescuentoEspecial_Debe_Retornar0_39()
    {
        Producto tomates = new Producto("Tomate cherry", 0.69);
        var oferta = new DescuentoEspecial("Tomate cherry", 2, 0.99);

        var descuento = oferta.CalcularDescuento(tomates, 2);

        descuento.Should().Be(0.39);
    }
    
    [Fact]
    public void Dado_8CajasDeTomateCherryConPrecio0_99_Cuando_CalculoElDescuentoEspecial_Debe_Retornar4_56()
    {
        Producto tomates = new Producto("Tomate cherry", 0.69);
        var oferta = new DescuentoEspecial("Tomate cherry", 2, 0.99);

        var descuento = oferta.CalcularDescuento(tomates, 8);

        descuento.Should().Be(1.56);
    }

    [Fact]
    public void Dado_CarritoCon1KiloDeManzanaConPrecio1_99ElTotalDelCarrito_Debe_Retornar1_59()
    {
        Producto manzanas = new Producto("Manzanas", 1.99);
        var oferta = new DescuentoPorcentual("Manzanas", 20);
        _carrito.Agregar(manzanas);
        _carrito.AgregarOferta(oferta);
        
        var valorTotal = _carrito.CalcularTotal();

        valorTotal.Should().Be(1.59);
    }
}