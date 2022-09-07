using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: CasillasUsadas:  "+ date);
                var fechas = await _context.Pedidos.Select(x => new
                {
                    Oficina = x.IdOficina,
                    Locker = x.IdLocker,
                    casillero = x.IdCasillero,
                    fecha = Convert.ToDateTime(x.FechReg),
                }).ToListAsync();

                var list = new List<VecesUsados>();

                foreach (var fecha in fechas)
                {

                    if (fecha.fecha >= date.fechaInicio && fecha.fecha <= date.fechaFinal)
                    {
                        var tiene = false;
                        foreach (var lista in list)
                        {
                            if (lista.Casillero == fecha.casillero && lista.Oficina == fecha.Oficina && lista.Locker == fecha.Locker)
                            {
                                tiene = true;
                                lista.VecesUsado++;
                                break;
                            }
                        }

                        if (!tiene)
                        {
                            var vecesUsado = new VecesUsados
                            {
                                Oficina = fecha.Oficina,
                                Locker = fecha.Locker,
                                Casillero = fecha.casillero,
                                VecesUsado = 1,
                            };
                            list.Add(vecesUsado);
                        }

                    }
                }
                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: CasillasUsadas:  " + list);
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
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: GetByUsuarioMasUso:  " + date);
                var fechas = await _context.Pedidos.Include(x => x.SDniCliNavigation).Select(x => new
                {
                    usuario = x.SDniCliNavigation.SNombre,
                    RUT = x.SDniCliNavigation.SDniCli,
                    Oficina = x.IdOficina,
                    Locker = x.IdLocker,
                    casillero = x.IdCasillero,
                    fecha = Convert.ToDateTime(x.FechReg),
                }).ToListAsync();

                var list = new List<UsuariosPedidos>();

                foreach (var fecha in fechas)
                {
                    if (fecha.fecha >= date.fechaInicio && fecha.fecha <= date.fechaFinal)
                    {

                        var tiene = false;
                        foreach (var lista in list)
                        {
                            if (lista.Rut == fecha.RUT && lista.Casillero == fecha.casillero && lista.Oficina == fecha.Oficina && lista.Locker == fecha.Locker)
                            {
                                tiene = true;
                                lista.VecesUsado++;
                                break;
                            }
                        }

                        if (!tiene)
                        {
                            var vecesUsado = new UsuariosPedidos
                            {
                                Rut = fecha.RUT,
                                Nombre = fecha.usuario,
                                Oficina = fecha.Oficina,
                                Locker = fecha.Locker,
                                Casillero = fecha.casillero,
                                VecesUsado = 1,
                            };
                            list.Add(vecesUsado);
                        }

                    }
                }
                foreach (var lista in list.ToList())
                {
                    foreach (var listaComparacion in list.ToList())
                    {
                        if (lista == listaComparacion)
                        {
                            continue;
                        }
                        if (lista.Locker == listaComparacion.Locker && lista.Oficina == listaComparacion.Oficina)
                        {
                            if (lista.Casillero == listaComparacion.Casillero)
                            {
                                if (lista.VecesUsado >= listaComparacion.VecesUsado)
                                {
                                    list.Remove(listaComparacion);
                                }
                                else
                                {
                                    list.Remove(lista);
                                }
                            }
                        }
                    }
                }
                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: GetByUsuarioMasUso:  " + list);
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
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: GetByUsuarioMenosUso:  " + date);

                var fechas = await _context.Pedidos.Include(x => x.SDniCliNavigation).Select(x => new
                {
                    usuario = x.SDniCliNavigation.SNombre,
                    RUT = x.SDniCliNavigation.SDniCli,
                    Oficina = x.IdOficina,
                    Locker = x.IdLocker,
                    casillero = x.IdCasillero,
                    fecha = Convert.ToDateTime(x.FechReg),
                }).ToListAsync();

                var list = new List<UsuariosPedidos>();

                foreach (var fecha in fechas)
                {
                    if (fecha.fecha >= date.fechaInicio && fecha.fecha <= date.fechaFinal)
                    {

                        var tiene = false;
                        foreach (var lista in list)
                        {
                            if (lista.Rut == fecha.RUT && lista.Casillero == fecha.casillero && lista.Oficina == fecha.Oficina && lista.Locker == fecha.Locker)
                            {
                                tiene = true;
                                lista.VecesUsado++;
                                break;
                            }
                        }

                        if (!tiene)
                        {
                            var vecesUsado = new UsuariosPedidos
                            {
                                Rut = fecha.RUT,
                                Nombre = fecha.usuario,
                                Oficina = fecha.Oficina,
                                Locker = fecha.Locker,
                                Casillero = fecha.casillero,
                                VecesUsado = 1,
                            };
                            list.Add(vecesUsado);
                        }

                    }
                }

                foreach (var lista in list.ToList())
                {
                    foreach (var listaComparacion in list.ToList())
                    {
                        if (lista == listaComparacion)
                        {
                            continue;
                        }
                        if (lista.Locker == listaComparacion.Locker && lista.Oficina == listaComparacion.Oficina)
                        {
                            if (lista.Casillero == listaComparacion.Casillero)
                            {
                                if (lista.VecesUsado <= listaComparacion.VecesUsado)
                                {
                                    list.Remove(listaComparacion);
                                }
                                else
                                {
                                    list.Remove(lista);
                                }
                            }
                        }
                    }
                }
                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: GetByUsuarioMenosUso:  " + list);
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
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: PedidosCompleto:  " + date);

                var fechas = await _context.Pedidos.Include(x => x.SDniCliNavigation).Select(x => new
                {
                    Oficina = x.IdOficina,
                    Locker = x.IdLocker,
                    casillero = x.IdCasillero,
                    fecha = Convert.ToDateTime(x.FechReg),
                    fechaEntregado = x.FechOut,
                }).ToListAsync();

                var list = new List<Entregados>();

                foreach (var fecha in fechas)
                {
                    if (fecha.fecha >= date.fechaInicio && fecha.fecha <= date.fechaFinal)
                    {
                        if (fecha.fechaEntregado != null)
                        {
                            var entregado = new Entregados()
                            {
                                Oficina = fecha.Oficina,
                                Locker = fecha.Locker,
                                Casillero = fecha.casillero,
                                FechaRegistro = fecha.fecha,
                                FechaEntregado = Convert.ToDateTime(fecha.fechaEntregado).ToShortDateString(),
                            };

                            list.Add(entregado);
                        }
                    }
                }
                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: PedidosCompleto:  " + list);
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
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: PedidosPorCompletados:  " + date);

                var fechas = await _context.Pedidos.Include(x => x.SDniCliNavigation).Select(x => new
                {
                    Oficina = x.IdOficina,
                    Locker = x.IdLocker,
                    casillero = x.IdCasillero,
                    fecha = Convert.ToDateTime(x.FechReg),
                    fechaEntregado = x.FechOut,
                }).ToListAsync();

                var list = new List<Completados>();

                foreach (var fecha in fechas)
                {

                    if (fecha.fecha >= date.fechaInicio && fecha.fecha <= date.fechaFinal)
                    {
                        var tiene = false;
                        if (fecha.fechaEntregado != null)
                        {
                            foreach (var lista in list)
                            {
                                if (lista.Casillero == fecha.casillero && lista.Oficina == fecha.Oficina && lista.Locker == fecha.Locker)
                                {
                                    tiene = true;
                                    lista.PedidosEntregados++;
                                    break;
                                }
                            }

                            if (!tiene)
                            {
                                var entregado = new Completados()
                                {
                                    Oficina = fecha.Oficina,
                                    Locker = fecha.Locker,
                                    Casillero = fecha.casillero,
                                    PedidosEntregados = 1,
                                };

                                list.Add(entregado);
                            }
                        }

                    }
                }
                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: PedidosPorCompletados:  " + list);
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

                var fechas = await _context.Pedidos.Include(x => x.SDniCliNavigation).Select(x => new
                {
                    Oficina = x.IdOficina,
                    Locker = x.IdLocker,
                    casillero = x.IdCasillero,
                    fecha = Convert.ToDateTime(x.FechReg),
                    fechaEntregado = x.FechOut,
                }).ToListAsync();

                var list = new List<SinCompletados>();

                foreach (var fecha in fechas)
                {

                    if (fecha.fecha >= date.fechaInicio && fecha.fecha <= date.fechaFinal)
                    {
                        var tiene = false;
                        if (fecha.fechaEntregado == null)
                        {
                            foreach (var lista in list)
                            {
                                if (lista.Casillero == fecha.casillero && lista.Oficina == fecha.Oficina && lista.Locker == fecha.Locker)
                                {
                                    tiene = true;
                                    lista.PedidosNoEntregados++;
                                    break;
                                }
                            }

                            if (!tiene)
                            {
                                var entregado = new SinCompletados()
                                {
                                    Oficina = fecha.Oficina,
                                    Locker = fecha.Locker,
                                    Casillero = fecha.casillero,
                                    PedidosNoEntregados = 1,
                                };

                                list.Add(entregado);
                            }
                        }

                    }
                }
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
                _log.Info("| WebCRUDAPI.Services.FechasServices | REQ: PedidoMasRapido:  " + date);

                var fechas = await _context.Pedidos.Include(x => x.SDniCliNavigation).Select(x => new
                {
                    Oficina = x.IdOficina,
                    Locker = x.IdLocker,
                    casillero = x.IdCasillero,
                    fecha = Convert.ToDateTime(x.FechReg),
                    fechaEntregado = x.FechOut,
                }).ToListAsync();

                var list = new List<CompletadosConTiempo>();

                foreach (var fecha in fechas)
                {
                    if (fecha.fecha >= date.fechaInicio && fecha.fecha <= date.fechaFinal)
                    {
                        if (fecha.fechaEntregado != null)
                        {
                            var registrada = false;
                            foreach (var lista in list)
                            {
                                if (lista.Casillero == fecha.casillero && lista.Oficina == fecha.Oficina && lista.Locker == fecha.Locker)
                                {
                                    var resultado = fecha.fechaEntregado - fecha.fecha;
                                    var resultadoLista = lista.PedidosEntregados - lista.Registrado;
                                    registrada = true;
                                    if (resultado < resultadoLista)
                                    {
                                        var pedido = new CompletadosConTiempo()
                                        {
                                            Oficina = fecha.Oficina,
                                            Locker = fecha.Locker,
                                            Casillero = fecha.casillero,
                                            Registrado = fecha.fecha,
                                            PedidosEntregados = Convert.ToDateTime(fecha.fechaEntregado),
                                        };
                                        list.Remove(lista);
                                        list.Add(pedido);
                                        break;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            if (!registrada)
                            {
                                var pedido = new CompletadosConTiempo()
                                {
                                    Oficina = fecha.Oficina,
                                    Locker = fecha.Locker,
                                    Casillero = fecha.casillero,
                                    Registrado = fecha.fecha,
                                    PedidosEntregados = Convert.ToDateTime(fecha.fechaEntregado),
                                };
                                list.Add(pedido);
                            }
                        }
                    }
                }

                var final = new List<Object>();

                for (int i = 0; i < list.Count; i++)
                {
                    var cadena = "";
                    var resultadoLista = list[i].PedidosEntregados - list[i].Registrado;

                    if (resultadoLista.Days > 0)
                    {
                        cadena = cadena + resultadoLista.Days.ToString() + " Dias ";
                    }

                    if (resultadoLista.Hours > 0)
                    {
                        cadena = cadena + resultadoLista.Hours.ToString() + " Horas ";
                    }

                    if (resultadoLista.Seconds > 0)
                    {
                        cadena = cadena + resultadoLista.Seconds.ToString() + " Segundos ";
                    }



                    var obj = new
                    {
                        locker = list[i].Locker,
                        Oficina = list[i].Oficina,
                        Casillero = list[i].Casillero,
                        FechaRegistro = list[i].Registrado,
                        TiempoDentro = cadena,
                    };
                    final.Add(obj);
                }
                _log.Info("| WebCRUDAPI.Services.FechasServices | RES: PedidoMasRapido:  " + final);
                return final;
            }catch(Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.FechasServices | E: PedidoMasRapido:  " + e);
                return null;
            }
        }
    }
}
