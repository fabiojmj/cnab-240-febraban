using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento H — Cobrança Complementar (Detalhe Tipo 3, Segmento H).</summary>
public sealed record SegmentoH : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'H';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "H"</summary>
    // Fixed literal "H" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Tipo de Inscrição — pos 18-18 (G005)</summary>
    public int TipoInscricao { get; init; }

    /// <summary>Número de Inscrição — pos 19-33 (G006)</summary>
    public long NumeroInscricao { get; init; }

    /// <summary>Nome do Sacador/Avalista — pos 34-73 (H001)</summary>
    public string NomeSacadorAvalista { get; init; } = "";

    /// <summary>Código do Desconto 2 — pos 74-74 (H002)</summary>
    public int CodigoDesconto2 { get; init; }

    /// <summary>Data do Desconto 2 — pos 75-82 (H003)</summary>
    public DateOnly? DataDesconto2 { get; init; }

    /// <summary>Valor do Desconto 2 — pos 83-97 (H004), 2 decimais</summary>
    public decimal ValorDesconto2 { get; init; }

    /// <summary>Código do Desconto 3 — pos 98-98 (H005)</summary>
    public int CodigoDesconto3 { get; init; }

    /// <summary>Data do Desconto 3 — pos 99-106 (H006)</summary>
    public DateOnly? DataDesconto3 { get; init; }

    /// <summary>Valor do Desconto 3 — pos 107-121 (H007), 2 decimais</summary>
    public decimal ValorDesconto3 { get; init; }

    /// <summary>Código da Multa — pos 122-122 (H008)</summary>
    public int CodigoMulta { get; init; }

    /// <summary>Data da Multa — pos 123-130 (H009)</summary>
    public DateOnly? DataMulta { get; init; }

    /// <summary>Valor da Multa — pos 131-145 (H010), 2 decimais</summary>
    public decimal ValorMulta { get; init; }

    /// <summary>Valor do Abatimento — pos 146-160 (H011), 2 decimais</summary>
    public decimal ValorAbatimento { get; init; }

    /// <summary>Mensagem 1 — pos 161-200 (H012)</summary>
    public string Mensagem1 { get; init; } = "";

    /// <summary>Mensagem 2 — pos 201-240 (H013)</summary>
    public string Mensagem2 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento H.</summary>
    public static SegmentoH Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoH
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
            TipoInscricao = linha.ExtrairInt(18, 18),
            NumeroInscricao = linha.ExtrairLong(19, 33),
            NomeSacadorAvalista = linha.ExtrairAlfa(34, 73),
            CodigoDesconto2 = linha.ExtrairInt(74, 74),
            DataDesconto2 = linha.ExtrairData(75, 82),
            ValorDesconto2 = linha.ExtrairDecimal(83, 97, 2),
            CodigoDesconto3 = linha.ExtrairInt(98, 98),
            DataDesconto3 = linha.ExtrairData(99, 106),
            ValorDesconto3 = linha.ExtrairDecimal(107, 121, 2),
            CodigoMulta = linha.ExtrairInt(122, 122),
            DataMulta = linha.ExtrairData(123, 130),
            ValorMulta = linha.ExtrairDecimal(131, 145, 2),
            ValorAbatimento = linha.ExtrairDecimal(146, 160, 2),
            Mensagem1 = linha.ExtrairAlfa(161, 200),
            Mensagem2 = linha.ExtrairAlfa(201, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),              // 01: 1-3   (3)
            LoteServico.PadNum(4),              // 02: 4-7   (4)
            "3",                                 // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),         // 04: 9-13  (5)
            "H",                                 // 05: 14-14 (1) fixo
            "".PadAlfa(1),                      // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2),   // 07: 16-17 (2)
            TipoInscricao.PadNum(1),            // 08: 18-18 (1)
            NumeroInscricao.PadNum(15),         // 09: 19-33 (15)
            NomeSacadorAvalista.PadAlfa(40),    // 10: 34-73 (40)
            CodigoDesconto2.PadNum(1),          // 11: 74-74 (1)
            DataDesconto2.FormatarData(),       // 12: 75-82 (8)
            ValorDesconto2.PadDecimal(15, 2),   // 13: 83-97 (15)
            CodigoDesconto3.PadNum(1),          // 14: 98-98 (1)
            DataDesconto3.FormatarData(),       // 15: 99-106 (8)
            ValorDesconto3.PadDecimal(15, 2),   // 16: 107-121 (15)
            CodigoMulta.PadNum(1),              // 17: 122-122 (1)
            DataMulta.FormatarData(),           // 18: 123-130 (8)
            ValorMulta.PadDecimal(15, 2),       // 19: 131-145 (15)
            ValorAbatimento.PadDecimal(15, 2),  // 20: 146-160 (15)
            Mensagem1.PadAlfa(40),              // 21: 161-200 (40)
            Mensagem2.PadAlfa(40)               // 22: 201-240 (40)
        );
    // SOMA: 3+4+1+5+1+1+2+1+15+40+1+8+15+1+8+15+1+8+15+15+40+40 = 240 ✓
}
