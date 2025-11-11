namespace ReciboDeSupermercado;

public interface IOferta
{
    public double CalcularDescuento(Producto producto, int cantidad);
    public bool AplicaA(Producto producto);
}