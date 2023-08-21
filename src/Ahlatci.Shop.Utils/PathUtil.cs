namespace Ahlatci.Shop.Utils
{
    public static class PathUtil
    {
        public static string GenerateFileNameFromBase64File(string base64Image)
        {
            var date = DateTime.Now;
            var extension = GetExtensionFromBase64String(base64Image);
            var fileName = $"{date.Day}_{date.Month}_{date.Year}_{date.Hour}_{date.Minute}_{date.Second}_{date.Millisecond}{extension}";
            return fileName;
        }

        //Dosya base64 string olarak alınmaktadır.
        //base64 string bilgi içerisinde yer alan ilk 5 karakterden dosya türü öğrenilebilmektedir.
        private static string GetExtensionFromBase64String(string base64Image)
        {
            //Örnek olarak bir gif resmi için 
            //tarayıcıdan upload yapıldığında resim bilgisi [data:image/gif;base64,R0lGODlh7gI2BdU/AP.......] şeklinde gelir.
            //postman ile upload yapıldığında aynı bilgi [R0lGODlh7gI2BdU/AP.......] şeklinde gelir.
            //Aşağıdaki koşul bu nedenle yazıldı.
            var fileTypeString = base64Image.Contains("base64") ? base64Image.Split(",")[1] : base64Image;
            switch (fileTypeString.Substring(0, 5).ToUpper())
            {
                case "iVBOR":
                    return ".png";
                case "/9J/4":
                    return ".jpg";
                case "R0lGO":
                    return ".gif";
                default:
                    return ".png";
            }
        }
    }
}
