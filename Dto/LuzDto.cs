using System.ComponentModel.DataAnnotations;

public class LuzCreateDto
{
    [Required]
    public double Quantidade { get; set; } // kg, litros, etc...

    [Required]
    public int CasaId { get; set; }

    [Required]
    public double Limite { get; set; }

    [Required]
    [MaxLength(100)] // Um limite de caracteres para o identificador
    public string Identificador { get; set; } = string.Empty;
}
public class LuzAtualizarDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public double Quantidade { get; set; } // kg, litros, etc...

    [Required]
    public int CasaId { get; set; }

    [Required]
    public double Limite { get; set; }

    [Required]
    [MaxLength(100)] // Um limite de caracteres para o identificador
    public string Identificador { get; set; } = string.Empty;
}
