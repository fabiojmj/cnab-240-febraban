using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Records;

/// <summary>Header do Lote CNAB 240 (Tipo 1).</summary>
public sealed record HeaderLote : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.HeaderLote;

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "1"</summary>
    // Fixed literal "1" in ToLinhaFormatada

    /// <summary>Tipo de Operação — pos 9-9 (G014). C=Crédito, D=Débito, E=Extrato</summary>
    public string TipoOperacao { get; init; } = "C";

    /// <summary>Tipo de Serviço — pos 10-11 (G025)</summary>
    public int TipoServico { get; init; }

    /// <summary>Uso Reservado CNAB — pos 12-13 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Versão do Layout do Lote — pos 14-16 (G030)</summary>
    public string VersaoLayoutLote { get; init; } = "045";

    /// <summary>Uso Reservado CNAB 2 — pos 17-17 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

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

    /// <summary>Mensagem/Finalidade do Lote — pos 103-142 (G028)</summary>
    public string MensagemFinalidade { get; init; } = "";

    /// <summary>Histórico de CC — pos 143-172 (G029)</summary>
    public string ReservadoEmpresa { get; init; } = "";

    /// <summary>Número da Remessa — pos 173-177 (G079)</summary>
    public int NumeroRemessa { get; init; }

    /// <summary>Data de Gravação do Arquivo — pos 178-185 (G083)</summary>
    public DateOnly? DataGravacao { get; init; }

    /// <summary>Data do Crédito — pos 186-193 (G032)</summary>
    public DateOnly? DataCredito { get; init; }

    /// <summary>Uso Reservado CNAB 3 — pos 194-240 (G004), Brancos</summary>
    public string CNAB3 { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Header do Lote.</summary>
    public static HeaderLote Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new HeaderLote
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            TipoOperacao = linha.ExtrairAlfa(9, 9),
            TipoServico = linha.ExtrairInt(10, 11),
            CNAB = linha.ExtrairAlfa(12, 13),
            VersaoLayoutLote = linha.ExtrairAlfa(14, 16),
            CNAB2 = linha.ExtrairAlfa(17, 17),
            TipoInscricaoEmpresa = linha.ExtrairInt(18, 18),
            NumeroInscricaoEmpresa = linha.ExtrairLong(19, 32),
            CodigoConvenio = linha.ExtrairAlfa(33, 52),
            AgenciaCodigo = linha.ExtrairInt(53, 57),
            AgenciaDV = linha.ExtrairAlfa(58, 58),
            ContaNumero = linha.ExtrairLong(59, 70),
            ContaDV = linha.ExtrairAlfa(71, 71),
            DVAgenciaConta = linha.ExtrairAlfa(72, 72),
            NomeEmpresa = linha.ExtrairAlfa(73, 102),
            MensagemFinalidade = linha.ExtrairAlfa(103, 142),
            ReservadoEmpresa = linha.ExtrairAlfa(143, 172),
            NumeroRemessa = linha.ExtrairInt(173, 177),
            DataGravacao = linha.ExtrairData(178, 185),
            DataCredito = linha.ExtrairData(186, 193),
            CNAB3 = linha.ExtrairAlfa(194, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),             // 01: 1-3   (3)
            LoteServico.PadNum(4),             // 02: 4-7   (4)
            "1",                               // 03: 8-8   (1) fixo
            TipoOperacao.PadAlfa(1),           // 04: 9-9   (1)
            TipoServico.PadNum(2),             // 05: 10-11 (2)
            "".PadAlfa(2),                     // 06: 12-13 (2) CNAB Brancos
            VersaoLayoutLote.PadAlfa(3),       // 07: 14-16 (3)
            "".PadAlfa(1),                     // 08: 17-17 (1) CNAB2 Brancos
            TipoInscricaoEmpresa.PadNum(1),   // 09: 18-18 (1)
            NumeroInscricaoEmpresa.PadNum(14), // 10: 19-32 (14)
            CodigoConvenio.PadAlfa(20),       // 11: 33-52 (20)
            AgenciaCodigo.PadNum(5),          // 12: 53-57 (5)
            AgenciaDV.PadAlfa(1),             // 13: 58-58 (1)
            ContaNumero.PadNum(12),           // 14: 59-70 (12)
            ContaDV.PadAlfa(1),               // 15: 71-71 (1)
            DVAgenciaConta.PadAlfa(1),        // 16: 72-72 (1)
            NomeEmpresa.PadAlfa(30),          // 17: 73-102 (30)
            MensagemFinalidade.PadAlfa(40),   // 18: 103-142 (40)
            ReservadoEmpresa.PadAlfa(30),     // 19: 143-172 (30)
            NumeroRemessa.PadNum(5),          // 20: 173-177 (5)
            DataGravacao.FormatarData(),      // 21: 178-185 (8)
            DataCredito.FormatarData(),       // 22: 186-193 (8)
            "".PadAlfa(47)                    // 23: 194-240 (47) CNAB3 Brancos
        );
    // SOMA: 3+4+1+1+2+2+3+1+1+14+20+5+1+12+1+1+30+40+30+5+8+8+47 = 240 ✓
}

/// <summary>Trailer do Lote CNAB 240 (Tipo 5).</summary>
public sealed record TrailerLote : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.TrailerLote;

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "5"</summary>
    // Fixed literal "5" in ToLinhaFormatada

    /// <summary>Uso Reservado CNAB — pos 9-17 (G004), Brancos</summary>
    public string CNAB { get; init; } = "";

    /// <summary>Quantidade de Registros do Lote — pos 18-23 (G057)</summary>
    public int QtdeRegistros { get; init; }

    /// <summary>Somatória dos Valores — pos 24-41 (G058), 2 decimais</summary>
    public decimal SomatorioValores { get; init; }

    /// <summary>Somatória de Quantidade de Moedas — pos 42-59 (G059), 5 decimais</summary>
    public decimal SomatorioQtdeMoeda { get; init; }

    /// <summary>Número do Aviso de Lançamento — pos 60-65 (G060)</summary>
    public int NumeroAviso { get; init; }

    /// <summary>Uso Reservado CNAB 2 — pos 66-230 (G004), Brancos</summary>
    public string CNAB2 { get; init; } = "";

    /// <summary>Ocorrências de Retorno — pos 231-240 (G061)</summary>
    public string OcorrenciasRetorno { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Trailer do Lote.</summary>
    public static TrailerLote Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new TrailerLote
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            CNAB = linha.ExtrairAlfa(9, 17),
            QtdeRegistros = linha.ExtrairInt(18, 23),
            SomatorioValores = linha.ExtrairDecimal(24, 41, 2),
            SomatorioQtdeMoeda = linha.ExtrairDecimal(42, 59, 5),
            NumeroAviso = linha.ExtrairInt(60, 65),
            CNAB2 = linha.ExtrairAlfa(66, 230),
            OcorrenciasRetorno = linha.ExtrairAlfa(231, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                    // 01: 1-3   (3)
            LoteServico.PadNum(4),                    // 02: 4-7   (4)
            "5",                                       // 03: 8-8   (1) fixo
            "".PadAlfa(9),                            // 04: 9-17  (9) CNAB Brancos
            QtdeRegistros.PadNum(6),                  // 05: 18-23 (6)
            SomatorioValores.PadDecimal(18, 2),       // 06: 24-41 (18)
            SomatorioQtdeMoeda.PadDecimal(18, 5),     // 07: 42-59 (18)
            NumeroAviso.PadNum(6),                    // 08: 60-65 (6)
            "".PadAlfa(165),                          // 09: 66-230 (165) CNAB2 Brancos
            OcorrenciasRetorno.PadAlfa(10)            // 10: 231-240 (10)
        );
    // SOMA: 3+4+1+9+6+18+18+6+165+10 = 240 ✓
}
