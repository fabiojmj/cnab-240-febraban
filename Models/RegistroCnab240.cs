using Cnab240.Enums;

namespace Cnab240.Models;

public abstract record RegistroCnab240
{
    public abstract TipoRegistro TipoRegistro { get; }
    public abstract string ToLinhaFormatada();
}
