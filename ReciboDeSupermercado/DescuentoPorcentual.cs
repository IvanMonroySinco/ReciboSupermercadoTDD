namespace ReciboDeSupermercado;

public class DescuentoPorcentual : IOferta
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
        var precioProducto = (producto.Precio * cantidad);
        return Math.Round(precioProducto * _porcentaje / 100, 2);
    }

    public bool AplicaA(Producto producto)
    {
        return producto.Nombre == _nombreProducto;
    }
}