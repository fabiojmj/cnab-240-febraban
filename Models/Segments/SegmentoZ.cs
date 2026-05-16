using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento Z — Autenticação Bancária (Detalhe Tipo 3, Segmento Z).</summary>
public sealed record SegmentoZ : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'Z';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "Z"</summary>
    // Fixed literal "Z" in ToLinhaFormatada

    /// <summary>Autenticação Legislação — pos 15-78 (Z001)</summary>
    public string AutenticacaoLegislacao { get; init; } = "";

    /// <summary>Autenticação Bancária — pos 79-103 (Z002)</summary>
    public string AutenticacaoBancaria { get; init; } = "";

    /// <summary>Uso Reservado CNAB — pos 104-230 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Códigos de Ocorrências — pos 231-240 (G061)</summary>
    public string CodigosOcorrencias { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento Z.</summary>
    public static SegmentoZ Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoZ
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            AutenticacaoLegislacao = linha.ExtrairAlfa(15, 78),
            AutenticacaoBancaria = linha.ExtrairAlfa(79, 103),
            CNAB = linha.ExtrairAlfa(104, 230),
            CodigosOcorrencias = linha.ExtrairAlfa(231, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),               // 01: 1-3   (3)
            LoteServico.PadNum(4),               // 02: 4-7   (4)
            "3",                                  // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),          // 04: 9-13  (5)
            "Z",                                  // 05: 14-14 (1) fixo
            AutenticacaoLegislacao.PadAlfa(64),  // 06: 15-78 (64)
            AutenticacaoBancaria.PadAlfa(25),    // 07: 79-103 (25)
            "".PadAlfa(127),                     // 08: 104-230 (127) CNAB Brancos
            CodigosOcorrencias.PadAlfa(10)       // 09: 231-240 (10)
        );
    // SOMA: 3+4+1+5+1+64+25+127+10 = 240 ✓
}
