using Cnab240.Models;

namespace Cnab240.Parsers;

public static class Cnab240Writer
{
    public static string GerarConteudo(ArquivoCnab240 arquivo)
        => string.Join("\r\n", GerarLinhas(arquivo)) + "\r\n";

    private static IEnumerable<string> GerarLinhas(ArquivoCnab240 arquivo)
    {
        yield return arquivo.Header.ToLinhaFormatada();
        foreach (var lote in arquivo.Lotes)
        {
            yield return lote.Header.ToLinhaFormatada();
            foreach (var detalhe in lote.Detalhes)
            {
                if (detalhe.SegmentoA is not null) yield return detalhe.SegmentoA.ToLinhaFormatada();
                if (detalhe.SegmentoB is not null) yield return detalhe.SegmentoB.ToLinhaFormatada();
                if (detalhe.SegmentoC is not null) yield return detalhe.SegmentoC.ToLinhaFormatada();
                if (detalhe.SegmentoD is not null) yield return detalhe.SegmentoD.ToLinhaFormatada();
                if (detalhe.SegmentoE is not null) yield return detalhe.SegmentoE.ToLinhaFormatada();
                if (detalhe.SegmentoF is not null) yield return detalhe.SegmentoF.ToLinhaFormatada();
                if (detalhe.SegmentoG is not null) yield return detalhe.SegmentoG.ToLinhaFormatada();
                if (detalhe.SegmentoH is not null) yield return detalhe.SegmentoH.ToLinhaFormatada();
                if (detalhe.SegmentoI is not null) yield return detalhe.SegmentoI.ToLinhaFormatada();
                if (detalhe.SegmentoJ is not null) yield return detalhe.SegmentoJ.ToLinhaFormatada();
                if (detalhe.SegmentoK is not null) yield return detalhe.SegmentoK.ToLinhaFormatada();
                if (detalhe.SegmentoN is not null) yield return detalhe.SegmentoN.ToLinhaFormatada();
                if (detalhe.SegmentoO is not null) yield return detalhe.SegmentoO.ToLinhaFormatada();
                if (detalhe.SegmentoP is not null) yield return detalhe.SegmentoP.ToLinhaFormatada();
                if (detalhe.SegmentoQ is not null) yield return detalhe.SegmentoQ.ToLinhaFormatada();
                if (detalhe.SegmentoR is not null) yield return detalhe.SegmentoR.ToLinhaFormatada();
                if (detalhe.SegmentoS is not null) yield return detalhe.SegmentoS.ToLinhaFormatada();
                if (detalhe.SegmentoT is not null) yield return detalhe.SegmentoT.ToLinhaFormatada();
                if (detalhe.SegmentoU is not null) yield return detalhe.SegmentoU.ToLinhaFormatada();
                if (detalhe.SegmentoW is not null) yield return detalhe.SegmentoW.ToLinhaFormatada();
                if (detalhe.SegmentoZ is not null) yield return detalhe.SegmentoZ.ToLinhaFormatada();
                foreach (var raw in detalhe.LinhasNaoMapeadas) yield return raw;
            }
            yield return lote.Trailer.ToLinhaFormatada();
        }
        yield return arquivo.Trailer.ToLinhaFormatada();
    }
}
