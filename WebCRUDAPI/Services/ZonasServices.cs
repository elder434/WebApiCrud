using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCRUDAPI.Datos;
using WebCRUDAPI.Models;
using WebCRUDAPI.Services.Interfaces;

namespace WebCRUDAPI.Services
{
    public class ZonasServices : IZonasServices
    {
        private readonly lockersqaContext _context;
        public static readonly NLog.ILogger _log = NLog.LogManager.GetCurrentClassLogger();

        public ZonasServices(lockersqaContext context)
        {
            this._context = context;
        }


        public async Task<List<Zona>> GetZonasToList()
        {
            try
            {
                var zona = await _context.Zonas.ToListAsync();
                _log.Error("| WebCRUDAPI.Services.ZonasServices | RES: GetZonasToList:  " + zona);
                return zona;
            }
            catch (InvalidCastException e)
            {
                _log.Error("| WebCRUDAPI.Services.ZonasServices | E: GetZonasToList:  " + e);
                return null;
            }

            
        }

        public async Task<ZonaConRegiones> GetZonaConRegiones(int id)
        {
            try
            {
                _log.Info("| WebCRUDAPI.Services.ZonasServices | REQ: GetZonaConRegiones: "+ id);
                var zona = await _context.Zonas.Include(x => x.Regiones).Select(x => new ZonaConRegiones
                {
                    IdZona = x.IdZona,
                    SZona = x.SZona,
                    SRegion = x.Regiones.Select(x => x.SRegion).ToList(),
                }).FirstOrDefaultAsync(x => x.IdZona == id);
                _log.Info("| WebCRUDAPI.Services.ZonasServices | RES: GetZonaConRegiones: " + zona);
                return zona;
            }catch(Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.ZonasServices | E: GetZonaConRegiones: " + e);
                return null;
            }
        }

        public async Task<Zona> AgregarZona(ZonaAgregar zonaAgregar)
        {
            try
            {
                _log.Info("| WebCRUDAPI.Services.ZonasServices | R: AgregarZona: " + zonaAgregar);
                var zona = new Zona();
                zona.IdZona = zonaAgregar.IdZona;
                zona.SZona = zonaAgregar.SZona;

                await _context.Zonas.AddAsync(zona);
                await _context.SaveChangesAsync();

                return zona;
            }
            catch (Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.ZonasServices | E: AgregarZona: " + e);
                return null;
            }
        }


        public async Task<Zona> EditarZona(int id, ZonaAgregar zonaAgregar)
        {
            try
            {
                _log.Info("| WebCRUDAPI.Services.ZonasServices | R: EditarZona: " + zonaAgregar);
                var zonaToUpadate = await _context.Zonas.FirstOrDefaultAsync(x => x.IdZona == id);
                if (zonaToUpadate == null)
                {
                    return null;
                }

                zonaToUpadate.SZona = zonaAgregar.SZona;

                _context.Update(zonaToUpadate);
                await _context.SaveChangesAsync();

                return zonaToUpadate;
            }
            catch (Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.ZonasServices | E: EditarZona: " + e);
                return null;
            }
        }



        public async Task<Zona> EliminarZona(int id)
        {
            try
            {

                _log.Info("| WebCRUDAPI.Services.ZonasServices | R: EliminarZona: " + id);
                var zona = await _context.Zonas.FirstOrDefaultAsync(x => x.IdZona == id);
                if (zona == null)
                {
                    return null;
                }

                _context.Remove(zona);
                await _context.SaveChangesAsync();

                return zona;
            }catch(Exception e)
            {
                _log.Error("| WebCRUDAPI.Services.ZonasServices | E: EliminarZona: " + e);
                return null;
            }
        }

    }
}
