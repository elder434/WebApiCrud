using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using NLog.Fluent;
using WebCRUDAPI.Datos;
using WebCRUDAPI.Models;
using WebCRUDAPI.Services.Interfaces;

namespace WebCRUDAPI.Services
{
    public class FechasServices : IFechaServices
    {
        private readonly lockersqaContext _context;
        public static readonly NLog.ILogger _log = NLog.LogManager.GetCurrentClassLogger();

        public FechasServices(lockersqaContext context)
        {
            this._context = context;
        }



        public async Task<List<VecesUsados>> CasillasUsadas(fechas date)
        {
            
            try
            {
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: CasillasUsadas:  "+ JsonConvert.SerializeObject(date));


                var list = await (from p in _context.Pedidos
                                    where p.FechReg >= date.fechaInicio && p.FechReg <= date.fechaFinal
                                    group p by new { p.IdLocker, p.IdOficina, p.IdCasillero } into total
                                    select new VecesUsados
                                    {
                                        Locker = total.Key.IdLocker,
                                        Oficina = total.Key.IdOficina,
                                        Casillero = total.Key.IdCasillero,
                                        VecesUsado = total.Count()
                                    }).ToListAsync();

                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: CasillasUsadas:  " + JsonConvert.SerializeObject(list));
                return list;
            }
            catch(Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.FechasServices | E: CasillasUsadas:  " + e);
                return null;
            }
            
        }

        public async Task<List<UsuariosPedidos>> GetByUsuarioMasUso(fechas date)
        {
            try
            {
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: GetByUsuarioMasUso:  " + JsonConvert.SerializeObject(date));


                var lista =(from p in _context.Pedidos
                                  where p.FechReg >= date.fechaInicio && p.FechReg <= date.fechaFinal
                                  group p by new { p.SDniCliNavigation.SNombre, p.SDniCli, p.IdLocker, p.IdOficina, p.IdCasillero } into total
                                  select new UsuariosPedidos
                                  {
                                      Rut = total.Key.SDniCli,
                                      Nombre = total.Key.SNombre,
                                      Locker = total.Key.IdLocker,
                                      Oficina = total.Key.IdOficina,
                                      Casillero = total.Key.IdCasillero,
                                      VecesUsado = total.Count()
                                  });

                var list = lista.OrderBy(x => x.VecesUsado).ToList().GroupBy(p => new { p.Rut, p.Oficina, p.Casillero, p.Nombre, p.Locker }).Select(p => p.First()).ToList();
                list = list.OrderByDescending(x => x.VecesUsado).GroupBy(p => new { p.Oficina, p.Casillero, p.Locker }).Select(p => p.First()).ToList();

                
                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: GetByUsuarioMasUso:  " + JsonConvert.SerializeObject(list));
                return list;
            }catch(Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.FechasServices | E: GetByUsuarioMasUso:  " + e);
                return null;
            }
        }



        public async Task<List<UsuariosPedidos>> GetByUsuarioMenosUso(fechas date)
        {
            try
            {
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: GetByUsuarioMenosUso:  " + JsonConvert.SerializeObject(date));

                var lista =(from p in _context.Pedidos
                                  where p.FechReg >= date.fechaInicio && p.FechReg <= date.fechaFinal
                                  group p by new { p.SDniCliNavigation.SNombre, p.SDniCli, p.IdLocker, p.IdOficina, p.IdCasillero } into total
                                  select new UsuariosPedidos
                                  {
                                      Rut = total.Key.SDniCli,
                                      Nombre = total.Key.SNombre,
                                      Locker = total.Key.IdLocker,
                                      Oficina = total.Key.IdOficina,
                                      Casillero = total.Key.IdCasillero,
                                      VecesUsado = total.Count()
                                  });

                var list = lista.OrderByDescending(x => x.VecesUsado).ToList().GroupBy(p => new { p.Rut, p.Oficina, p.Casillero, p.Nombre, p.Locker }).Select(p => p.First()).ToList();
                list = list.OrderBy(x => x.VecesUsado).GroupBy(p => new { p.Oficina, p.Casillero, p.Locker }).Select(p => p.First()).ToList();

                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: GetByUsuarioMenosUso:  " + JsonConvert.SerializeObject(list));
                return list;
            }catch(Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.FechasServices | E: GetByUsuarioMenosUso:  " + e);
                return null;
            }
        }




        public async Task<List<Entregados>> PedidosCompleto(fechas date)
        {
            try
            {
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: PedidosCompleto:  " + JsonConvert.SerializeObject(date));

                var list = await (from p in _context.Pedidos
                                   where p.FechReg >= date.fechaInicio && p.FechReg <= date.fechaFinal
                                   && p.FechOut != null
                                   select new Entregados
                                   {
                                       Oficina = p.IdOficina,
                                       Locker = p.IdLocker,
                                       Casillero = p.IdCasillero,
                                       FechaRegistro = Convert.ToDateTime(p.FechReg),
                                       FechaEntregado = Convert.ToDateTime(p.FechOut)
                                   }).ToListAsync();

                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: PedidosCompleto:  " + JsonConvert.SerializeObject(list));
                return list;
            }catch(Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.FechasServices | E: PedidosCompleto:  " + e);
                return null;
            }
        }


        public async Task<List<Completados>> PedidosPorCompletados(fechas date)
        {
            try
            {
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: PedidosPorCompletados:  " + JsonConvert.SerializeObject(date));


                var list = await (from p in _context.Pedidos
                                  where p.FechReg >= date.fechaInicio && p.FechReg <= date.fechaFinal
                                  && p.FechOut != null
                                  group p by new {p.IdCasillero, p.IdLocker, p.IdOficina} into total
                                  select new Completados
                                  {
                                      Oficina = total.Key.IdOficina,
                                      Locker = total.Key.IdLocker,
                                      Casillero = total.Key.IdCasillero,
                                      PedidosEntregados = total.Count()
                                  }).ToListAsync();

                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: PedidosPorCompletados:  " + JsonConvert.SerializeObject(list));
                return list;
            }catch(Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.FechasServices | E: PedidosPorCompletados:  " + e);
                return null;
            }
        }


        public async Task<List<SinCompletados>> PedidosSinCompletados(fechas date)
        {
            try
            {
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: PedidosSinCompletados:  " + date);

                var list = await (from p in _context.Pedidos
                                  where p.FechReg >= date.fechaInicio && p.FechReg <= date.fechaFinal
                                  && p.FechOut == null
                                  group p by new { p.IdCasillero, p.IdLocker, p.IdOficina } into total
                                  select new SinCompletados
                                  {
                                      Oficina = total.Key.IdOficina,
                                      Locker = total.Key.IdLocker,
                                      Casillero = total.Key.IdCasillero,
                                      PedidosNoEntregados = total.Count()
                                  }).ToListAsync();

                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: PedidosSinCompletados:  " + list);
                return list;
            }catch(Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.FechasServices | E: PedidosSinCompletados:  " + e);
                return null;
            }
        }



        public async Task<List<Object>> PedidoMasRapido(fechas date)
        {
            try
            {
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: PedidoMasRapido:  " + JsonConvert.SerializeObject(date));

                var lista = _context.Pedidos.Where(p => p.FechReg >= date.fechaInicio && p.FechReg <= date.fechaFinal && p.FechOut != null).Select(p => new CompletadosConTiempo
                {
                    Oficina = p.IdOficina,
                    Locker = p.IdLocker,
                    Casillero = p.IdCasillero,
                    Registrado = Convert.ToDateTime(p.FechReg),
                    PedidosEntregados = Convert.ToDateTime(p.FechOut) - Convert.ToDateTime(p.FechReg)
                });

                var list = lista.ToList().GroupBy(p => new { p.Oficina, p.Locker, p.Casillero, p.Registrado, p.PedidosEntregados }).Select(p=> p);

                var listaFinal = list.OrderBy(x => x.Key.PedidosEntregados).GroupBy(p => new { p.Key.Oficina, p.Key.Locker, p.Key.Casillero }).Select(x => x.First()).ToList();






                var final = new List<Object>();

                for (int i = 0; i < listaFinal.Count; i++)
                {
                    var cadena = "";
                    var resultadoLista = listaFinal[i].Key.PedidosEntregados;

                    if (resultadoLista.Days > 0)
                    {
                        cadena = cadena + resultadoLista.Days.ToString() + " Dias ";
                    }

                    if (resultadoLista.Hours > 0)
                    {
                        cadena = cadena + resultadoLista.Hours.ToString() + " Horas ";
                    }

                    if (resultadoLista.Minutes > 0)
                    {
                        cadena = cadena + resultadoLista.Minutes.ToString() + " Minutos ";
                    }

                    if (resultadoLista.Seconds > 0)
                    {
                        cadena = cadena + resultadoLista.Seconds.ToString() + " Segundos ";
                    }



                    var obj = new
                    {
                        locker = listaFinal[i].Key.Locker,
                        Oficina = listaFinal[i].Key.Oficina,
                        Casillero = listaFinal[i].Key.Casillero,
                        FechaRegistro = listaFinal[i].Key.Registrado,
                        TiempoDentro = cadena,
                    };
                    final.Add(obj);
                }
                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: PedidoMasRapido:  " + JsonConvert.SerializeObject(final));
                return final;
            }
            catch (Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.FechasServices | E: PedidoMasRapido:  " + e);
                return null;
            }
        }

    }
}
