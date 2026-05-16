using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento Q — Pagador (Detalhe Tipo 3, Segmento Q).</summary>
public sealed record SegmentoQ : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'Q';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "Q"</summary>
    // Fixed literal "Q" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução (Remessa) — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Tipo de Inscrição do Pagador — pos 18-18 (G005)</summary>
    public int TipoInscricaoPagador { get; init; }

    /// <summary>Número de Inscrição do Pagador — pos 19-33 (G006)</summary>
    public long NumeroInscricaoPagador { get; init; }

    /// <summary>Nome do Pagador — pos 34-73 (Q001)</summary>
    public string NomePagador { get; init; } = "";

    /// <summary>Endereço do Pagador — pos 74-113 (Q002)</summary>
    public string EnderecoPagador { get; init; } = "";

    /// <summary>Bairro do Pagador — pos 114-128 (Q003)</summary>
    public string BairroPagador { get; init; } = "";

    /// <summary>CEP do Pagador — pos 129-133 (Q004)</summary>
    public int CEPPagador { get; init; }

    /// <summary>Sufixo do CEP do Pagador — pos 134-136 (Q005)</summary>
    public int SufixoCEPPagador { get; init; }

    /// <summary>Cidade do Pagador — pos 137-151 (Q006)</summary>
    public string CidadePagador { get; init; } = "";

    /// <summary>UF do Pagador — pos 152-153 (Q007)</summary>
    public string UFPagador { get; init; } = "";

    /// <summary>Tipo de Inscrição do Sacador/Avalista — pos 154-154 (G005)</summary>
    public int TipoInscricaoSacadorAvalista { get; init; }

    /// <summary>Número de Inscrição do Sacador/Avalista — pos 155-169 (G006)</summary>
    public long NumeroInscricaoSacadorAvalista { get; init; }

    /// <summary>Nome do Sacador/Avalista — pos 170-209 (Q008)</summary>
    public string NomeSacadorAvalista { get; init; } = "";

    /// <summary>Código do Banco Correspondente — pos 210-212 (Q009)</summary>
    public int CodigoBancoCorrespondente { get; init; }

    /// <summary>Nosso Número no Banco Correspondente — pos 213-232 (Q010)</summary>
    public string NossoNumeroBancoCorrespondente { get; init; } = "";

    /// <summary>Uso Reservado CNAB 2 — pos 233-240 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento Q.</summary>
    public static SegmentoQ Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoQ
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CNAB = linha.ExtrairAlfa(15, 15),
            CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
            TipoInscricaoPagador = linha.ExtrairInt(18, 18),
            NumeroInscricaoPagador = linha.ExtrairLong(19, 33),
            NomePagador = linha.ExtrairAlfa(34, 73),
            EnderecoPagador = linha.ExtrairAlfa(74, 113),
            BairroPagador = linha.ExtrairAlfa(114, 128),
            CEPPagador = linha.ExtrairInt(129, 133),
            SufixoCEPPagador = linha.ExtrairInt(134, 136),
            CidadePagador = linha.ExtrairAlfa(137, 151),
            UFPagador = linha.ExtrairAlfa(152, 153),
            TipoInscricaoSacadorAvalista = linha.ExtrairInt(154, 154),
            NumeroInscricaoSacadorAvalista = linha.ExtrairLong(155, 169),
            NomeSacadorAvalista = linha.ExtrairAlfa(170, 209),
            CodigoBancoCorrespondente = linha.ExtrairInt(210, 212),
            NossoNumeroBancoCorrespondente = linha.ExtrairAlfa(213, 232),
            CNAB2 = linha.ExtrairAlfa(233, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                          // 01: 1-3   (3)
            LoteServico.PadNum(4),                          // 02: 4-7   (4)
            "3",                                             // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),                     // 04: 9-13  (5)
            "Q",                                             // 05: 14-14 (1) fixo
            "".PadAlfa(1),                                  // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2),               // 07: 16-17 (2)
            TipoInscricaoPagador.PadNum(1),                 // 08: 18-18 (1)
            NumeroInscricaoPagador.PadNum(15),              // 09: 19-33 (15)
            NomePagador.PadAlfa(40),                        // 10: 34-73 (40)
            EnderecoPagador.PadAlfa(40),                    // 11: 74-113 (40)
            BairroPagador.PadAlfa(15),                      // 12: 114-128 (15)
            CEPPagador.PadNum(5),                           // 13: 129-133 (5)
            SufixoCEPPagador.PadNum(3),                     // 14: 134-136 (3)
            CidadePagador.PadAlfa(15),                      // 15: 137-151 (15)
            UFPagador.PadAlfa(2),                           // 16: 152-153 (2)
            TipoInscricaoSacadorAvalista.PadNum(1),         // 17: 154-154 (1)
            NumeroInscricaoSacadorAvalista.PadNum(15),      // 18: 155-169 (15)
            NomeSacadorAvalista.PadAlfa(40),                // 19: 170-209 (40)
            CodigoBancoCorrespondente.PadNum(3),            // 20: 210-212 (3)
            NossoNumeroBancoCorrespondente.PadAlfa(20),     // 21: 213-232 (20)
            "".PadAlfa(8)                                   // 22: 233-240 (8) CNAB2 Brancos
        );
    // SOMA: 3+4+1+5+1+1+2+1+15+40+40+15+5+3+15+2+1+15+40+3+20+8 = 240 ✓
}
