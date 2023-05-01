using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Seleccion
{
    public int Id { get; set; }

    public string NombreSeleccion { get; set; } = null!;

    public int Corriente { get; set; }

    public int Tension { get; set; }

    public string Instalacion { get; set; } = null!;

    public string Material { get; set; } = null!;

    public string NombreProyecto { get; set; } = null!;

    public DateTime? FechaInsercion { get; set; }
}
