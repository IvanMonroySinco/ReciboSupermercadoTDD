namespace ReciboDeSupermercado;

public class Carrito
{
    private Dictionary<Producto, int> _productos = new();
    private readonly List<DescuentoPorcentual> _ofertas = new();

    public double CalcularTotal()
    {
        double total = 0;

        foreach (var item in _productos)
        {
            var producto = item.Key;
            var cantidad = item.Value;
            var subtotal = producto.Precio * cantidad;

            if (producto.Nombre == "Manzanas" && _ofertas.Count > 0)
                subtotal -= _ofertas[0].CalcularDescuento(producto, cantidad);

            total += subtotal;
        }

        return Math.Round(total, 2);
    }

    public void Agregar(Producto producto)
    {
        Agregar(producto, 1);
    }

    public void Agregar(Producto producto, int cantidad)
    {
        if (_productos.ContainsKey(producto))
        {
            _productos[producto] += cantidad;
            return;
        }

        _productos.Add(producto, cantidad);
    }


    public Dictionary<Producto, int> ObtenerProductos()
    {
        return new Dictionary<Producto, int>(_productos);
    }

    public void AgregarOferta(DescuentoPorcentual oferta)
    {
        _ofertas.Add(oferta);
    }
}