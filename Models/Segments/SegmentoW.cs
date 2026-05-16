using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento W — Informações Complementares de Tributos (Detalhe Tipo 3, Segmento W).</summary>
public sealed record SegmentoW : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'W';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "W"</summary>
    // Fixed literal "W" in ToLinhaFormatada

    /// <summary>Número de Sequência do Registro Complementar — pos 15-15 (W001)</summary>
    public int NumeroSeqRegistroComplementar { get; init; }

    /// <summary>Identificação do Uso das Informações — pos 16-16 (W002)</summary>
    public string IdentificaUsoInformacoes { get; init; } = "";

    /// <summary>Informação Complementar 1 — pos 17-96 (W003)</summary>
    public string InformacaoComplementar1 { get; init; } = "";

    /// <summary>Informação Complementar 2 — pos 97-176 (W004)</summary>
    public string InformacaoComplementar2 { get; init; } = "";

    /// <summary>Identificador do Tributo — pos 177-178 (W005)</summary>
    public string IdentificadorTributo { get; init; } = "";

    /// <summary>Informação Complementar do Tributo — pos 179-228 (W006)</summary>
    public string InformacaoComplementarTributo { get; init; } = "";

    /// <summary>Uso Reservado CNAB — pos 229-230 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Códigos de Ocorrências — pos 231-240 (G061)</summary>
    public string CodigosOcorrencias { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento W.</summary>
    public static SegmentoW Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoW
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            NumeroSeqRegistroComplementar = linha.ExtrairInt(15, 15),
            IdentificaUsoInformacoes = linha.ExtrairAlfa(16, 16),
            InformacaoComplementar1 = linha.ExtrairAlfa(17, 96),
            InformacaoComplementar2 = linha.ExtrairAlfa(97, 176),
            IdentificadorTributo = linha.ExtrairAlfa(177, 178),
            InformacaoComplementarTributo = linha.ExtrairAlfa(179, 228),
            CNAB = linha.ExtrairAlfa(229, 230),
            CodigosOcorrencias = linha.ExtrairAlfa(231, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                         // 01: 1-3   (3)
            LoteServico.PadNum(4),                         // 02: 4-7   (4)
            "3",                                            // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),                    // 04: 9-13  (5)
            "W",                                            // 05: 14-14 (1) fixo
            NumeroSeqRegistroComplementar.PadNum(1),       // 06: 15-15 (1)
            IdentificaUsoInformacoes.PadAlfa(1),           // 07: 16-16 (1)
            InformacaoComplementar1.PadAlfa(80),           // 08: 17-96 (80)
            InformacaoComplementar2.PadAlfa(80),           // 09: 97-176 (80)
            IdentificadorTributo.PadAlfa(2),               // 10: 177-178 (2)
            InformacaoComplementarTributo.PadAlfa(50),     // 11: 179-228 (50)
            "".PadAlfa(2),                                 // 12: 229-230 (2) CNAB Brancos
            CodigosOcorrencias.PadAlfa(10)                // 13: 231-240 (10)
        );
    // SOMA: 3+4+1+5+1+1+1+80+80+2+50+2+10 = 240 ✓
}
