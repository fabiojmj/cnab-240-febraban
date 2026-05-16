using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento B — Informações do Sacador/Avalista (Detalhe Tipo 3, Segmento B).</summary>
public sealed record SegmentoB : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'B';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "B"</summary>
    // Fixed literal "B" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Tipo de Inscrição — pos 18-18 (G005)</summary>
    public int TipoInscricao { get; init; }

    /// <summary>Número de Inscrição — pos 19-33 (G006)</summary>
    public long NumeroInscricao { get; init; }

    /// <summary>Logradouro — pos 34-73 (B001)</summary>
    public string Logradouro { get; init; } = "";

    /// <summary>Número do local — pos 74-78 (B002)</summary>
    public int Numero { get; init; }

    /// <summary>Complemento — pos 79-93 (B003)</summary>
    public string Complemento { get; init; } = "";

    /// <summary>Bairro — pos 94-108 (B004)</summary>
    public string Bairro { get; init; } = "";

    /// <summary>Cidade — pos 109-123 (B005)</summary>
    public string Cidade { get; init; } = "";

    /// <summary>CEP — pos 124-128 (B006)</summary>
    public int CEP { get; init; }

    /// <summary>Sufixo do CEP — pos 129-131 (B007)</summary>
    public int SufixoCEP { get; init; }

    /// <summary>UF — pos 132-133 (B008)</summary>
    public string UF { get; init; } = "";

    /// <summary>E-mail do Pagador — pos 134-173 (B009)</summary>
    public string EmailPagador { get; init; } = "";

    /// <summary>Uso Reservado CNAB 2 — pos 174-240 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento B.</summary>
    public static SegmentoB Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoB
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
            TipoInscricao = linha.ExtrairInt(18, 18),
            NumeroInscricao = linha.ExtrairLong(19, 33),
            Logradouro = linha.ExtrairAlfa(34, 73),
            Numero = linha.ExtrairInt(74, 78),
            Complemento = linha.ExtrairAlfa(79, 93),
            Bairro = linha.ExtrairAlfa(94, 108),
            Cidade = linha.ExtrairAlfa(109, 123),
            CEP = linha.ExtrairInt(124, 128),
            SufixoCEP = linha.ExtrairInt(129, 131),
            UF = linha.ExtrairAlfa(132, 133),
            EmailPagador = linha.ExtrairAlfa(134, 173),
            CNAB2 = linha.ExtrairAlfa(174, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),            // 01: 1-3   (3)
            LoteServico.PadNum(4),            // 02: 4-7   (4)
            "3",                               // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),       // 04: 9-13  (5)
            "B",                               // 05: 14-14 (1) fixo
            "".PadAlfa(1),                    // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2), // 07: 16-17 (2)
            TipoInscricao.PadNum(1),          // 08: 18-18 (1)
            NumeroInscricao.PadNum(15),       // 09: 19-33 (15)
            Logradouro.PadAlfa(40),           // 10: 34-73 (40)
            Numero.PadNum(5),                 // 11: 74-78 (5)
            Complemento.PadAlfa(15),          // 12: 79-93 (15)
            Bairro.PadAlfa(15),               // 13: 94-108 (15)
            Cidade.PadAlfa(15),               // 14: 109-123 (15)
            CEP.PadNum(5),                    // 15: 124-128 (5)
            SufixoCEP.PadNum(3),              // 16: 129-131 (3)
            UF.PadAlfa(2),                    // 17: 132-133 (2)
            EmailPagador.PadAlfa(40),         // 18: 134-173 (40)
            "".PadAlfa(67)                    // 19: 174-240 (67) CNAB2 Brancos
        );
    // SOMA: 3+4+1+5+1+1+2+1+15+40+5+15+15+15+5+3+2+40+67 = 240 ✓
}

/// <summary>Segmento C — Informações Complementares (Detalhe Tipo 3, Segmento C).</summary>
public sealed record SegmentoC : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'C';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "C"</summary>
    // Fixed literal "C" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Tipo de Inscrição do Favorecido — pos 18-18 (G005)</summary>
    public int TipoInscricaoFavorecido { get; init; }

    /// <summary>Número de Inscrição do Favorecido — pos 19-33 (G006)</summary>
    public long NumeroInscricaoFavorecido { get; init; }

    /// <summary>Nome do Favorecido — pos 34-73 (C001)</summary>
    public string NomeFavorecido { get; init; } = "";

    /// <summary>Valor do Documento — pos 74-88 (C002), 2 decimais</summary>
    public decimal ValorDocumento { get; init; }

    /// <summary>Valor do Abatimento — pos 89-103 (C003), 2 decimais</summary>
    public decimal ValorAbatimento { get; init; }

    /// <summary>Valor do Desconto — pos 104-118 (C004), 2 decimais</summary>
    public decimal ValorDesconto { get; init; }

    /// <summary>Valor da Mora — pos 119-133 (C005), 2 decimais</summary>
    public decimal ValorMora { get; init; }

    /// <summary>Valor da Multa — pos 134-148 (C006), 2 decimais</summary>
    public decimal ValorMulta { get; init; }

    /// <summary>Código do Histórico — pos 149-158 (C007)</summary>
    public string CodigoHistorico { get; init; } = "";

    /// <summary>Complemento do Histórico — pos 159-198 (C008)</summary>
    public string HistoricoComplemento { get; init; } = "";

    /// <summary>Uso Reservado CNAB 2 — pos 199-240 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento C.</summary>
    public static SegmentoC Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoC
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
            TipoInscricaoFavorecido = linha.ExtrairInt(18, 18),
            NumeroInscricaoFavorecido = linha.ExtrairLong(19, 33),
            NomeFavorecido = linha.ExtrairAlfa(34, 73),
            ValorDocumento = linha.ExtrairDecimal(74, 88, 2),
            ValorAbatimento = linha.ExtrairDecimal(89, 103, 2),
            ValorDesconto = linha.ExtrairDecimal(104, 118, 2),
            ValorMora = linha.ExtrairDecimal(119, 133, 2),
            ValorMulta = linha.ExtrairDecimal(134, 148, 2),
            CodigoHistorico = linha.ExtrairAlfa(149, 158),
            HistoricoComplemento = linha.ExtrairAlfa(159, 198),
            CNAB2 = linha.ExtrairAlfa(199, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                    // 01: 1-3   (3)
            LoteServico.PadNum(4),                    // 02: 4-7   (4)
            "3",                                       // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),               // 04: 9-13  (5)
            "C",                                       // 05: 14-14 (1) fixo
            "".PadAlfa(1),                            // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2),         // 07: 16-17 (2)
            TipoInscricaoFavorecido.PadNum(1),        // 08: 18-18 (1)
            NumeroInscricaoFavorecido.PadNum(15),     // 09: 19-33 (15)
            NomeFavorecido.PadAlfa(40),               // 10: 34-73 (40)
            ValorDocumento.PadDecimal(15, 2),         // 11: 74-88 (15)
            ValorAbatimento.PadDecimal(15, 2),        // 12: 89-103 (15)
            ValorDesconto.PadDecimal(15, 2),          // 13: 104-118 (15)
            ValorMora.PadDecimal(15, 2),              // 14: 119-133 (15)
            ValorMulta.PadDecimal(15, 2),             // 15: 134-148 (15)
            CodigoHistorico.PadAlfa(10),              // 16: 149-158 (10)
            HistoricoComplemento.PadAlfa(40),         // 17: 159-198 (40)
            "".PadAlfa(42)                            // 18: 199-240 (42) CNAB2 Brancos
        );
    // SOMA: 3+4+1+5+1+1+2+1+15+40+15+15+15+15+15+10+40+42 = 240 ✓
}

/// <summary>Segmento J — Pagamento de Boleto (Detalhe Tipo 3, Segmento J).</summary>
public sealed record SegmentoJ : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'J';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "J"</summary>
    // Fixed literal "J" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Código de Barras do Título — pos 18-61 (J001)</summary>
    public string CodigoBarras { get; init; } = "";

    /// <summary>Nome do Beneficiário — pos 62-91 (J002)</summary>
    public string NomeBeneficiario { get; init; } = "";

    /// <summary>Data do Vencimento do Título — pos 92-99 (J004)</summary>
    public DateOnly? DataVencimento { get; init; }

    /// <summary>Valor Nominal do Título — pos 100-114 (J005), 2 decimais</summary>
    public decimal ValorNominal { get; init; }

    /// <summary>Valor do Desconto/Abatimento — pos 115-129 (J006), 2 decimais</summary>
    public decimal ValorDesconto { get; init; }

    /// <summary>Valor do Acréscimo — pos 130-144 (J007), 2 decimais</summary>
    public decimal ValorAcrescimo { get; init; }

    /// <summary>Data do Pagamento — pos 145-152 (J008)</summary>
    public DateOnly? DataPagamento { get; init; }

    /// <summary>Valor do Pagamento — pos 153-167 (J009), 2 decimais</summary>
    public decimal ValorPagamento { get; init; }

    /// <summary>Número do Documento Atribuído pela Empresa — pos 168-187 (J010)</summary>
    public string NumeroDoctoEmpresa { get; init; } = "";

    /// <summary>Número do Documento Atribuído pelo Banco — pos 188-207 (J011)</summary>
    public string NumeroDoctoAtribBanco { get; init; } = "";

    /// <summary>Código da Moeda — pos 208-209 (J012)</summary>
    public int CodigoMoeda { get; init; }

    /// <summary>Valor Real do Pagamento — pos 210-224 (J013), 2 decimais</summary>
    public decimal ValorRealPagamento { get; init; }

    /// <summary>Uso Reservado CNAB 2 — pos 225-230 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Ocorrências de Retorno — pos 231-240 (G061)</summary>
    public string OcorrenciasRetorno { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento J.</summary>
    public static SegmentoJ Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoJ
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
            CodigoBarras = linha.ExtrairAlfa(18, 61),
            NomeBeneficiario = linha.ExtrairAlfa(62, 91),
            DataVencimento = linha.ExtrairData(92, 99),
            ValorNominal = linha.ExtrairDecimal(100, 114, 2),
            ValorDesconto = linha.ExtrairDecimal(115, 129, 2),
            ValorAcrescimo = linha.ExtrairDecimal(130, 144, 2),
            DataPagamento = linha.ExtrairData(145, 152),
            ValorPagamento = linha.ExtrairDecimal(153, 167, 2),
            NumeroDoctoEmpresa = linha.ExtrairAlfa(168, 187),
            NumeroDoctoAtribBanco = linha.ExtrairAlfa(188, 207),
            CodigoMoeda = linha.ExtrairInt(208, 209),
            ValorRealPagamento = linha.ExtrairDecimal(210, 224, 2),
            CNAB2 = linha.ExtrairAlfa(225, 230),
            OcorrenciasRetorno = linha.ExtrairAlfa(231, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),               // 01: 1-3   (3)
            LoteServico.PadNum(4),               // 02: 4-7   (4)
            "3",                                  // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),          // 04: 9-13  (5)
            "J",                                  // 05: 14-14 (1) fixo
            "".PadAlfa(1),                       // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2),    // 07: 16-17 (2)
            CodigoBarras.PadAlfa(44),            // 08: 18-61 (44)
            NomeBeneficiario.PadAlfa(30),        // 09: 62-91 (30)
            DataVencimento.FormatarData(),       // 10: 92-99 (8)
            ValorNominal.PadDecimal(15, 2),      // 11: 100-114 (15)
            ValorDesconto.PadDecimal(15, 2),     // 12: 115-129 (15)
            ValorAcrescimo.PadDecimal(15, 2),    // 13: 130-144 (15)
            DataPagamento.FormatarData(),        // 14: 145-152 (8)
            ValorPagamento.PadDecimal(15, 2),    // 15: 153-167 (15)
            NumeroDoctoEmpresa.PadAlfa(20),      // 16: 168-187 (20)
            NumeroDoctoAtribBanco.PadAlfa(20),   // 17: 188-207 (20)
            CodigoMoeda.PadNum(2),               // 18: 208-209 (2)
            ValorRealPagamento.PadDecimal(15, 2),// 19: 210-224 (15)
            "".PadAlfa(6),                       // 20: 225-230 (6) CNAB2 Brancos
            OcorrenciasRetorno.PadAlfa(10)       // 21: 231-240 (10)
        );
    // SOMA: 3+4+1+5+1+1+2+44+30+8+15+15+15+8+15+20+20+2+15+6+10 = 240 ✓
}
