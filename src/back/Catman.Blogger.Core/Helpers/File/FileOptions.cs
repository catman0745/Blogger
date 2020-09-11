namespace Catman.Blogger.Core.Helpers.File
{
    using System.Collections.Generic;

    public class FileOptions : IFileOptions
    {
        public long MaxImageSize { get; set; }
        
        public ICollection<string> SupportedImageTypes { get; set; }
        
        public string UploadsDirectoryPath { get; set; }
    }
}
