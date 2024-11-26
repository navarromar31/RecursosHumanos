using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models.ViewModels;
using RecursosHumanos_Models;

public class ColaboradorController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IColaboradorRepositorio _colaboradorRepo;
    private readonly string WC_ImagenRuta = "imagenes/colaboradores";
    private readonly string WC_DepartamentoNombre = "Departamento";
    private readonly string WC_InstitucionNombre = "Institucion";
    private readonly string WC_PuestoNombre = "Puesto";

    public ColaboradorController(IColaboradorRepositorio colaboradorRepo, IWebHostEnvironment webHostEnvironment)
    {
        _colaboradorRepo = colaboradorRepo;
        _webHostEnvironment = webHostEnvironment;
    }

    // Acción para mostrar la lista de colaboradores
    public IActionResult Index()
    {
        var listaColaboradores = _colaboradorRepo.ObtenerTodos(incluirPropiedades: "Departamento,Institucion,Puesto");

        ColaboradorVM colaboradorVM = new ColaboradorVM()
        {
            Colaboradores = listaColaboradores
        };

        return View(colaboradorVM);
    }

    public IActionResult Crear()
    {
        ColaboradorVM colaboradorVM = new ColaboradorVM()
        {
            Colaborador = new Colaborador(),
            Departamento = _colaboradorRepo.ObtenerTodosDropDownList(WC_DepartamentoNombre),
            Institucion = _colaboradorRepo.ObtenerTodosDropDownList(WC_InstitucionNombre),
            Puesto = _colaboradorRepo.ObtenerTodosDropDownList(WC_PuestoNombre)
        };

        return View(colaboradorVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(ColaboradorVM colaboradorVM)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (files != null && files.Count > 0)
                {
                    string upload = Path.Combine(webRootPath, WC_ImagenRuta);

                    // Verificar si la carpeta de destino existe
                    if (!Directory.Exists(upload))
                    {
                        Directory.CreateDirectory(upload);
                    }

                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName).ToLower();

                    // Validar si el archivo es una imagen válida
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        ModelState.AddModelError("Imagen", "El archivo debe ser una imagen válida (.jpg, .jpeg, .png).");
                        return View(colaboradorVM);
                    }

                    // Guardar el archivo en el servidor
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    colaboradorVM.Colaborador.ImagenUrlCol = fileName + extension;
                }

                // Agregar el colaborador a la base de datos
                _colaboradorRepo.Agregar(colaboradorVM.Colaborador);
                _colaboradorRepo.Grabar();

                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones, loguear el error y agregar mensaje al modelo
            ModelState.AddModelError("", "Hubo un error al guardar el colaborador. Intente nuevamente.");
            Console.WriteLine("Error al crear colaborador: " + ex.Message);
        }

        // Si no es válido, regresamos a la vista Crear con el ViewModel y los dropdowns
        colaboradorVM.Departamento = _colaboradorRepo.ObtenerTodosDropDownList(WC_DepartamentoNombre);
        colaboradorVM.Institucion = _colaboradorRepo.ObtenerTodosDropDownList(WC_InstitucionNombre);
        colaboradorVM.Puesto = _colaboradorRepo.ObtenerTodosDropDownList(WC_PuestoNombre);

        return View(colaboradorVM);
    }
}
