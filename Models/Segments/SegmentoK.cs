using Cnab240.Enums;
using Cnab240.Extensions;

namespace Cnab240.Models.Segments;

/// <summary>Segmento K — Débito Automático (Detalhe Tipo 3, Segmento K).</summary>
public sealed record SegmentoK : RegistroCnab240
{
    /// <inheritdoc/>
    public override TipoRegistro TipoRegistro => TipoRegistro.Detalhe;

    /// <summary>Identificador do segmento.</summary>
    public char Segmento => 'K';

    /// <summary>Código do Banco — pos 1-3 (G001)</summary>
    public int BancoCodigo { get; init; }

    /// <summary>Lote de Serviço — pos 4-7 (G002)</summary>
    public int LoteServico { get; init; }

    /// <summary>Tipo de Registro — pos 8-8 (G003), fixo "3"</summary>
    // Fixed literal "3" in ToLinhaFormatada

    /// <summary>Número Sequencial do Registro no Lote — pos 9-13 (G038)</summary>
    public int NumeroSequencial { get; init; }

    /// <summary>Código do Segmento do Registro Detalhe — pos 14-14 (G039), literal "K"</summary>
    // Fixed literal "K" in ToLinhaFormatada

    /// <summary>Código de Instrução para Movimento — pos 15-16 (K001)</summary>
    public int CodigoInstrucaoMovimento { get; init; }

    /// <summary>Identificação da Ocorrência — pos 17-19 (K002)</summary>
    public int IdentificacaoOcorrencia { get; init; }

    /// <summary>Tipo de Inscrição do Comprador — pos 20-20 (G005)</summary>
    public int TipoInscricaoComprador { get; init; }

    /// <summary>Número de Inscrição do Comprador — pos 21-34 (G006)</summary>
    public long NumeroInscricaoComprador { get; init; }

    /// <summary>Nome do Comprador — pos 35-74 (K003)</summary>
    public string NomeComprador { get; init; } = "";

    /// <summary>Endereço do Comprador — pos 75-114 (K004)</summary>
    public string EnderecoComprador { get; init; } = "";

    /// <summary>Bairro do Comprador — pos 115-129 (K005)</summary>
    public string BairroComprador { get; init; } = "";

    /// <summary>CEP do Comprador — pos 130-134 (K006)</summary>
    public int CEPComprador { get; init; }

    /// <summary>Sufixo do CEP do Comprador — pos 135-137 (K007)</summary>
    public int SufixoCEPComprador { get; init; }

    /// <summary>Cidade do Comprador — pos 138-152 (K008)</summary>
    public string CidadeComprador { get; init; } = "";

    /// <summary>UF do Comprador — pos 153-154 (K009)</summary>
    public string UFComprador { get; init; } = "";

    /// <summary>Banco para Débito — pos 155-157 (G001)</summary>
    public int BancoDebito { get; init; }

    /// <summary>Agência para Débito — pos 158-162 (G008)</summary>
    public int AgenciaDebito { get; init; }

    /// <summary>Dígito Verificador da Agência para Débito — pos 163-163 (G009)</summary>
    public string DVAgenciaDebito { get; init; } = "";

    /// <summary>Conta Corrente para Débito — pos 164-175 (G010)</summary>
    public long ContaDebitoCorrente { get; init; }

    /// <summary>Dígito Verificador da Conta para Débito — pos 176-176 (G011)</summary>
    public string DVContaDebito { get; init; } = "";

    /// <summary>Dígito Verificador da Agência/Conta para Débito — pos 177-177 (G012)</summary>
    public string DVAgenciaContaDebito { get; init; } = "";

    /// <summary>Identificador do Título no Banco — pos 178-197 (K010)</summary>
    public long IdentificadorTituloBanco { get; init; }

    /// <summary>Atividade Social do Comprador — pos 198-203 (K011)</summary>
    public long AtividadeSocialComprador { get; init; }

    /// <summary>Código do Programa Operacional — pos 204-208 (K012)</summary>
    public string CodigoProgramaOperacional { get; init; } = "";

    /// <summary>Mensagem — pos 209-213 (K013)</summary>
    public string Mensagem { get; init; } = "";

    /// <summary>Identificador do Título na Empresa — pos 214-240 (K014)</summary>
    public string IdentificadorTituloEmpresa { get; init; } = "";

    /// <summary>Realiza o parse de uma linha CNAB 240 no formato Segmento K.</summary>
    public static SegmentoK Parse(string linha, int numeroLinha = 0)
    {
        linha.ValidarTamanho(numeroLinha);
        return new SegmentoK
        {
            BancoCodigo = linha.ExtrairInt(1, 3),
            LoteServico = linha.ExtrairInt(4, 7),
            NumeroSequencial = linha.ExtrairInt(9, 13),
            CodigoInstrucaoMovimento = linha.ExtrairInt(15, 16),
            IdentificacaoOcorrencia = linha.ExtrairInt(17, 19),
            TipoInscricaoComprador = linha.ExtrairInt(20, 20),
            NumeroInscricaoComprador = linha.ExtrairLong(21, 34),
            NomeComprador = linha.ExtrairAlfa(35, 74),
            EnderecoComprador = linha.ExtrairAlfa(75, 114),
            BairroComprador = linha.ExtrairAlfa(115, 129),
            CEPComprador = linha.ExtrairInt(130, 134),
            SufixoCEPComprador = linha.ExtrairInt(135, 137),
            CidadeComprador = linha.ExtrairAlfa(138, 152),
            UFComprador = linha.ExtrairAlfa(153, 154),
            BancoDebito = linha.ExtrairInt(155, 157),
            AgenciaDebito = linha.ExtrairInt(158, 162),
            DVAgenciaDebito = linha.ExtrairAlfa(163, 163),
            ContaDebitoCorrente = linha.ExtrairLong(164, 175),
            DVContaDebito = linha.ExtrairAlfa(176, 176),
            DVAgenciaContaDebito = linha.ExtrairAlfa(177, 177),
            IdentificadorTituloBanco = linha.ExtrairLong(178, 197),
            AtividadeSocialComprador = linha.ExtrairLong(198, 203),
            CodigoProgramaOperacional = linha.ExtrairAlfa(204, 208),
            Mensagem = linha.ExtrairAlfa(209, 213),
            IdentificadorTituloEmpresa = linha.ExtrairAlfa(214, 240),
        };
    }

    /// <summary>Serializa o registro em uma linha CNAB 240 com exatamente 240 caracteres.</summary>
    public override string ToLinhaFormatada()
        => string.Concat(
            BancoCodigo.PadNum(3),                   // 01: 1-3   (3)
            LoteServico.PadNum(4),                   // 02: 4-7   (4)
            "3",                                      // 03: 8-8   (1) fixo
            NumeroSequencial.PadNum(5),              // 04: 9-13  (5)
            "K",                                      // 05: 14-14 (1) fixo
            CodigoInstrucaoMovimento.PadNum(2),      // 06: 15-16 (2)
            IdentificacaoOcorrencia.PadNum(3),       // 07: 17-19 (3)
            TipoInscricaoComprador.PadNum(1),        // 08: 20-20 (1)
            NumeroInscricaoComprador.PadNum(14),     // 09: 21-34 (14)
            NomeComprador.PadAlfa(40),               // 10: 35-74 (40)
            EnderecoComprador.PadAlfa(40),           // 11: 75-114 (40)
            BairroComprador.PadAlfa(15),             // 12: 115-129 (15)
            CEPComprador.PadNum(5),                  // 13: 130-134 (5)
            SufixoCEPComprador.PadNum(3),            // 14: 135-137 (3)
            CidadeComprador.PadAlfa(15),             // 15: 138-152 (15)
            UFComprador.PadAlfa(2),                  // 16: 153-154 (2)
            BancoDebito.PadNum(3),                   // 17: 155-157 (3)
            AgenciaDebito.PadNum(5),                 // 18: 158-162 (5)
            DVAgenciaDebito.PadAlfa(1),              // 19: 163-163 (1)
            ContaDebitoCorrente.PadNum(12),          // 20: 164-175 (12)
            DVContaDebito.PadAlfa(1),                // 21: 176-176 (1)
            DVAgenciaContaDebito.PadAlfa(1),         // 22: 177-177 (1)
            IdentificadorTituloBanco.PadNum(20),     // 23: 178-197 (20)
            AtividadeSocialComprador.PadNum(6),      // 24: 198-203 (6)
            CodigoProgramaOperacional.PadAlfa(5),    // 25: 204-208 (5)
            Mensagem.PadAlfa(5),                     // 26: 209-213 (5)
            IdentificadorTituloEmpresa.PadAlfa(27)   // 27: 214-240 (27)
        );
    // SOMA: 3+4+1+5+1+2+3+1+14+40+40+15+5+3+15+2+3+5+1+12+1+1+20+6+5+5+27 = 240 ✓
}
