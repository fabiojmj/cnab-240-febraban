using Cnab240.Exceptions;

namespace Cnab240.Extensions;

public static class CnabStringExtensions
{
    /// <summary>Valida se a linha possui exatamente 240 caracteres.</summary>
    public static void ValidarTamanho(this string linha, int numeroLinha)
    {
        if (linha.Length != 240)
            throw new CnabTamanhoInvalidoException(numeroLinha, linha.Length);
    }

    /// <summary>Extrai inteiro da posição de (1-based) até ate (1-based).</summary>
    public static int ExtrairInt(this string linha, int de, int ate)
    {
        var s = linha.Substring(de - 1, ate - de + 1).Trim();
        if (string.IsNullOrEmpty(s)) return 0;
        return int.TryParse(s, out var v) ? v : 0;
    }

    /// <summary>Extrai long da posição de (1-based) até ate (1-based).</summary>
    public static long ExtrairLong(this string linha, int de, int ate)
    {
        var s = linha.Substring(de - 1, ate - de + 1).Trim();
        if (string.IsNullOrEmpty(s)) return 0L;
        return long.TryParse(s, out var v) ? v : 0L;
    }

    /// <summary>Extrai string alfanumérica da posição de (1-based) até ate (1-based).</summary>
    public static string ExtrairAlfa(this string linha, int de, int ate)
        => linha.Substring(de - 1, ate - de + 1).Trim();

    /// <summary>Extrai decimal da posição de (1-based) até ate (1-based) com dec casas decimais.</summary>
    public static decimal ExtrairDecimal(this string linha, int de, int ate, int dec)
    {
        var s = linha.Substring(de - 1, ate - de + 1).Trim();
        if (string.IsNullOrEmpty(s)) return 0m;
        if (!long.TryParse(s, out var raw)) return 0m;
        return raw / (decimal)Math.Pow(10, dec);
    }

    /// <summary>Extrai DateOnly da posição de (1-based) até ate (1-based). Retorna null se "00000000".</summary>
    public static DateOnly? ExtrairData(this string linha, int de, int ate)
    {
        var s = linha.Substring(de - 1, ate - de + 1).Trim();
        if (string.IsNullOrEmpty(s) || s == "00000000") return null;
        if (DateOnly.TryParseExact(s, "ddMMyyyy", out var d)) return d;
        return null;
    }

    /// <summary>Extrai TimeOnly da posição de (1-based) até ate (1-based). Retorna null se "000000".</summary>
    public static TimeOnly? ExtrairHora(this string linha, int de, int ate)
    {
        var s = linha.Substring(de - 1, ate - de + 1).Trim();
        if (string.IsNullOrEmpty(s) || s == "000000") return null;
        if (TimeOnly.TryParseExact(s, "HHmmss", out var t)) return t;
        return null;
    }

    /// <summary>Extrai o caractere do segmento (posição 14, índice 0-based 13).</summary>
    public static char ExtrairSegmento(this string linha)
        => linha[13];

    /// <summary>Formata int com zeros à esquerda.</summary>
    public static string PadNum(this int value, int tamanho)
        => value.ToString().PadLeft(tamanho, '0');

    /// <summary>Formata long com zeros à esquerda.</summary>
    public static string PadNum(this long value, int tamanho)
        => value.ToString().PadLeft(tamanho, '0');

    /// <summary>Formata decimal como inteiro com zeros à esquerda (sem casas decimais).</summary>
    public static string PadNum(this decimal value, int tamanho)
        => ((long)value).ToString().PadLeft(tamanho, '0');

    /// <summary>Formata string alfanumérica com espaços à direita, truncando se necessário.</summary>
    public static string PadAlfa(this string? value, int tamanho)
        => (value ?? "").PadRight(tamanho)[..tamanho];

    /// <summary>Formata decimal com casas decimais como inteiro com zeros à esquerda.</summary>
    public static string PadDecimal(this decimal value, int tamanho, int dec)
    {
        var raw = (long)(value * (decimal)Math.Pow(10, dec));
        return raw.ToString().PadLeft(tamanho, '0');
    }

    /// <summary>Formata DateOnly como "ddMMyyyy" ou "00000000" se null.</summary>
    public static string FormatarData(this DateOnly? data)
        => data?.ToString("ddMMyyyy") ?? "00000000";

    /// <summary>Formata TimeOnly como "HHmmss" ou "000000" se null.</summary>
    public static string FormatarHora(this TimeOnly? hora)
        => hora?.ToString("HHmmss") ?? "000000";
}
