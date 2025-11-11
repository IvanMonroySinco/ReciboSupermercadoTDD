namespace ReciboDeSupermercado;

public class DescuentoNxM
{  
    private readonly string _nombreProducto;
    private readonly int _cantidadCompra;
    private readonly int _cantidadPaga; 
    public DescuentoNxM(string nombreProducto, int contidadCompra, int cantidadPaga)
    {
        _nombreProducto = nombreProducto;
        _cantidadPaga = cantidadPaga;
        _cantidadCompra = contidadCompra;
    }
    
    public double CalcularDescuento(Producto producto, int cantidad)
    {
        var gruposCantidades = cantidad / _cantidadCompra;
        var cantidadesGratis = gruposCantidades * (_cantidadCompra - _cantidadPaga);
        return Math.Round(cantidadesGratis *  producto.Precio, 2);
    }
    
}