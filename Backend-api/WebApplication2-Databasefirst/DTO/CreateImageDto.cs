namespace WebApplication2_Databasefirst.DTO
{
    public class CreateImageDto
    {
        public CreateImageDto(int id, byte[] imageData)
        {
            Id = id;
            ImageData = imageData;
        }

        public int Id { get; set; }
        public byte[] ImageData { get; set; }
    }
}