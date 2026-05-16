using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento F — Extrato Complementar (Detalhe Tipo 3, Segmento F).</summary>
public sealed record SegmentoF : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'F';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "F"</summary>
    // Fixed literal "F" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-102 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Horário da Transação — pos 103-108 (F001)</summary>
    public TimeOnly? HorarioTransacao { get; init; }

    /// <summary>Natureza do Lançamento — pos 109-111 (F002)</summary>
    public string NaturezaLancamento { get; init; } = "";

    /// <summary>Tipo do Complemento do Lançamento — pos 112-113 (F003)</summary>
    public int TipoComplementoLancamento { get; init; }

    /// <summary>Complemento do Lançamento — pos 114-133 (F004)</summary>
    public string ComplementoLancamento { get; init; } = "";

    /// <summary>Identificação da Isenção de CPMF — pos 134-134 (F005)</summary>
    public string IdentificacaoIsencaoCPMF { get; init; } = "";

    /// <summary>Data Contábil — pos 135-142 (F006)</summary>
    public DateOnly? DataContabil { get; init; }

    /// <summary>Data do Lançamento — pos 143-150 (F007)</summary>
    public DateOnly? DataLancamento { get; init; }

    /// <summary>Valor do Lançamento — pos 151-168 (F008), 2 decimais</summary>
    public decimal ValorLancamento { get; init; }

    /// <summary>Tipo do Lançamento — pos 169-169 (F009). D=Débito, C=Crédito</summary>
    public string TipoLancamento { get; init; } = "";

    /// <summary>Categoria do Lançamento — pos 170-172 (F010)</summary>
    public int CategoriaLancamento { get; init; }

    /// <summary>Código do Histórico do Banco — pos 173-177 (F011)</summary>
    public string CodigoHistoricoBanco { get; init; } = "";

    /// <summary>Descrição do Histórico — pos 178-202 (F012)</summary>
    public string DescricaoHistorico { get; init; } = "";

    /// <summary>Número do Documento — pos 203-240 (F013)</summary>
    public string NumeroDocumento { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento F.</summary>
    public static SegmentoF Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoF
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 102),
            HorarioTransacao = linha.ExtrairHora(103, 108),
            NaturezaLancamento = linha.ExtrairAlfa(109, 111),
            TipoComplementoLancamento = linha.ExtrairInt(112, 113),
            ComplementoLancamento = linha.ExtrairAlfa(114, 133),
            IdentificacaoIsencaoCPMF = linha.ExtrairAlfa(134, 134),
            DataContabil = linha.ExtrairData(135, 142),
            DataLancamento = linha.ExtrairData(143, 150),
            ValorLancamento = linha.ExtrairDecimal(151, 168, 2),
            TipoLancamento = linha.ExtrairAlfa(169, 169),
            CategoriaLancamento = linha.ExtrairInt(170, 172),
            CodigoHistoricoBanco = linha.ExtrairAlfa(173, 177),
            DescricaoHistorico = linha.ExtrairAlfa(178, 202),
            NumeroDocumento = linha.ExtrairAlfa(203, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                 // 01: 1-3    (3)
            LoteServico.PadNum(4),                 // 02: 4-7    (4)
            "3",                                    // 03: 8-8    (1) fixo
            NumeroSequencial.PadNum(5),            // 04: 9-13   (5)
            "F",                                    // 05: 14-14  (1) fixo
            "".PadAlfa(88),                        // 06: 15-102 (88) CNAB Brancos
            HorarioTransacao.FormatarHora(),       // 07: 103-108 (6)
            NaturezaLancamento.PadAlfa(3),         // 08: 109-111 (3)
            TipoComplementoLancamento.PadNum(2),   // 09: 112-113 (2)
            ComplementoLancamento.PadAlfa(20),     // 10: 114-133 (20)
            IdentificacaoIsencaoCPMF.PadAlfa(1),   // 11: 134-134 (1)
            DataContabil.FormatarData(),            // 12: 135-142 (8)
            DataLancamento.FormatarData(),          // 13: 143-150 (8)
            ValorLancamento.PadDecimal(18, 2),     // 14: 151-168 (18)
            TipoLancamento.PadAlfa(1),              // 15: 169-169 (1)
            CategoriaLancamento.PadNum(3),          // 16: 170-172 (3)
            CodigoHistoricoBanco.PadAlfa(5),        // 17: 173-177 (5)
            DescricaoHistorico.PadAlfa(25),         // 18: 178-202 (25)
            NumeroDocumento.PadAlfa(38)             // 19: 203-240 (38)
        );
    // SOMA: 3+4+1+5+1+88+6+3+2+20+1+8+8+18+1+3+5+25+38 = 240 ✓
}
