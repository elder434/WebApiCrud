using Microsoft.AspNetCore.Mvc;
using WebCRUDAPI.Datos;
using WebCRUDAPI.Models;

namespace WebCRUDAPI.Services.Interfaces
{
    public interface IFechaServices
    {
        public Task<List<VecesUsados>> CasillasUsadas(fechas date);
        public Task<List<UsuariosPedidos>> GetByUsuarioMasUso(fechas date);
        public Task<List<UsuariosPedidos>> GetByUsuarioMenosUso(fechas date);
        public Task<List<Entregados>> PedidosCompleto(fechas date);
        public Task<List<Completados>> PedidosPorCompletados(fechas date);
        public Task<List<SinCompletados>> PedidosSinCompletados(fechas date);
        public Task<List<Object>> PedidoMasRapido(fechas date);
    }
}
