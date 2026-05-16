using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento E — Extrato de Conta Corrente (Detalhe Tipo 3, Segmento E).</summary>
public sealed record SegmentoE : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'E';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "E"</summary>
    // Fixed literal "E" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-17 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Tipo de Inscrição da Empresa — pos 18-18 (G005)</summary>
    public int TipoInscricaoEmpresa { get; init; }

    /// <summary>Número de Inscrição da Empresa — pos 19-32 (G006)</summary>
    public long NumeroInscricaoEmpresa { get; init; }

    /// <summary>Código do Convênio no Banco — pos 33-52 (E001)</summary>
    public string CodigoConvenioBanco { get; init; } = "";

    /// <summary>Código da Agência — pos 53-57 (G008)</summary>
    public int AgenciaCodigo { get; init; }

    /// <summary>Dígito Verificador da Agência — pos 58-58 (G009)</summary>
    public string AgenciaDV { get; init; } = "";

    /// <summary>Número da Conta — pos 59-70 (G010)</summary>
    public long ContaNumero { get; init; }

    /// <summary>Dígito Verificador da Conta — pos 71-71 (G011)</summary>
    public string ContaDV { get; init; } = "";

    /// <summary>Dígito Verificador Agência/Conta — pos 72-72 (G012)</summary>
    public string DVAgenciaConta { get; init; } = "";

    /// <summary>Nome da Empresa — pos 73-102 (G013)</summary>
    public string NomeEmpresa { get; init; } = "";

    /// <summary>Uso Reservado CNAB 2 — pos 103-108 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Natureza do Lançamento — pos 109-111 (E002)</summary>
    public string NaturezaLancamento { get; init; } = "";

    /// <summary>Tipo do Complemento do Lançamento — pos 112-113 (E003)</summary>
    public int TipoComplementoLancamento { get; init; }

    /// <summary>Complemento do Lançamento — pos 114-133 (E004)</summary>
    public string ComplementoLancamento { get; init; } = "";

    /// <summary>Identificação da Isenção de CPMF — pos 134-134 (E005)</summary>
    public string IdentificacaoIsencaoCPMF { get; init; } = "";

    /// <summary>Data Contábil — pos 135-142 (E006)</summary>
    public DateOnly? DataContabil { get; init; }

    /// <summary>Data do Lançamento — pos 143-150 (E007)</summary>
    public DateOnly? DataLancamento { get; init; }

    /// <summary>Valor do Lançamento — pos 151-168 (E008), 2 decimais</summary>
    public decimal ValorLancamento { get; init; }

    /// <summary>Tipo do Lançamento — pos 169-169 (E009). D=Débito, C=Crédito</summary>
    public string TipoLancamento { get; init; } = "";

    /// <summary>Categoria do Lançamento — pos 170-172 (E010)</summary>
    public int CategoriaLancamento { get; init; }

    /// <summary>Código do Histórico do Banco — pos 173-176 (E011)</summary>
    public string CodigoHistoricoBanco { get; init; } = "";

    /// <summary>Descrição do Histórico — pos 177-201 (E012)</summary>
    public string DescricaoHistorico { get; init; } = "";

    /// <summary>Número do Documento — pos 202-240 (E013)</summary>
    public string NumeroDocumento { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento E.</summary>
    public static SegmentoE Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoE
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 17),
            TipoInscricaoEmpresa = linha.ExtrairInt(18, 18),
            NumeroInscricaoEmpresa = linha.ExtrairLong(19, 32),
            CodigoConvenioBanco = linha.ExtrairAlfa(33, 52),
            AgenciaCodigo = linha.ExtrairInt(53, 57),
            AgenciaDV = linha.ExtrairAlfa(58, 58),
            ContaNumero = linha.ExtrairLong(59, 70),
            ContaDV = linha.ExtrairAlfa(71, 71),
            DVAgenciaConta = linha.ExtrairAlfa(72, 72),
            NomeEmpresa = linha.ExtrairAlfa(73, 102),
            CNAB2 = linha.ExtrairAlfa(103, 108),
            NaturezaLancamento = linha.ExtrairAlfa(109, 111),
            TipoComplementoLancamento = linha.ExtrairInt(112, 113),
            ComplementoLancamento = linha.ExtrairAlfa(114, 133),
            IdentificacaoIsencaoCPMF = linha.ExtrairAlfa(134, 134),
            DataContabil = linha.ExtrairData(135, 142),
            DataLancamento = linha.ExtrairData(143, 150),
            ValorLancamento = linha.ExtrairDecimal(151, 168, 2),
            TipoLancamento = linha.ExtrairAlfa(169, 169),
            CategoriaLancamento = linha.ExtrairInt(170, 172),
            CodigoHistoricoBanco = linha.ExtrairAlfa(173, 176),
            DescricaoHistorico = linha.ExtrairAlfa(177, 201),
            NumeroDocumento = linha.ExtrairAlfa(202, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                 // 01: 1-3   (3)
            LoteServico.PadNum(4),                 // 02: 4-7   (4)
            "3",                                    // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),            // 04: 9-13  (5)
            "E",                                    // 05: 14-14 (1) fixo
            "".PadAlfa(3),                         // 06: 15-17 (3) CNAB Brancos
            TipoInscricaoEmpresa.PadNum(1),       // 07: 18-18 (1)
            NumeroInscricaoEmpresa.PadNum(14),     // 08: 19-32 (14)
            CodigoConvenioBanco.PadAlfa(20),      // 09: 33-52 (20)
            AgenciaCodigo.PadNum(5),              // 10: 53-57 (5)
            AgenciaDV.PadAlfa(1),                 // 11: 58-58 (1)
            ContaNumero.PadNum(12),               // 12: 59-70 (12)
            ContaDV.PadAlfa(1),                   // 13: 71-71 (1)
            DVAgenciaConta.PadAlfa(1),            // 14: 72-72 (1)
            NomeEmpresa.PadAlfa(30),              // 15: 73-102 (30)
            "".PadAlfa(6),                        // 16: 103-108 (6) CNAB2 Brancos
            NaturezaLancamento.PadAlfa(3),        // 17: 109-111 (3)
            TipoComplementoLancamento.PadNum(2),  // 18: 112-113 (2)
            ComplementoLancamento.PadAlfa(20),    // 19: 114-133 (20)
            IdentificacaoIsencaoCPMF.PadAlfa(1),  // 20: 134-134 (1)
            DataContabil.FormatarData(),           // 21: 135-142 (8)
            DataLancamento.FormatarData(),         // 22: 143-150 (8)
            ValorLancamento.PadDecimal(18, 2),    // 23: 151-168 (18)
            TipoLancamento.PadAlfa(1),             // 24: 169-169 (1)
            CategoriaLancamento.PadNum(3),         // 25: 170-172 (3)
            CodigoHistoricoBanco.PadAlfa(4),      // 26: 173-176 (4)
            DescricaoHistorico.PadAlfa(25),       // 27: 177-201 (25)
            NumeroDocumento.PadAlfa(39)           // 28: 202-240 (39)
        );
    // SOMA: 3+4+1+5+1+3+1+14+20+5+1+12+1+1+30+6+3+2+20+1+8+8+18+1+3+4+25+39 = 240 ✓
}
