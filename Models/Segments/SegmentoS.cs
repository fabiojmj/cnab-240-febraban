using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento S — Impressão de Boletos (Detalhe Tipo 3, Segmento S). Dois layouts conforme TipoImpressao.</summary>
public sealed record SegmentoS : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'S';

    // ── Campos comuns (pos 1-18, 18 bytes) ──

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "S"</summary>
    // Fixed literal "S" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 15-15 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Código de Movimento/Instrução (Remessa) — pos 16-17 (G061)</summary>
    public int CodigoMovimentoRemessa { get; init; }

    /// <summary>Tipo de Impressão — pos 18-18 (S001). 1 ou 2 = layout mensagem; 3 = layout mensagens múltiplas</summary>
    public int TipoImpressao { get; init; }

    // ── Campos Layout 1/2 (TipoImpressao = 1 ou 2) ──

    /// <summary>Número da Linha — pos 19-20 (S002). Válido para TipoImpressao = 1 ou 2</summary>
    public int? NumeroLinha { get; init; }

    /// <summary>Mensagem de Impressão — pos 21-160 (S003). Válido para TipoImpressao = 1 ou 2</summary>
    public string? MensagemImpressao { get; init; }

    /// <summary>Tipo de Caracter — pos 161-162 (S004). Válido para TipoImpressao = 1 ou 2</summary>
    public int? TipoCaracter { get; init; }

    // ── Campos Layout 3 (TipoImpressao = 3) ──

    /// <summary>Mensagem 5 — pos 19-58 (S005). Válido para TipoImpressao = 3</summary>
    public string? Mensagem5 { get; init; }

    /// <summary>Mensagem 6 — pos 59-98 (S006). Válido para TipoImpressao = 3</summary>
    public string? Mensagem6 { get; init; }

    /// <summary>Mensagem 7 — pos 99-138 (S007). Válido para TipoImpressao = 3</summary>
    public string? Mensagem7 { get; init; }

    /// <summary>Mensagem 8 — pos 139-178 (S008). Válido para TipoImpressao = 3</summary>
    public string? Mensagem8 { get; init; }

    /// <summary>Mensagem 9 — pos 179-218 (S009). Válido para TipoImpressao = 3</summary>
    public string? Mensagem9 { get; init; }

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento S.</summary>
    public static SegmentoS Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        var tipoImpressao = linha.ExtrairInt(18, 18);

        if (tipoImpressao == 3)
        {
            return new SegmentoS
            {
                BancoCodigo = linha.ExtrairInt(1, 3),
                LoteServico = linha.ExtrairInt(4, 7),
                NumeroSequencial = linha.ExtrairInt(9, 13),
                CNAB = linha.ExtrairAlfa(15, 15),
                CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
                TipoImpressao = tipoImpressao,
                Mensagem5 = linha.ExtrairAlfa(19, 58),
                Mensagem6 = linha.ExtrairAlfa(59, 98),
                Mensagem7 = linha.ExtrairAlfa(99, 138),
                Mensagem8 = linha.ExtrairAlfa(139, 178),
                Mensagem9 = linha.ExtrairAlfa(179, 218),
            };
        }
        else
        {
            return new SegmentoS
            {
                BancoCodigo = linha.ExtrairInt(1, 3),
                LoteServico = linha.ExtrairInt(4, 7),
                NumeroSequencial = linha.ExtrairInt(9, 13),
                CNAB = linha.ExtrairAlfa(15, 15),
                CodigoMovimentoRemessa = linha.ExtrairInt(16, 17),
                TipoImpressao = tipoImpressao,
                NumeroLinha = linha.ExtrairInt(19, 20),
                MensagemImpressao = linha.ExtrairAlfa(21, 160),
                TipoCaracter = linha.ExtrairInt(161, 162),
            };
        }
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
    {
        var cabecalho = string.Concat(
            BancoCodigo.PadNum(3),            // 01: 1-3   (3)
            LoteServico.PadNum(4),            // 02: 4-7   (4)
            "3",                               // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),       // 04: 9-13  (5)
            "S",                               // 05: 14-14 (1) fixo
            "".PadAlfa(1),                    // 06: 15-15 (1) CNAB Brancos
            CodigoMovimentoRemessa.PadNum(2), // 07: 16-17 (2)
            TipoImpressao.PadNum(1)           // 08: 18-18 (1)
        ); // 18 chars

        if (TipoImpressao == 3)
        {
            return cabecalho + string.Concat(
                (Mensagem5 ?? "").PadAlfa(40),  // 19-58  (40)
                (Mensagem6 ?? "").PadAlfa(40),  // 59-98  (40)
                (Mensagem7 ?? "").PadAlfa(40),  // 99-138 (40)
                (Mensagem8 ?? "").PadAlfa(40),  // 139-178 (40)
                (Mensagem9 ?? "").PadAlfa(40),  // 179-218 (40)
                "".PadAlfa(22)                  // 219-240 (22) CNAB3 Brancos
            );
        }
        else
        {
            return cabecalho + string.Concat(
                (NumeroLinha ?? 0).PadNum(2),                    // 19-20  (2)
                (MensagemImpressao ?? "").PadAlfa(140),          // 21-160 (140)
                (TipoCaracter ?? 0).PadNum(2),                   // 161-162 (2)
                "".PadAlfa(78)                                    // 163-240 (78) CNAB2 Brancos
            );
        }
    }
    // Layout 1/2: 18 (comum) + 2+140+2+78 = 240 ✓
    // Layout 3:   18 (comum) + 40+40+40+40+40+22 = 240 ✓
}
