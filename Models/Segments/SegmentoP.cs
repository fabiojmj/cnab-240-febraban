using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento P — Cobrança de Título Registrado (Detalhe Tipo 3, Segmento P).</summary>
public sealed record SegmentoP : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'P';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "P"</summary>
    // Fixed literal "P" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução (Remessa) — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Código da Agência — pos 18-22 (G008)</summary>
    public int AgenciaCodigo { get; init; }

    /// <summary>Dígito Verificador da Agência — pos 23-23 (G009)</summary>
    public string AgenciaDV { get; init; } = "";

    /// <summary>Número da Conta — pos 24-35 (G010)</summary>
    public long ContaNumero { get; init; }

    /// <summary>Dígito Verificador da Conta — pos 36-36 (G011)</summary>
    public string ContaDV { get; init; } = "";

    /// <summary>Dígito Verificador da Agência/Conta — pos 37-37 (G012)</summary>
    public string DVAgenciaConta { get; init; } = "";

    /// <summary>Identificação do Título no Banco — pos 38-57 (P001)</summary>
    public string IdentificacaoTituloBanco { get; init; } = "";

    /// <summary>Código da Carteira — pos 58-58 (P002)</summary>
    public int CodigoCarteira { get; init; }

    /// <summary>Forma de Cadastramento do Título no Banco — pos 59-59 (P003)</summary>
    public int FormaCadastramentoBanco { get; init; }

    /// <summary>Tipo de Documento — pos 60-60 (P004)</summary>
    public string TipoDocumento { get; init; } = "";

    /// <summary>Identificação da Emissão do Boleto — pos 61-61 (P005)</summary>
    public int IdentificacaoEmissaoBoleto { get; init; }

    /// <summary>Identificação da Distribuição — pos 62-62 (P006)</summary>
    public string IdentificacaoDistribuicao { get; init; } = "";

    /// <summary>Número do Documento de Cobrança — pos 63-77 (P007)</summary>
    public string NumeroDocumentoCobranca { get; init; } = "";

    /// <summary>Data de Vencimento do Título — pos 78-85 (G065)</summary>
    public DateOnly? DataVencimento { get; init; }

    /// <summary>Valor Nominal do Título — pos 86-100 (G066), 2 decimais</summary>
    public decimal ValorNominalTitulo { get; init; }

    /// <summary>Código da Agência Cobradora — pos 101-105 (P008)</summary>
    public int AgenciaCobranca { get; init; }

    /// <summary>Dígito Verificador da Agência Cobradora — pos 106-106 (P009)</summary>
    public string DVAgenciaCobradora { get; init; } = "";

    /// <summary>Espécie do Título — pos 107-108 (G074)</summary>
    public int EspecieTitulo { get; init; }

    /// <summary>Aceite — pos 109-109 (P010)</summary>
    public string Aceite { get; init; } = "";

    /// <summary>Data de Emissão do Título — pos 110-117 (G075)</summary>
    public DateOnly? DataEmissao { get; init; }

    /// <summary>Código do Juros de Mora — pos 118-118 (P011)</summary>
    public int CodigoJurosMora { get; init; }

    /// <summary>Data dos Juros de Mora — pos 119-126 (P012)</summary>
    public DateOnly? DataJurosMora { get; init; }

    /// <summary>Juros de Mora por Dia — pos 127-141 (G076), 2 decimais</summary>
    public decimal JurosMoraDia { get; init; }

    /// <summary>Código do Desconto 1 — pos 142-142 (G077)</summary>
    public int CodigoDesconto1 { get; init; }

    /// <summary>Data do Desconto 1 — pos 143-150 (G078)</summary>
    public DateOnly? DataDesconto1 { get; init; }

    /// <summary>Valor do Desconto 1 — pos 151-165 (G079), 2 decimais</summary>
    public decimal ValorDesconto1 { get; init; }

    /// <summary>Valor do IOF — pos 166-180 (P013), 2 decimais</summary>
    public decimal ValorIOF { get; init; }

    /// <summary>Valor do Abatimento — pos 181-195 (P014), 2 decimais</summary>
    public decimal ValorAbatimento { get; init; }

    /// <summary>Identificação do Título na Empresa — pos 196-220 (P015)</summary>
    public string IdentificacaoTituloEmpresa { get; init; } = "";

    /// <summary>Código do Protesto — pos 221-221 (G080)</summary>
    public int CodigoProtesto { get; init; }

    /// <summary>Número de Dias para Protesto — pos 222-223 (G081)</summary>
    public int NumeroDiasProtesto { get; init; }

    /// <summary>Código de Baixa/Devolução — pos 224-224 (P016)</summary>
    public int CodigoBaixaDevolucao { get; init; }

    /// <summary>Número de Dias para Baixa/Devolução — pos 225-227 (P017)</summary>
    public string NumeroDiasBaixa { get; init; } = "";

    /// <summary>Código da Moeda — pos 228-229 (G068)</summary>
    public int CodigoMoeda { get; init; }

    /// <summary>Número do Contrato do Crédito — pos 230-239 (P018)</summary>
    public long NumeroContratoCredito { get; init; }

    /// <summary>Uso Livre do Banco/Empresa — pos 240-240 (P019)</summary>
    public string UsoLivreBancoEmpresa { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento P.</summary>
    public static SegmentoP Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoP
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
            AgenciaCodigo = linha.ExtrairInt(18, 22),
            AgenciaDV = linha.ExtrairAlfa(23, 23),
            ContaNumero = linha.ExtrairLong(24, 35),
            ContaDV = linha.ExtrairAlfa(36, 36),
            DVAgenciaConta = linha.ExtrairAlfa(37, 37),
            IdentificacaoTituloBanco = linha.ExtrairAlfa(38, 57),
            CodigoCarteira = linha.ExtrairInt(58, 58),
            FormaCadastramentoBanco = linha.ExtrairInt(59, 59),
            TipoDocumento = linha.ExtrairAlfa(60, 60),
            IdentificacaoEmissaoBoleto = linha.ExtrairInt(61, 61),
            IdentificacaoDistribuicao = linha.ExtrairAlfa(62, 62),
            NumeroDocumentoCobranca = linha.ExtrairAlfa(63, 77),
            DataVencimento = linha.ExtrairData(78, 85),
            ValorNominalTitulo = linha.ExtrairDecimal(86, 100, 2),
            AgenciaCobranca = linha.ExtrairInt(101, 105),
            DVAgenciaCobradora = linha.ExtrairAlfa(106, 106),
            EspecieTitulo = linha.ExtrairInt(107, 108),
            Aceite = linha.ExtrairAlfa(109, 109),
            DataEmissao = linha.ExtrairData(110, 117),
            CodigoJurosMora = linha.ExtrairInt(118, 118),
            DataJurosMora = linha.ExtrairData(119, 126),
            JurosMoraDia = linha.ExtrairDecimal(127, 141, 2),
            CodigoDesconto1 = linha.ExtrairInt(142, 142),
            DataDesconto1 = linha.ExtrairData(143, 150),
            ValorDesconto1 = linha.ExtrairDecimal(151, 165, 2),
            ValorIOF = linha.ExtrairDecimal(166, 180, 2),
            ValorAbatimento = linha.ExtrairDecimal(181, 195, 2),
            IdentificacaoTituloEmpresa = linha.ExtrairAlfa(196, 220),
            CodigoProtesto = linha.ExtrairInt(221, 221),
            NumeroDiasProtesto = linha.ExtrairInt(222, 223),
            CodigoBaixaDevolucao = linha.ExtrairInt(224, 224),
            NumeroDiasBaixa = linha.ExtrairAlfa(225, 227),
            CodigoMoeda = linha.ExtrairInt(228, 229),
            NumeroContratoCredito = linha.ExtrairLong(230, 239),
            UsoLivreBancoEmpresa = linha.ExtrairAlfa(240, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                      // 01: 1-3   (3)
            LoteServico.PadNum(4),                      // 02: 4-7   (4)
            "3",                                         // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),                 // 04: 9-13  (5)
            "P",                                         // 05: 14-14 (1) fixo
            "".PadAlfa(1),                              // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2),           // 07: 16-17 (2)
            AgenciaCodigo.PadNum(5),                    // 08: 18-22 (5)
            AgenciaDV.PadAlfa(1),                       // 09: 23-23 (1)
            ContaNumero.PadNum(12),                     // 10: 24-35 (12)
            ContaDV.PadAlfa(1),                         // 11: 36-36 (1)
            DVAgenciaConta.PadAlfa(1),                  // 12: 37-37 (1)
            IdentificacaoTituloBanco.PadAlfa(20),       // 13: 38-57 (20)
            CodigoCarteira.PadNum(1),                   // 14: 58-58 (1)
            FormaCadastramentoBanco.PadNum(1),          // 15: 59-59 (1)
            TipoDocumento.PadAlfa(1),                   // 16: 60-60 (1)
            IdentificacaoEmissaoBoleto.PadNum(1),       // 17: 61-61 (1)
            IdentificacaoDistribuicao.PadAlfa(1),       // 18: 62-62 (1)
            NumeroDocumentoCobranca.PadAlfa(15),        // 19: 63-77 (15)
            DataVencimento.FormatarData(),              // 20: 78-85 (8)
            ValorNominalTitulo.PadDecimal(15, 2),      // 21: 86-100 (15)
            AgenciaCobranca.PadNum(5),                 // 22: 101-105 (5)
            DVAgenciaCobradora.PadAlfa(1),             // 23: 106-106 (1)
            EspecieTitulo.PadNum(2),                   // 24: 107-108 (2)
            Aceite.PadAlfa(1),                         // 25: 109-109 (1)
            DataEmissao.FormatarData(),                // 26: 110-117 (8)
            CodigoJurosMora.PadNum(1),                 // 27: 118-118 (1)
            DataJurosMora.FormatarData(),              // 28: 119-126 (8)
            JurosMoraDia.PadDecimal(15, 2),           // 29: 127-141 (15)
            CodigoDesconto1.PadNum(1),                // 30: 142-142 (1)
            DataDesconto1.FormatarData(),             // 31: 143-150 (8)
            ValorDesconto1.PadDecimal(15, 2),         // 32: 151-165 (15)
            ValorIOF.PadDecimal(15, 2),               // 33: 166-180 (15)
            ValorAbatimento.PadDecimal(15, 2),        // 34: 181-195 (15)
            IdentificacaoTituloEmpresa.PadAlfa(25),   // 35: 196-220 (25)
            CodigoProtesto.PadNum(1),                 // 36: 221-221 (1)
            NumeroDiasProtesto.PadNum(2),             // 37: 222-223 (2)
            CodigoBaixaDevolucao.PadNum(1),           // 38: 224-224 (1)
            NumeroDiasBaixa.PadAlfa(3),               // 39: 225-227 (3)
            CodigoMoeda.PadNum(2),                    // 40: 228-229 (2)
            NumeroContratoCredito.PadNum(10),         // 41: 230-239 (10)
            UsoLivreBancoEmpresa.PadAlfa(1)           // 42: 240-240 (1)
        );
    // SOMA: 3+4+1+5+1+1+2+5+1+12+1+1+20+1+1+1+1+1+15+8+15+5+1+2+1+8+1+8+15+1+8+15+15+15+25+1+2+1+3+2+10+1 = 240 ✓
}
