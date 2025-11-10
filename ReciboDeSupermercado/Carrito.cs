namespace ReciboDeSupermercado;

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
        if (_productos.ContainsKey(producto))
        {
            _productos[producto] += cantidad;
            return;
        }
        _productos.Add(producto, cantidad);
    }
}