using System;

namespace nerderies.TelegramBotApi
{
    public class InputFile
    {
        #region public types

        public enum FileDescriptionMode
        {
            RealFile,
            ReferencedFile
        }

        #endregion

        #region private members

        private readonly string _fileIdOrUrl;
        private readonly byte[] _data;
        private readonly string _fileName;
        private readonly string _mime;

        #endregion

        #region .ctor

        public InputFile(byte[] data, string fileName, string mime)
        {
            _data = data;
            _fileName = fileName;
            _mime = mime;
            Mode = FileDescriptionMode.RealFile;
        }
        public InputFile(string fileIdorUrl)
        {
            _fileIdOrUrl = fileIdorUrl;
            Mode = FileDescriptionMode.ReferencedFile;
        }

        #endregion

        #region public properties

        public FileDescriptionMode Mode { get; private set; }

        #endregion

        #region public operations

        public MultiPartParameter GetMultiPartParameter(string fieldName)
        {
            if (Mode == FileDescriptionMode.RealFile)
            {
                return new MultiPartFileParameter(fieldName, _fileName, _data, _mime);
            }
            else
                if (Mode == FileDescriptionMode.ReferencedFile)
            {
                return new MultiPartStringParameter(fieldName, _fileIdOrUrl);
            }
            else
                throw new Exception("Unknown FileDescription Method");
        }

        #endregion
    }
}
