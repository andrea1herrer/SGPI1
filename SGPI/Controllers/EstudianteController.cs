using Microsoft.AspNetCore.Mvc;
using SGPI.Models;

namespace SGPI.Controllers
{
    public class EstudianteController : Controller
    {
        SGPIDBContext context = new SGPIDBContext();
        public IActionResult ActualizarEst(int ? IdUsuario)
        {
            Usuario usuario = context.Usuarios.Find(IdUsuario);

            if (usuario != null)
            {
                ViewBag.documento = context.Documentos.ToList();
                ViewBag.genero = context.Generos.ToList();
                ViewBag.programa = context.Programas.ToList();
                return View(usuario);
            }
            else
            {
                return Redirect("/Administrador/Login");
            }
            
        }

        [HttpPost]
        public IActionResult ActualizarEst(Usuario usuario)
        {
            var usuarioActualizar = context.Usuarios.Where(consulta => consulta.IdUsuario == usuario.IdUsuario).FirstOrDefault();

            //Usuario usuarioActualizar = context.Usuarios.Find(usuario);
            usuarioActualizar.NumeroDocumento = usuario.NumeroDocumento;
            usuarioActualizar.PrimerNombre = usuario.PrimerNombre;
            usuarioActualizar.SegundoNombre = usuario.SegundoNombre;
            usuarioActualizar.PrimerApellido = usuario.PrimerApellido;
            usuarioActualizar.SegundoApellido = usuario.SegundoApellido;
            usuarioActualizar.IdPrograma = usuario.IdPrograma;
            usuarioActualizar.IdGenero = usuario.IdGenero;
            usuarioActualizar.Password = usuario.Password;

            context.Update(usuarioActualizar);    
            context.SaveChanges();

            return Redirect("/Estudiante/ActualizarEst/?Idusuario=" + usuarioActualizar.IdUsuario);
        }

        public IActionResult PagosEst()
        {
            return View();  
        }
        [HttpPost]

        public IActionResult PagosEst(Pago pago, Estudiante est)
        {
            var pagUpdate = context.Estudiantes.Where(consulta => consulta.IdUsuario == est.IdUsuario).FirstOrDefault();

            pago.Estado = true;
            context.Pagos.Add(pago);
            context.SaveChanges();
            ViewBag.mensaje = "Pago registrado";

            pagUpdate.IdPago = pago.IdPago;
            pagUpdate.IdUsuario = est.IdUsuario;
            pagUpdate.Archivo = "";
            pagUpdate.Egreado = true;

            context.Update(pagUpdate);
            context.SaveChanges();

            return View();
        }
    }
}
