using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento A — Transferências e DOC/TED (Detalhe Tipo 3, Segmento A).</summary>
public sealed record SegmentoA : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'A';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "A"</summary>
    // Fixed literal "A" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução (Remessa) — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Câmara Centralizadora — pos 18-20 (P001)</summary>
    public int CamaraCentralizadora { get; init; }

    /// <summary>Banco do Favorecido — pos 21-23 (P002)</summary>
    public int BancoFavorecido { get; init; }

    /// <summary>Código da Agência do Favorecido — pos 24-28 (P003)</summary>
    public int AgenciaFavorecidoCodigo { get; init; }

    /// <summary>Dígito Verificador da Agência do Favorecido — pos 29-29 (P004)</summary>
    public string AgenciaFavorecidoDV { get; init; } = "";

    /// <summary>Número da Conta do Favorecido — pos 30-41 (P005)</summary>
    public long ContaFavorecidoNumero { get; init; }

    /// <summary>Dígito Verificador da Conta do Favorecido — pos 42-42 (P006)</summary>
    public string ContaFavorecidoDV { get; init; } = "";

    /// <summary>Dígito Verificador da Agência/Conta do Favorecido — pos 43-43 (P007)</summary>
    public string DVAgenciaConta { get; init; } = "";

    /// <summary>Nome do Favorecido — pos 44-73 (P008)</summary>
    public string NomeFavorecido { get; init; } = "";

    /// <summary>Número do Documento atribuído pela Empresa — pos 74-93 (P009)</summary>
    public string NumeroDoctoEmpresa { get; init; } = "";

    /// <summary>Data do Pagamento — pos 94-101 (P010)</summary>
    public DateOnly? DataPagamento { get; init; }

    /// <summary>Tipo da Moeda — pos 102-104 (P011)</summary>
    public string TipoMoeda { get; init; } = "BRL";

    /// <summary>Quantidade da Moeda — pos 105-119 (P012), 5 decimais</summary>
    public decimal QuantidadeMoeda { get; init; }

    /// <summary>Valor do Pagamento — pos 120-134 (P013), 2 decimais</summary>
    public decimal ValorPagamento { get; init; }

    /// <summary>Número do Documento atribuído pelo Banco — pos 135-154 (P014)</summary>
    public string NumeroDoctoAtribBanco { get; init; } = "";

    /// <summary>Data da Efetivação do Pagamento — pos 155-162 (P015)</summary>
    public DateOnly? DataEfetivacao { get; init; }

    /// <summary>Valor da Efetivação do Pagamento — pos 163-177 (P016), 2 decimais</summary>
    public decimal ValorEfetivacao { get; init; }

    /// <summary>Informação Complementar — pos 178-217 (P017)</summary>
    public string InformacaoComplementar { get; init; } = "";

    /// <summary>Finalidade do TED — pos 218-222 (P018)</summary>
    public int FinalidadeTed { get; init; }

    /// <summary>Uso Reservado CNAB 2 — pos 223-229 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Código do Aviso — pos 230-230 (P019)</summary>
    public int CodigoAviso { get; init; }

    /// <summary>Ocorrências de Retorno — pos 231-240 (G061)</summary>
    public string OcorrenciasRetorno { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento A.</summary>
    public static SegmentoA Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoA
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
            CamaraCentralizadora = linha.ExtrairInt(18, 20),
            BancoFavorecido = linha.ExtrairInt(21, 23),
            AgenciaFavorecidoCodigo = linha.ExtrairInt(24, 28),
            AgenciaFavorecidoDV = linha.ExtrairAlfa(29, 29),
            ContaFavorecidoNumero = linha.ExtrairLong(30, 41),
            ContaFavorecidoDV = linha.ExtrairAlfa(42, 42),
            DVAgenciaConta = linha.ExtrairAlfa(43, 43),
            NomeFavorecido = linha.ExtrairAlfa(44, 73),
            NumeroDoctoEmpresa = linha.ExtrairAlfa(74, 93),
            DataPagamento = linha.ExtrairData(94, 101),
            TipoMoeda = linha.ExtrairAlfa(102, 104),
            QuantidadeMoeda = linha.ExtrairDecimal(105, 119, 5),
            ValorPagamento = linha.ExtrairDecimal(120, 134, 2),
            NumeroDoctoAtribBanco = linha.ExtrairAlfa(135, 154),
            DataEfetivacao = linha.ExtrairData(155, 162),
            ValorEfetivacao = linha.ExtrairDecimal(163, 177, 2),
            InformacaoComplementar = linha.ExtrairAlfa(178, 217),
            FinalidadeTed = linha.ExtrairInt(218, 222),
            CNAB2 = linha.ExtrairAlfa(223, 229),
            CodigoAviso = linha.ExtrairInt(230, 230),
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
            "A",                                  // 05: 14-14 (1) fixo
            "".PadAlfa(1),                       // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2),    // 07: 16-17 (2)
            CamaraCentralizadora.PadNum(3),      // 08: 18-20 (3)
            BancoFavorecido.PadNum(3),           // 09: 21-23 (3)
            AgenciaFavorecidoCodigo.PadNum(5),   // 10: 24-28 (5)
            AgenciaFavorecidoDV.PadAlfa(1),      // 11: 29-29 (1)
            ContaFavorecidoNumero.PadNum(12),    // 12: 30-41 (12)
            ContaFavorecidoDV.PadAlfa(1),        // 13: 42-42 (1)
            DVAgenciaConta.PadAlfa(1),           // 14: 43-43 (1)
            NomeFavorecido.PadAlfa(30),          // 15: 44-73 (30)
            NumeroDoctoEmpresa.PadAlfa(20),      // 16: 74-93 (20)
            DataPagamento.FormatarData(),        // 17: 94-101 (8)
            TipoMoeda.PadAlfa(3),               // 18: 102-104 (3)
            QuantidadeMoeda.PadDecimal(15, 5),  // 19: 105-119 (15)
            ValorPagamento.PadDecimal(15, 2),   // 20: 120-134 (15)
            NumeroDoctoAtribBanco.PadAlfa(20),  // 21: 135-154 (20)
            DataEfetivacao.FormatarData(),      // 22: 155-162 (8)
            ValorEfetivacao.PadDecimal(15, 2),  // 23: 163-177 (15)
            InformacaoComplementar.PadAlfa(40), // 24: 178-217 (40)
            FinalidadeTed.PadNum(5),            // 25: 218-222 (5)
            "".PadAlfa(7),                      // 26: 223-229 (7) CNAB2 Brancos
            CodigoAviso.PadNum(1),              // 27: 230-230 (1)
            OcorrenciasRetorno.PadAlfa(10)      // 28: 231-240 (10)
        );
    // SOMA: 3+4+1+5+1+1+2+3+3+5+1+12+1+1+30+20+8+3+15+15+20+8+15+40+5+7+1+10 = 240 ✓
}
