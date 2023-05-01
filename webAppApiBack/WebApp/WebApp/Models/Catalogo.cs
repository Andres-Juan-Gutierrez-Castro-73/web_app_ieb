using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Catalogo
{
    public int Id { get; set; }

    public string Material { get; set; } = null!;

    public int EspesorPantalla { get; set; }

    public int DiametroCable { get; set; }

    public decimal Ampacidad { get; set; }

    public int CodigoCable { get; set; }

    public int Corriente { get; set; }

    public int Tension { get; set; }
}
