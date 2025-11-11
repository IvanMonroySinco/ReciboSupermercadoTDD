namespace ReciboDeSupermercado;

public class DescuentoEspecial : IOferta
{
    private readonly string _nombreProducto;
    private readonly int _cantidad;
    private readonly double _precioEspecial;
    public DescuentoEspecial(string nombreProducto, int cantidad, double precioEspecial)
    {
        _nombreProducto = nombreProducto;
        _cantidad = cantidad;
        _precioEspecial = precioEspecial;
    }

    public double CalcularDescuento(Producto producto, int cantidad)
    {
        var gruposCantidades = cantidad / _cantidad;
        var precioNormal = gruposCantidades * _cantidad * producto.Precio;
        var precioConOferta = gruposCantidades * _precioEspecial;
        return Math.Round(precioNormal - precioConOferta, 2);
    }
    
    public bool AplicaA(Producto producto)
    {
        return producto.Nombre == _nombreProducto;
    }
}