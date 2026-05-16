using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento D — Cheques (Detalhe Tipo 3, Segmento D).</summary>
public sealed record SegmentoD : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'D';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "D"</summary>
    // Fixed literal "D" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Tipo de Movimento Remessa/Retorno — pos 16-17 (D001)</summary>
    public int TipoMovimentoRemessaRetorno { get; init; }

    /// <summary>Código de Finalidade do Movimento — pos 18-19 (D002)</summary>
    public int CodigoFinalidadeMovimento { get; init; }

    /// <summary>Forma de Entrada dos Dados do Cheque — pos 20-20 (D003)</summary>
    public int FormaEntradaDadosCheque { get; init; }

    /// <summary>CMC7 — pos 21-54 (D004)</summary>
    public string CMC7 { get; init; } = "";

    /// <summary>Tipo de Inscrição do Emitente — pos 55-55 (D005)</summary>
    public int TipoInscricaoEmitente { get; init; }

    /// <summary>Número de Inscrição do Emitente — pos 56-69 (D006)</summary>
    public long NumeroInscricaoEmitente { get; init; }

    /// <summary>Valor do Cheque — pos 70-84 (D007), 2 decimais</summary>
    public decimal ValorCheque { get; init; }

    /// <summary>Data de Captura do Cheque — pos 85-92 (D008)</summary>
    public DateOnly? DataCapturaCheque { get; init; }

    /// <summary>Data de Depósito do Cheque — pos 93-100 (D009)</summary>
    public DateOnly? DataDepositoCheque { get; init; }

    /// <summary>Data Prevista de Débito/Crédito — pos 101-108 (D010)</summary>
    public DateOnly? DataPrevistaDebitoCredito { get; init; }

    /// <summary>Número Atribuído ao Título pelo Cliente — pos 109-128 (D011)</summary>
    public string NumeroAtribuidoCliente { get; init; } = "";

    /// <summary>Uso Exclusivo do Banco — pos 129-143 (D012), Brancos</summary>
    public string UsoExclusivoBanco { get; init; } = "";

    /// <summary>Código da Agência de Devolução — pos 144-148 (D013)</summary>
    public int CodigoAgenciaDevolucao { get; init; }

    /// <summary>Número da Conta de Devolução — pos 149-160 (D014)</summary>
    public long NumeroContaDevolucao { get; init; }

    /// <summary>Valor de Juros de Empréstimo — pos 161-171 (D015), 2 decimais</summary>
    public decimal ValorJurosEmprestimo { get; init; }

    /// <summary>Valor de IOF do Empréstimo — pos 172-182 (D016), 2 decimais</summary>
    public decimal ValorIOFEmprestimo { get; init; }

    /// <summary>Valor de Outros Encargos do Empréstimo — pos 183-193 (D017), 2 decimais</summary>
    public decimal ValorOutrosEncargosEmprestimo { get; init; }

    /// <summary>Número do Contrato do Empréstimo — pos 194-210 (D018)</summary>
    public long NumeroContratoEmprestimo { get; init; }

    /// <summary>Taxa de Juros do Empréstimo — pos 211-217 (D019), 4 decimais</summary>
    public decimal TaxaJurosEmprestimo { get; init; }

    /// <summary>Uso Reservado CNAB 2 — pos 218-230 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Códigos de Ocorrências — pos 231-240 (G061)</summary>
    public string CodigosOcorrencias { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento D.</summary>
    public static SegmentoD Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoD
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            TipoMovimentoRemessaRetorno = linha.ExtrairInt(16, 17),
            CodigoFinalidadeMovimento = linha.ExtrairInt(18, 19),
            FormaEntradaDadosCheque = linha.ExtrairInt(20, 20),
            CMC7 = linha.ExtrairAlfa(21, 54),
            TipoInscricaoEmitente = linha.ExtrairInt(55, 55),
            NumeroInscricaoEmitente = linha.ExtrairLong(56, 69),
            ValorCheque = linha.ExtrairDecimal(70, 84, 2),
            DataCapturaCheque = linha.ExtrairData(85, 92),
            DataDepositoCheque = linha.ExtrairData(93, 100),
            DataPrevistaDebitoCredito = linha.ExtrairData(101, 108),
            NumeroAtribuidoCliente = linha.ExtrairAlfa(109, 128),
            UsoExclusivoBanco = linha.ExtrairAlfa(129, 143),
            CodigoAgenciaDevolucao = linha.ExtrairInt(144, 148),
            NumeroContaDevolucao = linha.ExtrairLong(149, 160),
            ValorJurosEmprestimo = linha.ExtrairDecimal(161, 171, 2),
            ValorIOFEmprestimo = linha.ExtrairDecimal(172, 182, 2),
            ValorOutrosEncargosEmprestimo = linha.ExtrairDecimal(183, 193, 2),
            NumeroContratoEmprestimo = linha.ExtrairLong(194, 210),
            TaxaJurosEmprestimo = linha.ExtrairDecimal(211, 217, 4),
            CNAB2 = linha.ExtrairAlfa(218, 230),
            CodigosOcorrencias = linha.ExtrairAlfa(231, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                          // 01: 1-3   (3)
            LoteServico.PadNum(4),                          // 02: 4-7   (4)
            "3",                                             // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),                     // 04: 9-13  (5)
            "D",                                             // 05: 14-14 (1) fixo
            "".PadAlfa(1),                                  // 06: 15-15 (1) CNAB Brancos
            TipoMovimentoRemessaRetorno.PadNum(2),          // 07: 16-17 (2)
            CodigoFinalidadeMovimento.PadNum(2),            // 08: 18-19 (2)
            FormaEntradaDadosCheque.PadNum(1),              // 09: 20-20 (1)
            CMC7.PadAlfa(34),                               // 10: 21-54 (34)
            TipoInscricaoEmitente.PadNum(1),                // 11: 55-55 (1)
            NumeroInscricaoEmitente.PadNum(14),             // 12: 56-69 (14)
            ValorCheque.PadDecimal(15, 2),                  // 13: 70-84 (15)
            DataCapturaCheque.FormatarData(),               // 14: 85-92 (8)
            DataDepositoCheque.FormatarData(),              // 15: 93-100 (8)
            DataPrevistaDebitoCredito.FormatarData(),       // 16: 101-108 (8)
            NumeroAtribuidoCliente.PadAlfa(20),             // 17: 109-128 (20)
            "".PadAlfa(15),                                 // 18: 129-143 (15) UsoExclusivoBanco Brancos
            CodigoAgenciaDevolucao.PadNum(5),               // 19: 144-148 (5)
            NumeroContaDevolucao.PadNum(12),                // 20: 149-160 (12)
            ValorJurosEmprestimo.PadDecimal(11, 2),         // 21: 161-171 (11)
            ValorIOFEmprestimo.PadDecimal(11, 2),           // 22: 172-182 (11)
            ValorOutrosEncargosEmprestimo.PadDecimal(11, 2),// 23: 183-193 (11)
            NumeroContratoEmprestimo.PadNum(17),            // 24: 194-210 (17)
            TaxaJurosEmprestimo.PadDecimal(7, 4),          // 25: 211-217 (7)
            "".PadAlfa(13),                                 // 26: 218-230 (13) CNAB2 Brancos
            CodigosOcorrencias.PadAlfa(10)                 // 27: 231-240 (10)
        );
    // SOMA: 3+4+1+5+1+1+2+2+1+34+1+14+15+8+8+8+20+15+5+12+11+11+11+17+7+13+10 = 240 ✓
}
