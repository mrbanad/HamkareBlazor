namespace HamkareBlazor;

public static class FileExtension
{
    private static List<string> ImageFormats { get; set; } = [".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg"];
    public static string ImageFormatString => string.Join(", ", ImageFormats);

    private static List<string> VideoFormat { get; set; } = [".mp4", ".webm", ".ogg", ".av1"];
    public static string VideoFormatString => string.Join(", ", VideoFormat);
}
