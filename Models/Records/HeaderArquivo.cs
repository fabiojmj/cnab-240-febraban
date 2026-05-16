using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Records;

/// <summary>Header do Arquivo CNAB 240 (Tipo 0).</summary>
public sealed record HeaderArquivo : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.HeaderArquivo;

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002), fixo "0000"</summary>
    public int LoteServico { get; init; } = 0;

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "0"</summary>
    // Fixed literal "0" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 9-17 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Tipo de Inscrição da Empresa — pos 18-18 (G005)</summary>
    public int TipoInscricaoEmpresa { get; init; }

    /// <summary>Número de Inscrição da Empresa (CNPJ/CPF) — pos 19-32 (G006)</summary>
    public long NumeroInscricaoEmpresa { get; init; }

    /// <summary>Código do Convênio no Banco — pos 33-52 (G007)</summary>
    public string CodigoConvenio { get; init; } = "";

    /// <summary>Agência Mantenedora da Conta — pos 53-57 (G008)</summary>
    public int AgenciaCodigo { get; init; }

    /// <summary>Dígito Verificador da Agência — pos 58-58 (G009)</summary>
    public string AgenciaDV { get; init; } = "";

    /// <summary>Número da Conta Corrente — pos 59-70 (G010)</summary>
    public long ContaNumero { get; init; }

    /// <summary>Dígito Verificador da Conta — pos 71-71 (G011)</summary>
    public string ContaDV { get; init; } = "";

    /// <summary>Dígito Verificador da Agência/Conta — pos 72-72 (G012)</summary>
    public string DVAgenciaConta { get; init; } = "";

    /// <summary>Nome da Empresa — pos 73-102 (G013)</summary>
    public string NomeEmpresa { get; init; } = "";

    /// <summary>Nome do Banco — pos 103-132 (G014)</summary>
    public string NomeBanco { get; init; } = "";

    /// <summary>Uso Reservado CNAB 2 — pos 133-142 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Código do Arquivo — pos 143-143 (G015). 1=Remessa, 2=Retorno</summary>
    public int CodigoArquivo { get; init; } = 1;

    /// <summary>Data de Geração do Arquivo — pos 144-151 (G016)</summary>
    public DateOnly? DataGeracao { get; init; }

    /// <summary>Hora de Geração do Arquivo — pos 152-157 (G017)</summary>
    public TimeOnly? HoraGeracao { get; init; }

    /// <summary>Número Sequencial do Arquivo — pos 158-163 (G018)</summary>
    public int NumeroSequencialArquivo { get; init; }

    /// <summary>Versão do Layout do Arquivo — pos 164-166 (G019)</summary>
    public string VersaoLayoutArquivo { get; init; } = "103";

    /// <summary>Densidade de Gravação do Arquivo — pos 167-171 (G020)</summary>
    public int DensidadeGravacao { get; init; }

    /// <summary>Reservado para Uso do Banco — pos 172-191 (G021), Brancos</summary>
    public string ReservadoBanco { get; init; } = "";

    /// <summary>Reservado para Uso da Empresa — pos 192-211 (G022), Brancos</summary>
    public string ReservadoEmpresa { get; init; } = "";

    /// <summary>Uso Reservado CNAB 3 — pos 212-240 (G004), Brancos</summary>
    public string CNAB3 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Header do Arquivo.</summary>
    public static HeaderArquivo Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new HeaderArquivo
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            CNAB = linha.ExtrairAlfa(9, 17),
            TipoInscricaoEmpresa = linha.ExtrairInt(18, 18),
            NumeroInscricaoEmpresa = linha.ExtrairLong(19, 32),
            CodigoConvenio = linha.ExtrairAlfa(33, 52),
            AgenciaCodigo = linha.ExtrairInt(53, 57),
            AgenciaDV = linha.ExtrairAlfa(58, 58),
            ContaNumero = linha.ExtrairLong(59, 70),
            ContaDV = linha.ExtrairAlfa(71, 71),
            DVAgenciaConta = linha.ExtrairAlfa(72, 72),
            NomeEmpresa = linha.ExtrairAlfa(73, 102),
            NomeBanco = linha.ExtrairAlfa(103, 132),
            CNAB2 = linha.ExtrairAlfa(133, 142),
            CodigoArquivo = linha.ExtrairInt(143, 143),
            DataGeracao = linha.ExtrairData(144, 151),
            HoraGeracao = linha.ExtrairHora(152, 157),
            NumeroSequencialArquivo = linha.ExtrairInt(158, 163),
            VersaoLayoutArquivo = linha.ExtrairAlfa(164, 166),
            DensidadeGravacao = linha.ExtrairInt(167, 171),
            ReservadoBanco = linha.ExtrairAlfa(172, 191),
            ReservadoEmpresa = linha.ExtrairAlfa(192, 211),
            CNAB3 = linha.ExtrairAlfa(212, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),           // 01: 1-3   (3)
            "0000",                           // 02: 4-7   (4) fixo
            "0",                              // 03: 8-8   (1) fixo
            "".PadAlfa(9),                    // 04: 9-17  (9) CNAB Brancos
            TipoInscricaoEmpresa.PadNum(1),  // 05: 18-18 (1)
            NumeroInscricaoEmpresa.PadNum(14),// 06: 19-32 (14)
            CodigoConvenio.PadAlfa(20),      // 07: 33-52 (20)
            AgenciaCodigo.PadNum(5),         // 08: 53-57 (5)
            AgenciaDV.PadAlfa(1),            // 09: 58-58 (1)
            ContaNumero.PadNum(12),          // 10: 59-70 (12)
            ContaDV.PadAlfa(1),              // 11: 71-71 (1)
            DVAgenciaConta.PadAlfa(1),       // 12: 72-72 (1)
            NomeEmpresa.PadAlfa(30),         // 13: 73-102 (30)
            NomeBanco.PadAlfa(30),           // 14: 103-132 (30)
            "".PadAlfa(10),                  // 15: 133-142 (10) CNAB2 Brancos
            CodigoArquivo.PadNum(1),         // 16: 143-143 (1)
            DataGeracao.FormatarData(),      // 17: 144-151 (8)
            HoraGeracao.FormatarHora(),      // 18: 152-157 (6)
            NumeroSequencialArquivo.PadNum(6),// 19: 158-163 (6)
            VersaoLayoutArquivo.PadAlfa(3),  // 20: 164-166 (3)
            DensidadeGravacao.PadNum(5),     // 21: 167-171 (5)
            "".PadAlfa(20),                  // 22: 172-191 (20) ReservadoBanco Brancos
            "".PadAlfa(20),                  // 23: 192-211 (20) ReservadoEmpresa Brancos
            "".PadAlfa(29)                   // 24: 212-240 (29) CNAB3 Brancos
        );
    // SOMA: 3+4+1+9+1+14+20+5+1+12+1+1+30+30+10+1+8+6+6+3+5+20+20+29 = 240 ✓
}
