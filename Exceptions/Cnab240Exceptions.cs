namespace Cnab240.Exceptions;

public class CnabTamanhoInvalidoException(int numeroLinha, int tamanhoAtual)
    : Exception($"Linha {numeroLinha}: tamanho {tamanhoAtual}, esperado 240.");

public class CnabSegmentoNaoReconhecidoException(char segmento)
    : Exception($"Segmento '{segmento}' não reconhecido.");
