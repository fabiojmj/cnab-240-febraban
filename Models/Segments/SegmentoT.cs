using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento T — Retorno de Cobrança (Detalhe Tipo 3, Segmento T).</summary>
public sealed record SegmentoT : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'T';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "T"</summary>
    // Fixed literal "T" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento de Retorno — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRetorno { get; init; }

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

    /// <summary>Identificação do Título no Banco — pos 38-57 (T001)</summary>
    public string IdentificacaoTitulo { get; init; } = "";

    /// <summary>Código da Carteira — pos 58-58 (G073)</summary>
    public int CodigoCarteira { get; init; }

    /// <summary>Número do Documento de Cobrança — pos 59-73 (T002)</summary>
    public string NumeroDocumentoCobranca { get; init; } = "";

    /// <summary>Data de Vencimento do Título — pos 74-81 (G065)</summary>
    public DateOnly? DataVencimento { get; init; }

    /// <summary>Valor Nominal do Título — pos 82-96 (G066), 2 decimais</summary>
    public decimal ValorNominalTitulo { get; init; }

    /// <summary>Número do Banco Cobrador/Recebedor — pos 97-99 (T003)</summary>
    public int NumeroBancoCobradorRecebedor { get; init; }

    /// <summary>Agência Cobradora/Recebedora — pos 100-104 (T004)</summary>
    public int AgenciaCobradoraRecebedora { get; init; }

    /// <summary>Dígito Verificador da Agência Cobradora — pos 105-105 (T005)</summary>
    public string DVAgenciaCobradora { get; init; } = "";

    /// <summary>Identificação do Título na Empresa — pos 106-130 (T006)</summary>
    public string IdentificacaoTituloEmpresa { get; init; } = "";

    /// <summary>Código da Moeda — pos 131-132 (G068)</summary>
    public int CodigoMoeda { get; init; }

    /// <summary>Tipo de Inscrição do Pagador — pos 133-133 (G005)</summary>
    public int TipoInscricaoPagador { get; init; }

    /// <summary>Número de Inscrição do Pagador — pos 134-148 (G006)</summary>
    public long NumeroInscricaoPagador { get; init; }

    /// <summary>Nome do Pagador — pos 149-188 (T007)</summary>
    public string NomePagador { get; init; } = "";

    /// <summary>Número do Contrato do Crédito — pos 189-198 (T008)</summary>
    public long NumeroContratoCredito { get; init; }

    /// <summary>Valor da Tarifa/Custas — pos 199-213 (T009), 2 decimais</summary>
    public decimal ValorTarifaCustas { get; init; }

    /// <summary>Motivo da Ocorrência — pos 214-223 (T010)</summary>
    public string MotivoOcorrencia { get; init; } = "";

    /// <summary>Uso Reservado CNAB 2 — pos 224-240 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento T.</summary>
    public static SegmentoT Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoT
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRetorno = linha.ExtrairInt(16, 17),
            AgenciaCodigo = linha.ExtrairInt(18, 22),
            AgenciaDV = linha.ExtrairAlfa(23, 23),
            ContaNumero = linha.ExtrairLong(24, 35),
            ContaDV = linha.ExtrairAlfa(36, 36),
            DVAgenciaConta = linha.ExtrairAlfa(37, 37),
            IdentificacaoTitulo = linha.ExtrairAlfa(38, 57),
            CodigoCarteira = linha.ExtrairInt(58, 58),
            NumeroDocumentoCobranca = linha.ExtrairAlfa(59, 73),
            DataVencimento = linha.ExtrairData(74, 81),
            ValorNominalTitulo = linha.ExtrairDecimal(82, 96, 2),
            NumeroBancoCobradorRecebedor = linha.ExtrairInt(97, 99),
            AgenciaCobradoraRecebedora = linha.ExtrairInt(100, 104),
            DVAgenciaCobradora = linha.ExtrairAlfa(105, 105),
            IdentificacaoTituloEmpresa = linha.ExtrairAlfa(106, 130),
            CodigoMoeda = linha.ExtrairInt(131, 132),
            TipoInscricaoPagador = linha.ExtrairInt(133, 133),
            NumeroInscricaoPagador = linha.ExtrairLong(134, 148),
            NomePagador = linha.ExtrairAlfa(149, 188),
            NumeroContratoCredito = linha.ExtrairLong(189, 198),
            ValorTarifaCustas = linha.ExtrairDecimal(199, 213, 2),
            MotivoOcorrencia = linha.ExtrairAlfa(214, 223),
            CNAB2 = linha.ExtrairAlfa(224, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                    // 01: 1-3   (3)
            LoteServico.PadNum(4),                    // 02: 4-7   (4)
            "3",                                       // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),               // 04: 9-13  (5)
            "T",                                       // 05: 14-14 (1) fixo
            "".PadAlfa(1),                            // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRetorno.PadNum(2),         // 07: 16-17 (2)
            AgenciaCodigo.PadNum(5),                  // 08: 18-22 (5)
            AgenciaDV.PadAlfa(1),                     // 09: 23-23 (1)
            ContaNumero.PadNum(12),                   // 10: 24-35 (12)
            ContaDV.PadAlfa(1),                       // 11: 36-36 (1)
            DVAgenciaConta.PadAlfa(1),                // 12: 37-37 (1)
            IdentificacaoTitulo.PadAlfa(20),          // 13: 38-57 (20)
            CodigoCarteira.PadNum(1),                 // 14: 58-58 (1)
            NumeroDocumentoCobranca.PadAlfa(15),      // 15: 59-73 (15)
            DataVencimento.FormatarData(),            // 16: 74-81 (8)
            ValorNominalTitulo.PadDecimal(15, 2),    // 17: 82-96 (15)
            NumeroBancoCobradorRecebedor.PadNum(3),  // 18: 97-99 (3)
            AgenciaCobradoraRecebedora.PadNum(5),    // 19: 100-104 (5)
            DVAgenciaCobradora.PadAlfa(1),           // 20: 105-105 (1)
            IdentificacaoTituloEmpresa.PadAlfa(25),  // 21: 106-130 (25)
            CodigoMoeda.PadNum(2),                   // 22: 131-132 (2)
            TipoInscricaoPagador.PadNum(1),          // 23: 133-133 (1)
            NumeroInscricaoPagador.PadNum(15),       // 24: 134-148 (15)
            NomePagador.PadAlfa(40),                 // 25: 149-188 (40)
            NumeroContratoCredito.PadNum(10),        // 26: 189-198 (10)
            ValorTarifaCustas.PadDecimal(15, 2),     // 27: 199-213 (15)
            MotivoOcorrencia.PadAlfa(10),            // 28: 214-223 (10)
            "".PadAlfa(17)                           // 29: 224-240 (17) CNAB2 Brancos
        );
    // SOMA: 3+4+1+5+1+1+2+5+1+12+1+1+20+1+15+8+15+3+5+1+25+2+1+15+40+10+15+10+17 = 240 ✓
}
