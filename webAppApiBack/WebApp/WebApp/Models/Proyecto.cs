using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string NombreProyecto { get; set; } = null!;

    public string TipoProyecto { get; set; } = null!;

    public string Usuario { get; set; } = null!;
}
