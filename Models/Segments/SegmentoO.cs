using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento O — Pagamento de Concessionárias (Detalhe Tipo 3, Segmento O).</summary>
public sealed record SegmentoO : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'O';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "O"</summary>
    // Fixed literal "O" in ToLinhaFormatada

    /// <summary>Tipo de Movimento — pos 15-15 (O001)</summary>
    public int TipoMovimento { get; init; }

    /// <summary>Código de Instrução para Movimento — pos 16-17 (O002)</summary>
    public int CodigoInstrucaoMovimento { get; init; }

    /// <summary>Código de Barras — pos 18-61 (O003)</summary>
    public string CodigoBarras { get; init; } = "";

    /// <summary>Nome da Concessionária — pos 62-91 (O004)</summary>
    public string NomeConcessionaria { get; init; } = "";

    /// <summary>Data de Vencimento — pos 92-99 (O005)</summary>
    public DateOnly? DataVencimento { get; init; }

    /// <summary>Data do Pagamento — pos 100-107 (O006)</summary>
    public DateOnly? DataPagamento { get; init; }

    /// <summary>Valor do Pagamento — pos 108-122 (O007), 2 decimais</summary>
    public decimal ValorPagamento { get; init; }

    /// <summary>Número do Documento Atribuído pela Empresa — pos 123-142 (O008)</summary>
    public string NumeroDoctoEmpresa { get; init; } = "";

    /// <summary>Número do Documento Atribuído pelo Banco — pos 143-162 (O009)</summary>
    public string NumeroDoctoAtribBanco { get; init; } = "";

    /// <summary>Uso Reservado CNAB — pos 163-230 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Códigos de Ocorrências — pos 231-240 (G061)</summary>
    public string CodigosOcorrencias { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento O.</summary>
    public static SegmentoO Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoO
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            TipoMovimento = linha.ExtrairInt(15, 15),
            CodigoInstrucaoMovimento = linha.ExtrairInt(16, 17),
            CodigoBarras = linha.ExtrairAlfa(18, 61),
            NomeConcessionaria = linha.ExtrairAlfa(62, 91),
            DataVencimento = linha.ExtrairData(92, 99),
            DataPagamento = linha.ExtrairData(100, 107),
            ValorPagamento = linha.ExtrairDecimal(108, 122, 2),
            NumeroDoctoEmpresa = linha.ExtrairAlfa(123, 142),
            NumeroDoctoAtribBanco = linha.ExtrairAlfa(143, 162),
            CNAB = linha.ExtrairAlfa(163, 230),
            CodigosOcorrencias = linha.ExtrairAlfa(231, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                  // 01: 1-3   (3)
            LoteServico.PadNum(4),                  // 02: 4-7   (4)
            "3",                                     // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),             // 04: 9-13  (5)
            "O",                                     // 05: 14-14 (1) fixo
            TipoMovimento.PadNum(1),                // 06: 15-15 (1)
            CodigoInstrucaoMovimento.PadNum(2),     // 07: 16-17 (2)
            CodigoBarras.PadAlfa(44),               // 08: 18-61 (44)
            NomeConcessionaria.PadAlfa(30),         // 09: 62-91 (30)
            DataVencimento.FormatarData(),          // 10: 92-99 (8)
            DataPagamento.FormatarData(),           // 11: 100-107 (8)
            ValorPagamento.PadDecimal(15, 2),      // 12: 108-122 (15)
            NumeroDoctoEmpresa.PadAlfa(20),        // 13: 123-142 (20)
            NumeroDoctoAtribBanco.PadAlfa(20),     // 14: 143-162 (20)
            "".PadAlfa(68),                        // 15: 163-230 (68) CNAB Brancos
            CodigosOcorrencias.PadAlfa(10)         // 16: 231-240 (10)
        );
    // SOMA: 3+4+1+5+1+1+2+44+30+8+8+15+20+20+68+10 = 240 ✓
}
