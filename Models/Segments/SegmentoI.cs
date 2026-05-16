using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento I — Saldo em Conta Investimento (Detalhe Tipo 3, Segmento I).</summary>
public sealed record SegmentoI : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'I';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "I"</summary>
    // Fixed literal "I" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-102 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Valor Total em CDS — pos 103-120 (I001), 2 decimais</summary>
    public decimal ValorTotalCDS { get; init; }

    /// <summary>Valor Disponível — pos 121-138 (I002), 2 decimais</summary>
    public decimal ValorDisponivel { get; init; }

    /// <summary>Valor Vinculado — pos 139-156 (I003), 2 decimais</summary>
    public decimal ValorVinculado { get; init; }

    /// <summary>Valor Bloqueado — pos 157-174 (I004), 2 decimais</summary>
    public decimal ValorBloqueado { get; init; }

    /// <summary>Uso Reservado CNAB 2 — pos 175-240 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento I.</summary>
    public static SegmentoI Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoI
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 102),
            ValorTotalCDS = linha.ExtrairDecimal(103, 120, 2),
            ValorDisponivel = linha.ExtrairDecimal(121, 138, 2),
            ValorVinculado = linha.ExtrairDecimal(139, 156, 2),
            ValorBloqueado = linha.ExtrairDecimal(157, 174, 2),
            CNAB2 = linha.ExtrairAlfa(175, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),             // 01: 1-3    (3)
            LoteServico.PadNum(4),             // 02: 4-7    (4)
            "3",                               // 03: 8-8    (1) fixo
            NumeroSequencial.PadNum(5),        // 04: 9-13   (5)
            "I",                               // 05: 14-14  (1) fixo
            "".PadAlfa(88),                    // 06: 15-102 (88) CNAB Brancos
            ValorTotalCDS.PadDecimal(18, 2),   // 07: 103-120 (18)
            ValorDisponivel.PadDecimal(18, 2), // 08: 121-138 (18)
            ValorVinculado.PadDecimal(18, 2),  // 09: 139-156 (18)
            ValorBloqueado.PadDecimal(18, 2),  // 10: 157-174 (18)
            "".PadAlfa(66)                     // 11: 175-240 (66) CNAB2 Brancos
        );
    // SOMA: 3+4+1+5+1+88+18+18+18+18+66 = 240 ✓
}
