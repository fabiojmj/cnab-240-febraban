using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento R — Desconto/Multa (Detalhe Tipo 3, Segmento R).</summary>
public sealed record SegmentoR : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'R';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "R"</summary>
    // Fixed literal "R" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução (Remessa) — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Código do Desconto 2 — pos 18-18 (R001)</summary>
    public int CodigoDesconto2 { get; init; }

    /// <summary>Data do Desconto 2 — pos 19-26 (R002)</summary>
    public DateOnly? DataDesconto2 { get; init; }

    /// <summary>Valor do Desconto 2 — pos 27-41 (R003), 2 decimais</summary>
    public decimal ValorDesconto2 { get; init; }

    /// <summary>Código do Desconto 3 — pos 42-42 (R004)</summary>
    public int CodigoDesconto3 { get; init; }

    /// <summary>Data do Desconto 3 — pos 43-50 (R005)</summary>
    public DateOnly? DataDesconto3 { get; init; }

    /// <summary>Valor do Desconto 3 — pos 51-65 (R006), 2 decimais</summary>
    public decimal ValorDesconto3 { get; init; }

    /// <summary>Código da Multa — pos 66-66 (R007)</summary>
    public string CodigoMulta { get; init; } = "";

    /// <summary>Data da Multa — pos 67-74 (R008)</summary>
    public DateOnly? DataMulta { get; init; }

    /// <summary>Valor da Multa — pos 75-89 (R009), 2 decimais</summary>
    public decimal ValorMulta { get; init; }

    /// <summary>Informação ao Pagador — pos 90-99 (R010)</summary>
    public string InformacaoPagador { get; init; } = "";

    /// <summary>Mensagem 3 — pos 100-139 (R011)</summary>
    public string Mensagem3 { get; init; } = "";

    /// <summary>Mensagem 4 — pos 140-179 (R012)</summary>
    public string Mensagem4 { get; init; } = "";

    /// <summary>Uso Reservado CNAB 2 — pos 180-199 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Código de Ocorrência do Pagador — pos 200-207 (R013)</summary>
    public long CodigoOcorrenciaPagador { get; init; }

    /// <summary>Banco para Débito — pos 208-210 (G001)</summary>
    public int BancoDebito { get; init; }

    /// <summary>Agência para Débito — pos 211-215 (G008)</summary>
    public int AgenciaDebito { get; init; }

    /// <summary>Dígito Verificador da Agência para Débito — pos 216-216 (G009)</summary>
    public string DVAgenciaDebito { get; init; } = "";

    /// <summary>Conta para Débito — pos 217-228 (G010)</summary>
    public long ContaDebito { get; init; }

    /// <summary>Dígito Verificador da Conta para Débito — pos 229-229 (G011)</summary>
    public string DVContaDebito { get; init; } = "";

    /// <summary>Dígito Verificador da Agência/Conta para Débito — pos 230-230 (G012)</summary>
    public string DVAgenciaContaDebito { get; init; } = "";

    /// <summary>Aviso para Débito Automático — pos 231-231 (R014)</summary>
    public int AvisoDebitoAutomatico { get; init; }

    /// <summary>Uso Reservado CNAB 3 — pos 232-240 (G004), Brancos</summary>
    public string CNAB3 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento R.</summary>
    public static SegmentoR Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoR
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
            CodigoDesconto2 = linha.ExtrairInt(18, 18),
            DataDesconto2 = linha.ExtrairData(19, 26),
            ValorDesconto2 = linha.ExtrairDecimal(27, 41, 2),
            CodigoDesconto3 = linha.ExtrairInt(42, 42),
            DataDesconto3 = linha.ExtrairData(43, 50),
            ValorDesconto3 = linha.ExtrairDecimal(51, 65, 2),
            CodigoMulta = linha.ExtrairAlfa(66, 66),
            DataMulta = linha.ExtrairData(67, 74),
            ValorMulta = linha.ExtrairDecimal(75, 89, 2),
            InformacaoPagador = linha.ExtrairAlfa(90, 99),
            Mensagem3 = linha.ExtrairAlfa(100, 139),
            Mensagem4 = linha.ExtrairAlfa(140, 179),
            CNAB2 = linha.ExtrairAlfa(180, 199),
            CodigoOcorrenciaPagador = linha.ExtrairLong(200, 207),
            BancoDebito = linha.ExtrairInt(208, 210),
            AgenciaDebito = linha.ExtrairInt(211, 215),
            DVAgenciaDebito = linha.ExtrairAlfa(216, 216),
            ContaDebito = linha.ExtrairLong(217, 228),
            DVContaDebito = linha.ExtrairAlfa(229, 229),
            DVAgenciaContaDebito = linha.ExtrairAlfa(230, 230),
            AvisoDebitoAutomatico = linha.ExtrairInt(231, 231),
            CNAB3 = linha.ExtrairAlfa(232, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                   // 01: 1-3   (3)
            LoteServico.PadNum(4),                   // 02: 4-7   (4)
            "3",                                      // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),              // 04: 9-13  (5)
            "R",                                      // 05: 14-14 (1) fixo
            "".PadAlfa(1),                           // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2),        // 07: 16-17 (2)
            CodigoDesconto2.PadNum(1),               // 08: 18-18 (1)
            DataDesconto2.FormatarData(),            // 09: 19-26 (8)
            ValorDesconto2.PadDecimal(15, 2),        // 10: 27-41 (15)
            CodigoDesconto3.PadNum(1),               // 11: 42-42 (1)
            DataDesconto3.FormatarData(),            // 12: 43-50 (8)
            ValorDesconto3.PadDecimal(15, 2),        // 13: 51-65 (15)
            CodigoMulta.PadAlfa(1),                  // 14: 66-66 (1)
            DataMulta.FormatarData(),                // 15: 67-74 (8)
            ValorMulta.PadDecimal(15, 2),            // 16: 75-89 (15)
            InformacaoPagador.PadAlfa(10),           // 17: 90-99 (10)
            Mensagem3.PadAlfa(40),                   // 18: 100-139 (40)
            Mensagem4.PadAlfa(40),                   // 19: 140-179 (40)
            "".PadAlfa(20),                          // 20: 180-199 (20) CNAB2 Brancos
            CodigoOcorrenciaPagador.PadNum(8),       // 21: 200-207 (8)
            BancoDebito.PadNum(3),                   // 22: 208-210 (3)
            AgenciaDebito.PadNum(5),                 // 23: 211-215 (5)
            DVAgenciaDebito.PadAlfa(1),              // 24: 216-216 (1)
            ContaDebito.PadNum(12),                  // 25: 217-228 (12)
            DVContaDebito.PadAlfa(1),                // 26: 229-229 (1)
            DVAgenciaContaDebito.PadAlfa(1),         // 27: 230-230 (1)
            AvisoDebitoAutomatico.PadNum(1),         // 28: 231-231 (1)
            "".PadAlfa(9)                            // 29: 232-240 (9) CNAB3 Brancos
        );
    // SOMA: 3+4+1+5+1+1+2+1+8+15+1+8+15+1+8+15+10+40+40+20+8+3+5+1+12+1+1+1+9 = 240 ✓
}
