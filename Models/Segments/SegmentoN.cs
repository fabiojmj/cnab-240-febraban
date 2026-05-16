using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento N — Pagamentos de Tributos (Detalhe Tipo 3, Segmento N).</summary>
public sealed record SegmentoN : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'N';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "N"</summary>
    // Fixed literal "N" in ToLinhaFormatada

    /// <summary>
    /// Bloco combinado de Código de Movimento de Retorno (pos 15-16) e Código de Instrução de Movimento (pos 16-17).
    /// Armazenado como 3 dígitos (pos 15-17). Use CodigoMovimentoRetorno e CodigoInstrucaoMovimento para acessar.
    /// pos 15-17 (N001+N002)
    /// </summary>
    public int CodigoMovimentoEInstrucao { get; init; }

    /// <summary>Código de Movimento de Retorno (primeiros 2 dígitos de pos 15-17).</summary>
    public int CodigoMovimentoRetorno => CodigoMovimentoEInstrucao / 10;

    /// <summary>Código de Instrução de Movimento (últimos 2 dígitos de pos 15-17).</summary>
    public int CodigoInstrucaoMovimento => CodigoMovimentoEInstrucao % 100;

    /// <summary>Número do Documento Atribuído pela Empresa — pos 18-37 (N003)</summary>
    public string NumeroDoctoEmpresa { get; init; } = "";

    /// <summary>Número do Documento Atribuído pelo Banco — pos 38-57 (N004)</summary>
    public string NumeroDoctoAtribBanco { get; init; } = "";

    /// <summary>Nome do Contribuinte — pos 58-87 (N005)</summary>
    public string NomeContribuinte { get; init; } = "";

    /// <summary>Data do Pagamento — pos 88-95 (N006)</summary>
    public DateOnly? DataPagamento { get; init; }

    /// <summary>Valor Total do Pagamento — pos 96-110 (N007), 2 decimais</summary>
    public decimal ValorTotalPagamento { get; init; }

    /// <summary>Informações Complementares — pos 111-230 (N008)</summary>
    public string InformacoesComplementares { get; init; } = "";

    /// <summary>Códigos de Ocorrências — pos 231-240 (G061)</summary>
    public string CodigosOcorrencias { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento N.</summary>
    public static SegmentoN Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoN
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CodigoMovimentoEInstrucao = linha.ExtrairInt(15, 17),
            NumeroDoctoEmpresa = linha.ExtrairAlfa(18, 37),
            NumeroDoctoAtribBanco = linha.ExtrairAlfa(38, 57),
            NomeContribuinte = linha.ExtrairAlfa(58, 87),
            DataPagamento = linha.ExtrairData(88, 95),
            ValorTotalPagamento = linha.ExtrairDecimal(96, 110, 2),
            InformacoesComplementares = linha.ExtrairAlfa(111, 230),
            CodigosOcorrencias = linha.ExtrairAlfa(231, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                    // 01: 1-3   (3)
            LoteServico.PadNum(4),                    // 02: 4-7   (4)
            "3",                                       // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),               // 04: 9-13  (5)
            "N",                                       // 05: 14-14 (1) fixo
            CodigoMovimentoEInstrucao.PadNum(3),      // 06+07: 15-17 (3) combinado
            NumeroDoctoEmpresa.PadAlfa(20),           // 08: 18-37 (20)
            NumeroDoctoAtribBanco.PadAlfa(20),        // 09: 38-57 (20)
            NomeContribuinte.PadAlfa(30),             // 10: 58-87 (30)
            DataPagamento.FormatarData(),             // 11: 88-95 (8)
            ValorTotalPagamento.PadDecimal(15, 2),   // 12: 96-110 (15)
            InformacoesComplementares.PadAlfa(120),  // 13: 111-230 (120)
            CodigosOcorrencias.PadAlfa(10)           // 14: 231-240 (10)
        );
    // SOMA: 3+4+1+5+1+3+20+20+30+8+15+120+10 = 240 ✓
}
