using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Records;

/// <summary>Trailer do Arquivo CNAB 240 (Tipo 9).</summary>
public sealed record TrailerArquivo : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.TrailerArquivo;

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002), fixo "9999"</summary>
    public int LoteServico { get; init; } = 9999;

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "9"</summary>
    // Fixed literal "9" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 9-17 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Quantidade de Lotes do Arquivo — pos 18-23 (G049)</summary>
    public int QtdeLotes { get; init; }

    /// <summary>Quantidade de Registros do Arquivo — pos 24-29 (G056)</summary>
    public int QtdeRegistros { get; init; }

    /// <summary>Quantidade de Contas para Conciliação — pos 30-35 (G037)</summary>
    public int QtdeConcil { get; init; }

    /// <summary>Uso Reservado CNAB 2 — pos 36-240 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Trailer do Arquivo.</summary>
    public static TrailerArquivo Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new TrailerArquivo
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            CNAB = linha.ExtrairAlfa(9, 17),
            QtdeLotes = linha.ExtrairInt(18, 23),
            QtdeRegistros = linha.ExtrairInt(24, 29),
            QtdeConcil = linha.ExtrairInt(30, 35),
            CNAB2 = linha.ExtrairAlfa(36, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),   // 01: 1-3   (3)
            "9999",                   // 02: 4-7   (4) fixo
            "9",                      // 03: 8-8   (1) fixo
            "".PadAlfa(9),           // 04: 9-17  (9) CNAB Brancos
            QtdeLotes.PadNum(6),     // 05: 18-23 (6)
            QtdeRegistros.PadNum(6), // 06: 24-29 (6)
            QtdeConcil.PadNum(6),    // 07: 30-35 (6)
            "".PadAlfa(205)          // 08: 36-240 (205) CNAB2 Brancos
        );
    // SOMA: 3+4+1+9+6+6+6+205 = 240 ✓
}
