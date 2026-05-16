using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento U — Retorno de Cobrança Complementar (Detalhe Tipo 3, Segmento U).</summary>
public sealed record SegmentoU : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'U';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "U"</summary>
    // Fixed literal "U" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento de Retorno — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRetorno { get; init; }

    /// <summary>Juros/Multa/Encargos — pos 18-32 (U001), 2 decimais</summary>
    public decimal JurosMultaEncargos { get; init; }

    /// <summary>Valor do Desconto Concedido — pos 33-47 (U002), 2 decimais</summary>
    public decimal ValorDescontoConcedido { get; init; }

    /// <summary>Valor do Abatimento Concedido — pos 48-62 (U003), 2 decimais</summary>
    public decimal ValorAbatimentoConcedido { get; init; }

    /// <summary>Valor do IOF Recolhido — pos 63-77 (U004), 2 decimais</summary>
    public decimal ValorIOFRecolhido { get; init; }

    /// <summary>Valor Pago pelo Pagador — pos 78-92 (U005), 2 decimais</summary>
    public decimal ValorPagoPagador { get; init; }

    /// <summary>Valor Líquido a ser Creditado — pos 93-107 (U006), 2 decimais</summary>
    public decimal ValorLiquidoCreditado { get; init; }

    /// <summary>Valor de Outras Despesas — pos 108-122 (U007), 2 decimais</summary>
    public decimal ValorOutrasDespesas { get; init; }

    /// <summary>Valor de Outros Créditos — pos 123-137 (U008), 2 decimais</summary>
    public decimal ValorOutrosCreditos { get; init; }

    /// <summary>Data da Ocorrência — pos 138-145 (U009)</summary>
    public DateOnly? DataOcorrencia { get; init; }

    /// <summary>Data da Efetivação do Crédito — pos 146-153 (U010)</summary>
    public DateOnly? DataEfetivacaoCredito { get; init; }

    /// <summary>Código da Ocorrência — pos 154-157 (U011)</summary>
    public string CodigoOcorrencia { get; init; } = "";

    /// <summary>Data da Ocorrência 2 — pos 158-165 (C058)</summary>
    public string DataOcorrencia2 { get; init; } = "";

    /// <summary>Valor da Ocorrência — pos 166-180 (U012), 2 decimais</summary>
    public decimal ValorOcorrencia { get; init; }

    /// <summary>Complemento da Ocorrência — pos 181-210 (U013)</summary>
    public string ComplementoOcorrencia { get; init; } = "";

    /// <summary>Código do Banco Correspondente — pos 211-213 (U014)</summary>
    public int CodigoBancoCorrespondente { get; init; }

    /// <summary>Nosso Número no Banco Correspondente — pos 214-233 (U015)</summary>
    public long NossoNumeroBancoCorrespondente { get; init; }

    /// <summary>Uso Reservado CNAB 2 — pos 234-240 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento U.</summary>
    public static SegmentoU Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoU
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRetorno = linha.ExtrairInt(16, 17),
            JurosMultaEncargos = linha.ExtrairDecimal(18, 32, 2),
            ValorDescontoConcedido = linha.ExtrairDecimal(33, 47, 2),
            ValorAbatimentoConcedido = linha.ExtrairDecimal(48, 62, 2),
            ValorIOFRecolhido = linha.ExtrairDecimal(63, 77, 2),
            ValorPagoPagador = linha.ExtrairDecimal(78, 92, 2),
            ValorLiquidoCreditado = linha.ExtrairDecimal(93, 107, 2),
            ValorOutrasDespesas = linha.ExtrairDecimal(108, 122, 2),
            ValorOutrosCreditos = linha.ExtrairDecimal(123, 137, 2),
            DataOcorrencia = linha.ExtrairData(138, 145),
            DataEfetivacaoCredito = linha.ExtrairData(146, 153),
            CodigoOcorrencia = linha.ExtrairAlfa(154, 157),
            DataOcorrencia2 = linha.ExtrairAlfa(158, 165),
            ValorOcorrencia = linha.ExtrairDecimal(166, 180, 2),
            ComplementoOcorrencia = linha.ExtrairAlfa(181, 210),
            CodigoBancoCorrespondente = linha.ExtrairInt(211, 213),
            NossoNumeroBancoCorrespondente = linha.ExtrairLong(214, 233),
            CNAB2 = linha.ExtrairAlfa(234, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                              // 01: 1-3   (3)
            LoteServico.PadNum(4),                              // 02: 4-7   (4)
            "3",                                                 // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),                         // 04: 9-13  (5)
            "U",                                                 // 05: 14-14 (1) fixo
            "".PadAlfa(1),                                      // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRetorno.PadNum(2),                   // 07: 16-17 (2)
            JurosMultaEncargos.PadDecimal(15, 2),               // 08: 18-32 (15)
            ValorDescontoConcedido.PadDecimal(15, 2),           // 09: 33-47 (15)
            ValorAbatimentoConcedido.PadDecimal(15, 2),         // 10: 48-62 (15)
            ValorIOFRecolhido.PadDecimal(15, 2),                // 11: 63-77 (15)
            ValorPagoPagador.PadDecimal(15, 2),                 // 12: 78-92 (15)
            ValorLiquidoCreditado.PadDecimal(15, 2),            // 13: 93-107 (15)
            ValorOutrasDespesas.PadDecimal(15, 2),              // 14: 108-122 (15)
            ValorOutrosCreditos.PadDecimal(15, 2),              // 15: 123-137 (15)
            DataOcorrencia.FormatarData(),                      // 16: 138-145 (8)
            DataEfetivacaoCredito.FormatarData(),               // 17: 146-153 (8)
            CodigoOcorrencia.PadAlfa(4),                        // 18: 154-157 (4)
            DataOcorrencia2.PadAlfa(8),                         // 19: 158-165 (8)
            ValorOcorrencia.PadDecimal(15, 2),                  // 20: 166-180 (15)
            ComplementoOcorrencia.PadAlfa(30),                  // 21: 181-210 (30)
            CodigoBancoCorrespondente.PadNum(3),                // 22: 211-213 (3)
            NossoNumeroBancoCorrespondente.PadNum(20),          // 23: 214-233 (20)
            "".PadAlfa(7)                                       // 24: 234-240 (7) CNAB2 Brancos
        );
    // SOMA: 3+4+1+5+1+1+2+15+15+15+15+15+15+15+15+8+8+4+8+15+30+3+20+7 = 240 ✓
}
