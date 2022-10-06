namespace BlazorOS1.Models
{
    public class IconModel
    {
        public int Id { get; set; }
        public int IdContainer { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public FileTypes Type { get; set; }
    }

    public enum FileTypes
    {
        sys,
        img,
        txt,
        mp3
    }
}