using Cnab240.Extensions;
using Cnab240.Models;
using Cnab240.Models.Records;
using Cnab240.Models.Segments;

namespace Cnab240.Parsers;

public static class Cnab240Parser
{
    public static ArquivoCnab240 Parse(string conteudo)
    {
        var linhas = conteudo.Split('\n')
            .Select(l => l.TrimEnd('\r'))
            .Where(l => l.Length > 0)
            .ToList();

        HeaderArquivo? header = null;
        TrailerArquivo? trailer = null;
        var lotes = new List<LoteServico>();
        HeaderLote? headerLote = null;
        TrailerLote? trailerLote = null;
        var detalhes = new List<DetalheRegistro>();
        int numero = 0;

        foreach (var linha in linhas)
        {
            numero++;
            if (linha.Length < 8) continue;
            char tipo = linha[7];
            switch (tipo)
            {
                case '0':
                    header = HeaderArquivo.Parse(linha, numero);
                    break;
                case '1':
                    headerLote = HeaderLote.Parse(linha, numero);
                    detalhes = [];
                    break;
                case '3':
                    var detalhe = AplicarSegmento(new DetalheRegistro(), linha, numero);
                    detalhes.Add(detalhe);
                    break;
                case '5':
                    trailerLote = TrailerLote.Parse(linha, numero);
                    if (headerLote is not null)
                        lotes.Add(new LoteServico { Header = headerLote, Detalhes = detalhes, Trailer = trailerLote });
                    break;
                case '9':
                    trailer = TrailerArquivo.Parse(linha, numero);
                    break;
            }
        }

        return new ArquivoCnab240
        {
            Header = header ?? new HeaderArquivo(),
            Lotes = lotes,
            Trailer = trailer ?? new TrailerArquivo()
        };
    }

    private static DetalheRegistro AplicarSegmento(DetalheRegistro detalhe, string linha, int numero)
    {
        char seg = linha.ExtrairSegmento();
        return seg switch
        {
            'A' => detalhe with { SegmentoA = SegmentoA.Parse(linha, numero) },
            'B' => detalhe with { SegmentoB = SegmentoB.Parse(linha, numero) },
            'C' => detalhe with { SegmentoC = SegmentoC.Parse(linha, numero) },
            'D' => detalhe with { SegmentoD = SegmentoD.Parse(linha, numero) },
            'E' => detalhe with { SegmentoE = SegmentoE.Parse(linha, numero) },
            'F' => detalhe with { SegmentoF = SegmentoF.Parse(linha, numero) },
            'G' => detalhe with { SegmentoG = SegmentoG.Parse(linha, numero) },
            'H' => detalhe with { SegmentoH = SegmentoH.Parse(linha, numero) },
            'I' => detalhe with { SegmentoI = SegmentoI.Parse(linha, numero) },
            'J' => detalhe with { SegmentoJ = SegmentoJ.Parse(linha, numero) },
            'K' => detalhe with { SegmentoK = SegmentoK.Parse(linha, numero) },
            'N' => detalhe with { SegmentoN = SegmentoN.Parse(linha, numero) },
            'O' => detalhe with { SegmentoO = SegmentoO.Parse(linha, numero) },
            'P' => detalhe with { SegmentoP = SegmentoP.Parse(linha, numero) },
            'Q' => detalhe with { SegmentoQ = SegmentoQ.Parse(linha, numero) },
            'R' => detalhe with { SegmentoR = SegmentoR.Parse(linha, numero) },
            'S' => detalhe with { SegmentoS = SegmentoS.Parse(linha, numero) },
            'T' => detalhe with { SegmentoT = SegmentoT.Parse(linha, numero) },
            'U' => detalhe with { SegmentoU = SegmentoU.Parse(linha, numero) },
            'W' => detalhe with { SegmentoW = SegmentoW.Parse(linha, numero) },
            'Z' => detalhe with { SegmentoZ = SegmentoZ.Parse(linha, numero) },
            _ => detalhe with { LinhasNaoMapeadas = [.. detalhe.LinhasNaoMapeadas, linha] }
        };
    }
}
