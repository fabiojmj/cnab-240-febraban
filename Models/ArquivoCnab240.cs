using Cnab240.Models.Records;
using Cnab240.Models.Segments;

namespace Cnab240.Models;

public sealed record ArquivoCnab240
{
    public HeaderArquivo Header { get; init; } = new();
    public IReadOnlyList<LoteServico> Lotes { get; init; } = [];
    public TrailerArquivo Trailer { get; init; } = new();
}

public sealed record LoteServico
{
    public HeaderLote Header { get; init; } = new();
    public IReadOnlyList<DetalheRegistro> Detalhes { get; init; } = [];
    public TrailerLote Trailer { get; init; } = new();
}

public sealed record DetalheRegistro
{
    public SegmentoA? SegmentoA { get; init; }
    public SegmentoB? SegmentoB { get; init; }
    public SegmentoC? SegmentoC { get; init; }
    public SegmentoD? SegmentoD { get; init; }
    public SegmentoE? SegmentoE { get; init; }
    public SegmentoF? SegmentoF { get; init; }
    public SegmentoG? SegmentoG { get; init; }
    public SegmentoH? SegmentoH { get; init; }
    public SegmentoI? SegmentoI { get; init; }
    public SegmentoJ? SegmentoJ { get; init; }
    public SegmentoK? SegmentoK { get; init; }
    public SegmentoN? SegmentoN { get; init; }
    public SegmentoO? SegmentoO { get; init; }
    public SegmentoP? SegmentoP { get; init; }
    public SegmentoQ? SegmentoQ { get; init; }
    public SegmentoR? SegmentoR { get; init; }
    public SegmentoS? SegmentoS { get; init; }
    public SegmentoT? SegmentoT { get; init; }
    public SegmentoU? SegmentoU { get; init; }
    public SegmentoW? SegmentoW { get; init; }
    public SegmentoZ? SegmentoZ { get; init; }
    public IReadOnlyList<string> LinhasNaoMapeadas { get; init; } = [];
}
