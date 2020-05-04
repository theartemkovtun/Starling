namespace Starling.Application.DTOs
{
    public class FileDataDto
    {
        public FileDataDto(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }
            
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}