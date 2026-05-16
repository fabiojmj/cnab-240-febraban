using Cnab240.Models;

namespace Cnab240.Services;

public static class Cnab240Validator
{
    public static IReadOnlyList<string> Validar(ArquivoCnab240 arquivo)
    {
        var erros = new List<string>();

        foreach (var (lote, li) in arquivo.Lotes.Select((l, i) => (l, i)))
        {
            int qtdeDetalhes = lote.Detalhes.Sum(d =>
                (d.SegmentoA is not null ? 1 : 0) + (d.SegmentoB is not null ? 1 : 0) +
                (d.SegmentoC is not null ? 1 : 0) + (d.SegmentoJ is not null ? 1 : 0) +
                d.LinhasNaoMapeadas.Count);
            int qtdeRegistros = qtdeDetalhes + 2; // header + trailer
            if (lote.Trailer.QtdeRegistros != 0 && lote.Trailer.QtdeRegistros != qtdeRegistros)
                erros.Add($"Lote {li + 1}: QtdeRegistros esperado {qtdeRegistros}, encontrado {lote.Trailer.QtdeRegistros}");
        }

        return erros;
    }
}
