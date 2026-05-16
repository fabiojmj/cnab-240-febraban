using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento G — Cobrança de Títulos (Detalhe Tipo 3, Segmento G).</summary>
public sealed record SegmentoG : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'G';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "G"</summary>
    // Fixed literal "G" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Código de Barras do Título (44 dígitos, pode ter zeros à esquerda) — pos 18-61 (G002)</summary>
    public string CodigoBarras { get; init; } = "";

    /// <summary>Tipo de Inscrição do Beneficiário — pos 62-62 (G005)</summary>
    public int TipoInscricaoBeneficiario { get; init; }

    /// <summary>Número de Inscrição do Beneficiário — pos 63-77 (G006)</summary>
    public long NumeroInscricaoBeneficiario { get; init; }

    /// <summary>Nome do Beneficiário — pos 78-107 (G013)</summary>
    public string NomeBeneficiario { get; init; } = "";

    /// <summary>Data de Vencimento do Título — pos 108-115 (G065)</summary>
    public DateOnly? DataVencimento { get; init; }

    /// <summary>Valor Nominal do Título — pos 116-130 (G066), 2 decimais</summary>
    public decimal ValorNominalTitulo { get; init; }

    /// <summary>Quantidade de Moeda — pos 131-145 (G067), 5 decimais</summary>
    public decimal QuantidadeMoeda { get; init; }

    /// <summary>Código da Moeda — pos 146-147 (G068)</summary>
    public int CodigoMoeda { get; init; }

    /// <summary>Número do Documento de Cobrança — pos 148-162 (G069)</summary>
    public string NumeroDocumentoCobranca { get; init; } = "";

    /// <summary>Código da Agência de Cobrança — pos 163-167 (G070)</summary>
    public int AgenciaCobranca { get; init; }

    /// <summary>Dígito Verificador da Agência — pos 168-168 (G071)</summary>
    public string DVAgencia { get; init; } = "";

    /// <summary>Praça Cobradora — pos 169-178 (G072)</summary>
    public string PracaCobradora { get; init; } = "";

    /// <summary>Código da Carteira — pos 179-179 (G073)</summary>
    public string CodigoCarteira { get; init; } = "";

    /// <summary>Espécie do Título — pos 180-181 (G074)</summary>
    public int EspecieTitulo { get; init; }

    /// <summary>Data de Emissão do Título — pos 182-189 (G075)</summary>
    public DateOnly? DataEmissao { get; init; }

    /// <summary>Juros de Mora por Dia — pos 190-204 (G076), 2 decimais</summary>
    public decimal JurosMoraDia { get; init; }

    /// <summary>Código do Desconto 1 — pos 205-205 (G077)</summary>
    public int CodigoDesconto1 { get; init; }

    /// <summary>Data do Desconto 1 — pos 206-213 (G078)</summary>
    public DateOnly? DataDesconto1 { get; init; }

    /// <summary>Valor do Desconto 1 — pos 214-228 (G079), 2 decimais</summary>
    public decimal ValorDesconto1 { get; init; }

    /// <summary>Código do Protesto — pos 229-229 (G080)</summary>
    public int CodigoProtesto { get; init; }

    /// <summary>Número de Dias para Protesto — pos 230-231 (G081)</summary>
    public int NumeroDiasProtesto { get; init; }

    /// <summary>Data Limite de Pagamento — pos 232-239 (G082)</summary>
    public DateOnly? DataLimitePagamento { get; init; }

    /// <summary>Uso Reservado CNAB — pos 240-240 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento G.</summary>
    public static SegmentoG Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoG
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
            CodigoBarras = linha.ExtrairAlfa(18, 61),
            TipoInscricaoBeneficiario = linha.ExtrairInt(62, 62),
            NumeroInscricaoBeneficiario = linha.ExtrairLong(63, 77),
            NomeBeneficiario = linha.ExtrairAlfa(78, 107),
            DataVencimento = linha.ExtrairData(108, 115),
            ValorNominalTitulo = linha.ExtrairDecimal(116, 130, 2),
            QuantidadeMoeda = linha.ExtrairDecimal(131, 145, 5),
            CodigoMoeda = linha.ExtrairInt(146, 147),
            NumeroDocumentoCobranca = linha.ExtrairAlfa(148, 162),
            AgenciaCobranca = linha.ExtrairInt(163, 167),
            DVAgencia = linha.ExtrairAlfa(168, 168),
            PracaCobradora = linha.ExtrairAlfa(169, 178),
            CodigoCarteira = linha.ExtrairAlfa(179, 179),
            EspecieTitulo = linha.ExtrairInt(180, 181),
            DataEmissao = linha.ExtrairData(182, 189),
            JurosMoraDia = linha.ExtrairDecimal(190, 204, 2),
            CodigoDesconto1 = linha.ExtrairInt(205, 205),
            DataDesconto1 = linha.ExtrairData(206, 213),
            ValorDesconto1 = linha.ExtrairDecimal(214, 228, 2),
            CodigoProtesto = linha.ExtrairInt(229, 229),
            NumeroDiasProtesto = linha.ExtrairInt(230, 231),
            DataLimitePagamento = linha.ExtrairData(232, 239),
            CNAB2 = linha.ExtrairAlfa(240, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                    // 01: 1-3    (3)
            LoteServico.PadNum(4),                    // 02: 4-7    (4)
            "3",                                       // 03: 8-8    (1) fixo
            NumeroSequencial.PadNum(5),               // 04: 9-13   (5)
            "G",                                       // 05: 14-14  (1) fixo
            "".PadAlfa(1),                            // 06: 15-15  (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2),         // 07: 16-17  (2)
            CodigoBarras.PadAlfa(44),                 // 08: 18-61  (44)
            TipoInscricaoBeneficiario.PadNum(1),      // 09: 62-62  (1)
            NumeroInscricaoBeneficiario.PadNum(15),   // 10: 63-77  (15)
            NomeBeneficiario.PadAlfa(30),             // 11: 78-107 (30)
            DataVencimento.FormatarData(),            // 12: 108-115 (8)
            ValorNominalTitulo.PadDecimal(15, 2),    // 13: 116-130 (15)
            QuantidadeMoeda.PadDecimal(15, 5),       // 14: 131-145 (15)
            CodigoMoeda.PadNum(2),                   // 15: 146-147 (2)
            NumeroDocumentoCobranca.PadAlfa(15),     // 16: 148-162 (15)
            AgenciaCobranca.PadNum(5),               // 17: 163-167 (5)
            DVAgencia.PadAlfa(1),                    // 18: 168-168 (1)
            PracaCobradora.PadAlfa(10),              // 19: 169-178 (10)
            CodigoCarteira.PadAlfa(1),               // 20: 179-179 (1)
            EspecieTitulo.PadNum(2),                 // 21: 180-181 (2)
            DataEmissao.FormatarData(),              // 22: 182-189 (8)
            JurosMoraDia.PadDecimal(15, 2),          // 23: 190-204 (15)
            CodigoDesconto1.PadNum(1),               // 24: 205-205 (1)
            DataDesconto1.FormatarData(),            // 25: 206-213 (8)
            ValorDesconto1.PadDecimal(15, 2),        // 26: 214-228 (15)
            CodigoProtesto.PadNum(1),                // 27: 229-229 (1)
            NumeroDiasProtesto.PadNum(2),            // 28: 230-231 (2)
            DataLimitePagamento.FormatarData(),      // 29: 232-239 (8)
            "".PadAlfa(1)                            // 30: 240-240 (1) CNAB Brancos
        );
    // SOMA: 3+4+1+5+1+1+2+44+1+15+30+8+15+15+2+15+5+1+10+1+2+8+15+1+8+15+1+2+8+1 = 240 ✓
}
