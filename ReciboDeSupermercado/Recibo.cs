using System.Globalization;
using System.Text;

namespace ReciboDeSupermercado;

public class Recibo
{
    private readonly Carrito _carrito;

    public Recibo(Carrito carrito = null)
    {
        _carrito = carrito;
    }

    public string Generar()
    {
        if (_carrito == null)
            return "TOTAL: 0.00€";

        var recibo = new StringBuilder();
        double totalDescuentos = 0;

        
        var productos = _carrito.ObtenerProductos();
        foreach (var producto in productos)
        {
            var nombre = producto.Key.Nombre;
            var cantidad = producto.Value;
            var subtotal = producto.Key.Precio * cantidad;
            recibo.AppendLine($"{nombre} x {cantidad} - {subtotal.ToString("F2", CultureInfo.InvariantCulture)}€");
            
            foreach (var oferta in _carrito.ObtenerOfertas())
            {
                if (oferta.AplicaA(producto.Key))
                {
                    var descuento = oferta.CalcularDescuento(producto.Key, cantidad);
                    if (descuento > 0)
                    {
                        recibo.AppendLine($"Descuento: -{descuento.ToString("F2", CultureInfo.InvariantCulture)}€");
                        totalDescuentos += descuento;
                    }
                }
            }
            
        }
        recibo.AppendLine($"SUBTOTAL: {_carrito.CalcularSubtotal().ToString("F2", CultureInfo.InvariantCulture)}€");
        recibo.AppendLine($"DESCUENTOS: -{totalDescuentos.ToString("F2", CultureInfo.InvariantCulture)}€");
        recibo.AppendLine($"TOTAL: {_carrito.CalcularTotal().ToString("F2", CultureInfo.InvariantCulture)}€");
        return recibo.ToString();
    }
}