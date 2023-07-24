namespace Api.Services.Helpers
{
    public static class PhotoService
    {
        public static byte[] ToByteArray(this IFormFile formFile)
        {
            byte[] fileData = null;

            using (var binaryReader = new BinaryReader(formFile.OpenReadStream()))
            {
                fileData = binaryReader.ReadBytes((int)formFile.Length);
            }

            return fileData;
        }
    }
}
