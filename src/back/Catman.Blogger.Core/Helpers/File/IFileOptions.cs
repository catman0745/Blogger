namespace Catman.Blogger.Core.Helpers.File
{
    using System.Collections.Generic;

    public interface IFileOptions
    {
        long MaxImageSize { get; }
        
        ICollection<string> SupportedImageTypes { get; }
        
        string UploadsDirectoryPath { get; }
    }
}
